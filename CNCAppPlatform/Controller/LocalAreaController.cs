﻿using CefSharp.DevTools.CSS;
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
        private static int[] _record_node_status;
        private static int[] _RecordNodeStatus        // 紀錄的區域狀態
        {
            get => _record_node_status;
            set
            {
                _record_node_status = value;
                // 建立對照表
                var map = new Dictionary<int, string>
                {
                    { 0, "無貨架" },
                    { 1, "空載" },
                    { 2, "滿載" }
                };

                // 使用 LINQ 將 int[] 轉換為 string[]
                string[] result = value.Select(n => map.ContainsKey(n) ? map[n] : "unknown").ToArray();

                BindControl.UpdateContainerText(result);
            }
        }      

        private static DateTime? full_time = null;

        // 定義貨架前後狀態轉換對應的動作：0=無動作, 1=入貨, 2=異常
        private static readonly Dictionary<(int, int), int> _carry_rules = new Dictionary<(int, int), int>
            {
                { (0, 0), 0 },      // 無變化
                { (0, 1), 2 },      // 錯誤 (空貨架進站)
                { (0, 2), 0 },      // 貨架進站
                { (1, 0), 2 },      // 錯誤 (空貨架出站)
                { (1, 1), 0 },      // 無變化
                { (1, 2), 1 },      // 入貨
                { (2, 0), 0 },      // 貨架出站
                { (2, 1), 0 },      // 取貨
                { (2, 2), 0 }       // 無變化
            };


        public static KukaAreaControl BindControl { get; set; }

        public static event EventHandler<HttpListenerDispatcher.HeardEventArgs> StepChanged;        // 流程變更事件
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

            return isconn;
        }

        /// <summary>
        /// 重設區域IO數量，更新綁定區域時需要執行一次
        /// </summary>
        public static void ResetIOCount()
        {
            if(KukaParm.BindAreaModel != null)
            {
                _sensor_dispatcher.SensorRead -= ModbusTCPDispatcher_SensorRead;
                _sensor_dispatcher.RegisterCount = KukaParm.BindAreaModel.NodeList.Length * 2 + 1;      // 每個工作站 2 個 sensor + 按鈕 1 個
                _sensor_dispatcher.SensorRead += ModbusTCPDispatcher_SensorRead;
            }
        }

        /// <summary>
        /// 接收感測器資訊事件時，更新綁定模型資料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ModbusTCPDispatcher_SensorRead(object sender, SensorDataEventArgs e)
        {
            int[] current_node_status = ToNodeStatus(e.Data).ToArray();    // 當前節點狀態
            
            // 如果節點狀態有變更才執行
            if (!current_node_status.SequenceEqual(KukaParm.BindAreaModel.NodeStatus))
            {
                KukaParm.BindAreaModel.NodeStatus = current_node_status;
                BindControl?.UpdateContainerImage(KukaParm.BindAreaModel.NodeStatus.ToArray());        // 更新圖片

                ChatController.SyncNodeStatus(KukaParm.BindAreaModel);
            }

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
            int data_length = e.Data.Length - 1;
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

        public static void UpdateControl()
        {
            // 判定綁定區域是否存在/更新
            if (KukaParm.BindAreaModel == null || BindControl == null) return;


            // 更新控制項為綁定區域資訊
            //bind_control.Dock = DockStyle.Fill;
            //bind_control.Margin = new Padding(10);
            BindControl.AreaName = KukaParm.BindAreaModel.AreaName;
            BindControl.AreaCode = KukaParm.BindAreaModel.AreaCode;
            BindControl.AreaNode = KukaParm.BindAreaModel.NodeList.ToArray();
            BindControl.UpdateContainerImage(KukaParm.BindAreaModel.NodeStatus.ToArray());        // 初次建立，更新圖片
            // KukaParm.BindAreaModel.ControlUI = bind_control;
        }

        public static void InitAreaStatus()
        {
            int[] current_status = KukaParm.BindAreaModel?.NodeStatus;       // 當前區域狀態

            _RecordNodeStatus = current_status;
        }

        public static bool TryCreateCarryTask()
        {
            // 透過與歷史狀態的比對，判定當前區域的動作狀態
            // 若動作狀態為可出貨，執行以下
            // * 返回當前是否可派發任務
            // * 自動設定搬運任務的起點與終點

            int[] current_status = KukaParm.BindAreaModel?.NodeStatus;       // 當前區域狀態

            // 第一次執行，初始化歷史狀態
            if (_record_node_status == null)
            {
                _RecordNodeStatus = current_status;
                return false;
            }

            // 若狀態無改變，無需處理
            if (_record_node_status.SequenceEqual(current_status))
            {
                MsgBox.ShowFlash("貨架狀態沒有變化", "區域貨架異常", 1000);
                Log.Append("貨架狀態沒有變化", "WARN", "LocalAreaController");
                return false;
            }

            List<int> node_action = new List<int>();        // 區域動作狀態判定

            // 比對每一格的前後狀態，轉換為動作
            for (int i = 0; i < current_status.Length; i++)
            {
                var key = (_record_node_status[i], current_status[i]);
                node_action.Add(_carry_rules.TryGetValue(key, out var action) ? action : 0);
            }

            // 處理異常狀況
            if (node_action.Contains(2))
            {
                MsgBox.Show("資料異常", "區域貨架異常");
                Log.Append($"資料異常 [{string.Join(",", _record_node_status)}] => [{string.Join(",", current_status)}]", "ERROR", "LocalAreaController");
                return false;
            }
            else if (node_action.Count(n => n == 1) >= 2)
            {
                MsgBox.Show("可派發任務 > 1 筆", "區域貨架異常");
                Log.Append("可派發任務 > 1 筆", "WARN", "LocalAreaController");
                return false;
            }
            else if (node_action.Contains(1))
            {
                // 目標區域不可進貨（滿載）
                //if (KukaParm.TargetAreaModel.NodeStatus.Length>0 && !KukaParm.TargetAreaModel.NodeStatus.Contains(0))
                //{
                //    MsgBox.Show("目標區域滿載", "搬運任務異常");
                //    Log.Append("目標區域滿載", "WARN", "LocalAreaController");
                //    return false;
                //}
                // 目標是否滿仔應該在搬運前判定，而不是建立時判定

                string carry_node = KukaParm.BindAreaModel.NodeList[node_action.IndexOf(1)];        // 找到第一個需要入貨的節點

                // 設定搬運起點與終點
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

                _RecordNodeStatus = current_status;        // 更新歷史狀態


                return true;
            }
            _RecordNodeStatus = current_status;        // 更新歷史狀態
            // 沒有可派任務
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
            return index == -1 ? 0 : index + 1;

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

        public static void PubReady()
        {
            int _bind_station_no = GetStationNo();

            string feedback_string = INiReader.ReadINIFile(Env.LayoutPath, "Control", $"station{_bind_station_no}");
            string[] feedback_msgs = feedback_string.Split(';');

            if (_bind_station_no != 0)
            {
                //ChatController.SendFeedbackInfo($"station{_bind_station_no}_agv_ready");
                ChatController.SendFeedbackInfo(feedback_msgs[1]);
            }
        }

        public static void AreaReadyFunc()
        {
            int _bind_station_no = GetStationNo();

            string feedback_string = INiReader.ReadINIFile(Env.LayoutPath, "Control", $"station{_bind_station_no}");
            string[] feedback_msgs = feedback_string.Split(';');

            if (_bind_station_no != 0)
            {
                ChatController.SendFeedbackInfo(feedback_msgs[0]);
            }

        }

        public static void PubRobotFunc()
        {
            int _bind_station_no = GetStationNo();

            string feedback_string = INiReader.ReadINIFile(Env.LayoutPath, "Control", $"station{_bind_station_no}");
            string[] feedback_msgs = feedback_string.Split(';');

            if (_bind_station_no != 0)
            {
                //ChatController.SendFeedbackInfo($"station{_bind_station_no}_agv_star");
                ChatController.SendFeedbackInfo(feedback_msgs[2]);
            }

        }

        public static void PubRobotOut()
        {
            int _bind_station_no = GetStationNo();

            string feedback_string = INiReader.ReadINIFile(Env.LayoutPath, "Control", $"station{_bind_station_no}");
            string[] feedback_msgs = feedback_string.Split(';');

            if (_bind_station_no != 0)
            {
                //ChatController.SendFeedbackInfo($"station{_bind_station_no}_agv_begin");
                ChatController.SendFeedbackInfo(feedback_msgs[3]);
            }
                
        }

        public static void PubCarryOver()
        {
            // 頭尾未形成迴圈
            int _bind_station_no = GetStationNo() + 1;
            string feedback_string = INiReader.ReadINIFile(Env.LayoutPath, "Control", $"station{_bind_station_no}");
            string[] feedback_msgs = feedback_string.Split(';');

            if (_bind_station_no != 0)
            {
                //ChatController.SendFeedbackInfo($"station{_bind_station_no}_agv_end");
                ChatController.SendFeedbackInfo(feedback_msgs[4]);
            }
                
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
