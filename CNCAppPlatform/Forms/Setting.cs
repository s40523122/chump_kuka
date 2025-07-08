using Chump_kuka.Controller;
using Chump_kuka.Controls;
using iCAPS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chump_kuka.Forms
{
    public partial class Setting : Form
    {
        public Setting()
        {
            InitializeComponent();
            Load += Setting_Load;

            Env.EnvChanged += (s, e) => bind_comboBox.Text = Env.BindAreaName;
            KukaParm.AreaChanged += AreaChanged; ;
            // VisibleChanged += (s, e) => comboBox1.Text = KukaParm.BindArea?.AreaName;

            bind_comboBox.Items.Add("加工區");
        }

        private void AreaChanged(object sender, PropertyChangedEventArgs e)
        {
            bind_comboBox.Invoke(new Action(() =>
            {
                // 當區域列表出現變化時，同步更新綁定區域的下拉式選單，以便及時更改綁定區域
                bind_comboBox.Items.Clear();
                foreach (KukaAreaModel area in KukaParm.KukaAreaModels)
                {
                    bind_comboBox.Items.Add(area);
                    
                    // 若當前選單文字符合加入資料，強制觸發選取事件
                    if (area.AreaName == bind_comboBox.Text)
                    {
                        bind_comboBox.SelectedIndex = bind_comboBox.Items.Count - 1;
                    }
                }
            }));
            
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            local_ip_combo.Text = Env.LocalIp ?? "";
            switch_client.Checked = !Env.ICapsServer;
            linker_server_ip.Text = Env.IcapsLinkerServerIp ?? "";
            linker_server_port.Text = Env.IcapsLinkerServerPort ?? "5700";
            kuka_request_url.Text = Env.KukaApiUrl;
            modbus_ip.Text = Env.SensorModbusTcp?.Address.ToString();
            modbus_port.Text = Env.SensorModbusTcp?.Port.ToString() ?? "502";
            tcp_record_port.Text = Env.RecordLogTcpPort ?? "6400";
            kuka_response_url.Text = Env.KukaResponseUrl ?? "";

            bind_comboBox.Text = Env.BindAreaName ?? "";
        }

        private async Task RunTask(int start_val, int end_val, string running_msg, Task task)
        {
            // 設定任務開始前，進度條描述 & 數值
            progress_msg.Text = running_msg;
            progressBar1.Value = start_val;

            // 等待非同步任務完成
            await task;

            // 任務開始前，進度條數值
            progressBar1.Value = end_val;
        }

        private async Task KukaApiTask()
        {
            if (!Env.ICapsServer)
                return;
            // TODO 關閉現有連線
            bool isconn = await KukaApiController.ConnectAndCheck(kuka_request_url.Text);
            kuka_api_check.Change = isconn;
            kuka_api_check.Visible = true;

            // 若成功連線則加入區域查詢，作為稍後綁定區域的依據
            if (isconn)
            {
                //KukaApiController.GetAreaInfo();
                Env.KukaApiUrl = kuka_request_url.Text;
            }

            KukaApiController.GetRobotStatus();
        }
        private async Task ServerTask()
        {
            //bool is_icaps_server = bool.TryParse(radio_button_group.Controls        // 判定是否指定為 iCaps 伺服器
            //                                     .OfType<RadioButton>()
            //                                     .FirstOrDefault(rb => rb.Checked)
            //                                     .Tag.ToString(),
            //                                 out is_icaps_server);

            // SocketDispatcher _icaps_socket = new SocketDispatcher();
            //isconn = await SocketDispatcher.StartRecordListener(int.Parse(tcp_server_port.Text));


            IPEndPoint listen_server_ipep = new IPEndPoint(IPAddress.Parse(linker_server_ip.Text), int.Parse(linker_server_port.Text));       // 開啟 Linker 通訊

            bool isconn = await ChatController.Init(Env.ICapsServer, listen_server_ipep);

            Env.IcapsLinkerServerIp = linker_server_ip.Text;
            Env.IcapsLinkerServerPort = linker_server_port.Text;
            
            server_check.Change = isconn;
            server_check.Visible = true;
            
            // TODO
            // 等待訊息回應，目前透過等待 1.5 秒完成此效果
            await Task.Delay(1500);
        }

        private async Task RecordLogTask()
        {
            if (!Env.ICapsServer)
                return;
            
            // SocketDispatcher _icaps_socket = new SocketDispatcher();
            bool isconn = await FeedbackDispatcher.StartRecordListener(int.Parse(tcp_record_port.Text));
            if (isconn)
            {
                Env.RecordLogTcpPort = tcp_record_port.Text;
            }

            record_log_check.Change = isconn;
            record_log_check.Visible = true;
        }

        private async Task SensorModbusTask()
        {
            // Console.WriteLine(KukaParm.KukaAreaModels);
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(modbus_ip.Text), int.Parse(modbus_port.Text));
            bool isconn = await LocalAreaController.BuildBindArea(ip);

            sensor_check.Change = isconn;
            sensor_check.Visible = true;
            if (isconn)
            {
                Env.SensorModbusTcp = ip;
                LocalAreaController.ResetIOCount();
            }
        }

        private async Task KukaResponseTask()
        {
            if (!Env.ICapsServer)
                return;

            bool isconn = await KukaApiController.StartListen(kuka_response_url.Text);
            kuka_response_check.Change = isconn;
            kuka_response_check.Visible = true;

            if (isconn)
            {
                Env.KukaResponseUrl = kuka_response_url.Text;
            }
        }

        private async void connTest_Click(object sender, EventArgs e)
        {
            Env.LocalIp = local_ip_combo.Text;
            kuka_api_check.Visible = kuka_response_check.Visible = record_log_check.Visible = sensor_check.Visible = server_check.Visible = false;
            // 依序執行連線任務
            await RunTask(15, 20, "等待 iCAPS 伺服器開啟...", ServerTask());
            await RunTask(35, 40, "等待 KUKA API 連線...", KukaApiTask());

            if (switch_sever.Checked)
            {
                string strategy_string = Env.Strategy;      // 取得策略(搬運區域順序)
                if (strategy_string != "")
                {
                    List<string> sortedItems = strategy_string.Split(';').ToList();

                    List<KukaAreaModel> temp = new List<KukaAreaModel>();
                    foreach (var name in sortedItems)
                    {
                        var matched = KukaParm.KukaOriginAreaModels.FirstOrDefault(p => p.AreaName == name);
                        if (matched != null)
                        {
                            if (matched.NodeList == null)
                            {
                                Console.WriteLine("Is NULL");
                            }
                            temp.Add(matched);
                        }
                    }

                    KukaParm.KukaAreaModels = temp;
                }
            }
            else
            {
                await Task.Delay(2000);
            }

            await RunTask(55, 60, "等待 Modbus Tcp 連線...", SensorModbusTask());
            await RunTask(75, 80, "等待 KUKA 回應監聽開啟...", KukaResponseTask());
            await RunTask(95, 100, "等待工時監測伺服器開啟...", RecordLogTask());
            //bind_comboBox.SelectedIndex = 0;        // 強制套用當前選項
            progress_msg.Text = "已完成";
        }

        private void bind_SelectedIndexChanged(object sender, EventArgs e)
        {
            if((sender as ComboBox).SelectedItem is KukaAreaModel select_model)
            {
                KukaParm.BindAreaModel = select_model;      // 將指定模型淺複製為 BindAreaModel (數值更改會影響原列表)
            }

            LocalAreaController.ResetIOCount();
        }

        private void switch_client_CheckedChanged(object sender, EventArgs e)
        {
            Env.ICapsServer = switch_sever.Checked;
            kuka_request_url.Enabled = tcp_record_port.Enabled = kuka_response_url.Enabled = station_setting.Enabled = Env.ICapsServer;
            
        }

        private void station_setting_Click(object sender, EventArgs e)
        {
            // List<string> items = new List<string> { "項目1", "項目2", "項目3", "項目4" };
            List<string> list = KukaParm.KukaOriginAreaModels.Select(m => m.AreaName).ToList();
            List<string> sortedItems = SortableListForm.ShowDialogAndSort(list);

            KukaParm.KukaAreaModels = new List<KukaAreaModel>();
            List <KukaAreaModel> temp = new List<KukaAreaModel>();
            foreach (var name in sortedItems)
            {
                var matched = KukaParm.KukaOriginAreaModels.FirstOrDefault(p => p.AreaName == name);
                if (matched != null)
                {
                    temp.Add(matched);
                }
            }

            KukaParm.KukaAreaModels = temp;

            Env.Strategy = string.Join(";", sortedItems);
            // MessageBox.Show(KukaParm.KukaAreaModels[0].AreaName);

            KukaParm.BindAreaModel = KukaAreaModel.Find(Env.BindAreaName, KukaParm.KukaAreaModels);       // 將指定模型淺複製為 BindAreaModel


            if (KukaParm.BindAreaModel == null)
            {
                Log.Append($"綁定區域({Env.BindAreaName})不存在", "Error", "LocalAreaController.cs");
            }
        }

        private void LoadNetworkInterfaces()
        {
            local_ip_combo.Items.Clear();

            var interfaces = NetworkInterface.GetAllNetworkInterfaces()
                .Where(ni =>
                    ni.OperationalStatus == OperationalStatus.Up &&
                    ni.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .SelectMany(ni => ni.GetIPProperties().UnicastAddresses)
                .Where(ip => ip.Address.AddressFamily == AddressFamily.InterNetwork)
                .Select(ip => ip.Address.ToString())
                .ToList();

            if (interfaces.Count == 0)
            {
                local_ip_combo.Items.Add("沒有可用的 IPv4 網卡");
                local_ip_combo.Enabled = false;
            }
            else
            {
                local_ip_combo.Items.AddRange(interfaces.ToArray());
                local_ip_combo.SelectedIndex = 0;
            }
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            LoadNetworkInterfaces();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine(KukaParm.KukaAreaModels);
        }
    }
}
