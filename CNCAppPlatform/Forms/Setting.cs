using Chump_kuka.Controller;
using Chump_kuka.Controls;
using Modbus.Device;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
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

            Env.EnvChanged += (s, e) => comboBox1.Text = Env.BindAreaName;
            KukaParm.AreaChanged += AreaChanged; ;
            // VisibleChanged += (s, e) => comboBox1.Text = KukaParm.BindArea?.AreaName;

            comboBox1.Items.Add("加工區");
        }

        private void AreaChanged(object sender, PropertyChangedEventArgs e)
        {
            // 當區域列表出現變化時，同步更新綁定區域的下拉式選單，以便及時更改綁定區域
            comboBox1.Items.Clear();
            foreach (KukaAreaModel area in KukaParm.KukaAreaModels)
            {
                comboBox1.Items.Add(area.AreaName);
            }
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            udp_server_ip.Text = Env.IcapsServerUdpIp ?? "";
            upd_server_port.Text = Env.IcapsServerUdpPort ?? "5700";
            kuka_request_url.Text = Env.KukaApiUrl;
            modbus_ip.Text = Env.SensorModbusTcp?.Address.ToString();
            modbus_port.Text = Env.SensorModbusTcp?.Port.ToString() ?? "502";
            tcp_record_port.Text = Env.RecordLogTcpPort ?? "6400";
            kuka_response_url.Text = Env.KukaResponseUrl ?? "";

            comboBox1.Text = Env.BindAreaName ?? "";
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


            IPEndPoint listen_ipep = new IPEndPoint(IPAddress.Parse(udp_server_ip.Text), int.Parse(upd_server_port.Text));       // 開啟 UDP 監聽
            bool is_server = is_sever_check.Checked;
            CommController.Init(is_server, listen_ipep);

            Env.IcapsServerUdpIp = udp_server_ip.Text;
            Env.IcapsServerUdpPort = upd_server_port.Text;
            
            server_check.Change = true;
            server_check.Visible = true;
        }

        private async Task RecordLogTask()
        {
            bool record_log_server = bool.TryParse(radio_button_group.Controls        // 判定是否指定為 iCaps 伺服器
                                                 .OfType<RadioButton>()
                                                 .FirstOrDefault(rb => rb.Checked)
                                                 .Tag.ToString(),
                                             out record_log_server);
            bool isconn = false;
            if (record_log_server)
            {
                // SocketDispatcher _icaps_socket = new SocketDispatcher();
                isconn = await SocketDispatcher.StartRecordListener(int.Parse(tcp_record_port.Text));
                if (isconn)
                {
                    Env.RecordLogTcpPort = tcp_record_port.Text;
                }
            }

            record_log_check.Change = isconn;
            record_log_check.Visible = true;
        }

        private async Task SensorModbusTask()
        {
            ModbusTCPDispatcher.Enable = true;
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(modbus_ip.Text), int.Parse(modbus_port.Text));
            bool isconn = await ModbusTCPDispatcher.CheckConnect(ip);
            sensor_check.Change = isconn;
            sensor_check.Visible = true;
            if (isconn) Env.SensorModbusTcp = ip;
        }

        private async Task KukaResponseTask()
        {
            bool isconn = await CarryTaskController.StartListenKuka(kuka_response_url.Text);
            kuka_response_check.Change = isconn;
            kuka_response_check.Visible = true;

            if (isconn)
            {
                Env.KukaResponseUrl = kuka_response_url.Text;
            }
        }

        private async void scaleButton1_Click(object sender, EventArgs e)
        {
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
            LocalAreaController.BindArea = KukaAreaModel.Find(comboBox1.Text, KukaParm.KukaAreaModels);
        }
    }
}
