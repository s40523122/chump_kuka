using CefSharp.DevTools.CSS;
using Chump_kuka.Controls;
using Chump_kuka.Dispatchers;
using iCAPS;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chump_kuka.Controller
{
    internal class LocalAreaController
    {
        private static ModbusTCPDispatcher _sensor_dispatcher = null;
        private static List<int> _record_node_status = null;      // 紀錄的區域狀態
        private static DateTime? full_time = null;

        public static event EventHandler<HttpListenerDispatcher.HeardEventArgs> StepChanged;
        public static event EventHandler<ButtonPushEventArgs> ButtonPush;

        static LocalAreaController()
        {
            _sensor_dispatcher = new ModbusTCPDispatcher();

            ChatController.StepChanged += (s, e) => StepChanged?.Invoke(s, e);


        }

        public async static Task<bool> BuildBindArea(IPEndPoint modbus_tco_ip)
        {
            // 確認是否已經指定綁定區域名稱
            // 如果未指定名稱，開啟詢問表單
            // 反之，建立模型副本
            if (string.IsNullOrEmpty(Env.BindAreaName))
            {
                Form form = new Form() { StartPosition = FormStartPosition.CenterParent };

                ComboBox comboBox = new ComboBox();
                foreach (KukaAreaModel area in KukaParm.KukaAreaModels)
                {
                    comboBox.Items.Add(area.AreaName);
                }

                FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
                flowLayoutPanel.Controls.AddRange(new Control[2] { new Label() { Text = "請選擇綁定區域" }, comboBox });

                form.Controls.Add(flowLayoutPanel);

                comboBox.SelectedValueChanged += (_sender, _e) =>
                    Env.BindAreaName = comboBox.Text;
                    // KukaParm.BindAreaModel = KukaAreaModel.Find(comboBox.Text, KukaParm.KukaAreaModels);

                form.ShowDialog();
            }
            
            KukaParm.BindAreaModel = KukaAreaModel.Find(Env.BindAreaName, KukaParm.KukaAreaModels);       // 將指定模型淺複製為 BindAreaModel
            KukaParm.TargetAreaModel = KukaAreaModel.Find(Env.TargetAreaName, KukaParm.KukaAreaModels);

            if (KukaParm.BindAreaModel == null)
            {
                Log.Append($"綁定區域({Env.BindAreaName})不存在", "Error", "LocalAreaController.cs");
                return false;
            }

            // sensor_dispatcher.Enable = true;
            bool isconn = await _sensor_dispatcher.Start(modbus_tco_ip);
            if (isconn)
            {
                _sensor_dispatcher.RegisterCount = KukaParm.BindAreaModel.NodeList.Count * 2 + 1;      // 每個工作站 2 個 sensor + 按鈕 1 個
                _sensor_dispatcher.SensorRead += ModbusTCPDispatcher_SensorRead;
            }

            return isconn;
        }

        /// <summary>
        /// 接收感測器資訊事件時，更新綁定模型資料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ModbusTCPDispatcher_SensorRead(object sender, SensorDataEventArgs e)
        {
            KukaParm.BindAreaModel.NodeStatus = ToNodeStatus(e.Data);
            int data_length = e.Data.Length-1;

            // 若區域滿載達指定時數後，觸發亮燈
            bool[] result = e.Data.Take(e.Data.Length - 1).ToArray();
            bool is_alert = CheckFullAreaAndDuration(result.ToList(), 5);        // 等待5秒
            if (is_alert)
            {
                TurnOnLight();
            }
            else
            {
                TurnOffLight();
            }

            // 若按鈕狀態為 true ，觸發訊息
            bool button_state = e.Data[data_length];
            if (button_state)
            {
                ButtonPush?.Invoke(sender, new ButtonPushEventArgs(button_state));
            }

        }

        private static bool CheckFullAreaAndDuration(List<bool> statusList, int requiredSeconds)
        {
            bool all_dectect = statusList.TrueForAll(x => x == true);

            if (all_dectect)
            {
                if (full_time == null)
                    full_time = DateTime.Now;
                else if ((DateTime.Now - full_time.Value).TotalSeconds >= requiredSeconds)
                    return true;
            }
            else
            {
                full_time = null;

            }

            return false;
        }

        private static List<int> ToNodeStatus(bool[] input_readers)
        {
            List<int> status = new List<int>();
            int length = input_readers.Length - 3;
            for (int i = 0; i <= length; i += 2)
            {
                bool turtle_sensor = input_readers[i];    // 先取出當前索引的值，並遞增索引
                bool rack_sensor = input_readers[i + 1];    // 取得遞增後的索引值

                /*
                - 若 rack_sensor 為 false，代表無貨架，狀態設定為 0。
                - 若 rack_sensor 為 true 且 turtle_sensor 為 false，代表有貨架但無烏龜車，狀態設定為 1。
                - 若 rack_sensor 為 true 且 turtle_sensor 為 true，代表有貨架且有烏龜車，狀態設定為 2。
                */
                status.Add(!rack_sensor ? 0 : (!turtle_sensor ? 1 : 2));
            }
            
            return status;
        }

        public static void UpdateControl(KukaAreaControl bind_control)
        {
            // 判定綁定區域是否存在/更新
            if (KukaParm.BindAreaModel == null) return;
            
            // 如果控制項已綁定，不做後續動作
            if (KukaParm.BindAreaModel.UserControls.Contains(bind_control)) return;

            // 更新控制項為綁定區域資訊
            bind_control.Dock = DockStyle.Fill;
            bind_control.Margin = new Padding(10);
            bind_control.AreaName = KukaParm.BindAreaModel.AreaName;
            bind_control.AreaCode = KukaParm.BindAreaModel.AreaCode;
            bind_control.AreaNode = KukaParm.BindAreaModel.NodeList.ToArray();
            bind_control.UpdateContainerImage(KukaParm.BindAreaModel.NodeStatus.ToArray());        // 初次建立，更新圖片
            KukaParm.BindAreaModel.UserControls.Add(bind_control);
        }

        public static bool GetTaskNode()
        {
            List<int> current_status = KukaParm.BindAreaModel.NodeStatus;

            if (_record_node_status == null)
            {
                _record_node_status = current_status;
                return false;
            }

            if (_record_node_status.SequenceEqual(current_status))
            {
                MsgBox.Show("沒有變化", "區域貨架異常");
                return false;
            }

            // 分析貨架前後變化，判定目前工作狀態
            var rules = new Dictionary<(int, int), int>
            {
                { (0, 0), 0 },      // 無變化
                { (0, 1), 2 },      // 錯誤
                { (0, 2), 0 },      // 貨架進站
                { (1, 0), 2 },      // 錯誤
                { (1, 1), 0 },      // 無變化
                { (1, 2), 1 },      // 入貨
                { (2, 0), 0 },      // 貨架出站
                { (2, 1), 0 },      // 取貨
                { (2, 2), 0 }       // 無變化
            };

            List<int> result = new List<int>();

            for (int i = 0; i < current_status.Count; i++)
            {
                if (rules.TryGetValue((_record_node_status[i], current_status[i]), out int value))
                    result.Add(value);
                else
                    result.Add(0);
            }

            _record_node_status = current_status;

            if (result.Contains(2) || result.Count(n => n == 1) >= 2)
            {
                MsgBox.Show("資料異常", "區域貨架異常");
            }
            else if (result.Contains(1))
            {
                // return _bind_area.NodeList[result.IndexOf(1)];
                string carry_node = KukaParm.BindAreaModel.NodeList[result.IndexOf(1)];
                // TODO: 怎麼判定目前區域
                // 1. 獲取當前節點 OK
                // 2. 獲取目標區域
                // 3. 透過 laser 判定目標區域是否滿載 

                KukaParm.StartNode = new CarryNode()
                {
                    Code = carry_node,
                    Name = carry_node,
                    Type = "NODE_POINT"
                };
                KukaParm.GoalNode = new CarryNode()
                {
                    Code = KukaParm.TargetAreaModel.AreaCode,       // "A000000002",
                    Name = KukaParm.TargetAreaModel.AreaName,       // "倉庫區",
                    Type = "NODE_AREA"
                };

                return true;
            }

            return false;
        }

        public static void TurnOnLight()
        {
            _sensor_dispatcher.LightControl(true);
        }

        public static void TurnOffLight()
        {
            _sensor_dispatcher.LightControl(false);
        }

        public static int GetStationNo()
        {
            int index = KukaParm.KukaAreaModels.FindIndex(m => m.AreaName == KukaParm.BindAreaModel.AreaName);
            return index == -1 ? 0 : index;

            switch (KukaParm.BindAreaModel.AreaName)
            {
                case "产线作业区":
                    return  1;
                case "产线上料区":
                    return 2;
                default:
                    return 0;
            }
        }
        public static void PubRobotIn()
        {
            int _bind_station_no = GetStationNo();
            if (_bind_station_no != 0)
                SocketDispatcher.SendToRecordSystem($"station{_bind_station_no}_agv_star");
        }

        public static void PubRobotOut()
        {
            int _bind_station_no = GetStationNo();
            if (_bind_station_no != 0)
                SocketDispatcher.SendToRecordSystem($"station{_bind_station_no}_agv_begin");
        }

        public static void PubCarryOver()
        {
            int _bind_station_no = GetStationNo();
            if (_bind_station_no != 0)
                SocketDispatcher.SendToRecordSystem($"station{_bind_station_no}_agv_end");
        }
    }

    public class ButtonPushEventArgs : EventArgs
    {
        public bool Status { get; }

        public ButtonPushEventArgs(bool status)
        {
            Status = status;
        }
    }
}
