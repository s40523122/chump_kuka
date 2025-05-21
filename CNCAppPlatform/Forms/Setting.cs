using Chump_kuka.Controller;
using Chump_kuka.Controls;
using iCAPS;
using System;
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
                target_comboBox.Items.Clear();
                foreach (KukaAreaModel area in KukaParm.KukaAreaModels)
                {
                    bind_comboBox.Items.Add(area.AreaName);
                    target_comboBox.Items.Add(area.AreaName);
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
            target_comboBox.Text = Env.TargetAreaName ?? "";
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
                KukaApiController.GetAreaInfo();
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
            // 等待訊息回應，目前透過等待 1 秒完成此效果
            await Task.Delay(1000);
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
            Console.WriteLine(KukaParm.KukaAreaModels);
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

        private async void scaleButton1_Click(object sender, EventArgs e)
        {
            Env.LocalIp = local_ip_combo.Text;
            kuka_api_check.Visible = kuka_response_check.Visible = record_log_check.Visible = sensor_check.Visible = server_check.Visible = false;
            // 依序執行連線任務
            await RunTask(15, 20, "等待 iCAPS 伺服器開啟...", ServerTask());
            await RunTask(35, 40, "等待 KUKA API 連線...", KukaApiTask());
            await RunTask(55, 60, "等待 Modbus Tcp 連線...", SensorModbusTask());
            await RunTask(75, 80, "等待 KUKA 回應監聽開啟...", KukaResponseTask());
            await RunTask(95, 100, "等待工時監測伺服器開啟...", RecordLogTask());

            progress_msg.Text = "已完成";            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            KukaParm.BindAreaModel = KukaAreaModel.Find(bind_comboBox.Text, KukaParm.KukaAreaModels);       // 將指定模型淺複製為 BindAreaModel
            LocalAreaController.ResetIOCount();
        }
        private void target_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            KukaParm.TargetAreaModel = KukaAreaModel.Find(target_comboBox.Text, KukaParm.KukaAreaModels);
        }

        private void switch_client_CheckedChanged(object sender, EventArgs e)
        {
            Env.ICapsServer = switch_sever.Checked;
            kuka_request_url.Enabled = tcp_record_port.Enabled = kuka_response_url.Enabled = Env.ICapsServer;
        }

        private void station_setting_Click(object sender, EventArgs e)
        {
            // List<string> items = new List<string> { "項目1", "項目2", "項目3", "項目4" };
            List<string> list = KukaParm.KukaAreaModels.Select(m => m.AreaName).ToList();
            List<string> sortedItems = SortableListForm.ShowDialogAndSort(list);

            // 使用 Sort 來依照 sortedItems 調整順序
            KukaParm.KukaAreaModels.Sort((a, b) => sortedItems.IndexOf(a.AreaName).CompareTo(sortedItems.IndexOf(b.AreaName)));
            // MessageBox.Show(KukaParm.KukaAreaModels[0].AreaName);
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

    }
}
