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
            kuka_api_url.Text = Env.KukaApiUrl;
            modbus_ip.Text = Env.SensorModbusTcp.Address.ToString();
            modbus_port.Text = Env.SensorModbusTcp.Port.ToString();
            comboBox1.Text = Env.BindAreaName ?? "";
        }

        private async void scaleButton1_Click(object sender, EventArgs e)
        {
            bool isconn = false;

            // 暫存參數，防止連線失敗，覆蓋參數
            string kuka_url_temp = Env.KukaApiUrl;
            IPEndPoint modbus_temp = Env.SensorModbusTcp;

            // 讀取文字方塊內容，作為連線資訊依據
            try
            {
                Env.KukaApiUrl = kuka_api_url.Text;
                Env.SensorModbusTcp = new IPEndPoint(IPAddress.Parse(modbus_ip.Text), int.Parse(modbus_port.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            progress_msg.Text = "等待 KUKA API 連線...";
            progressBar1.Value = 15;
            KukaApiController.Enable = true;
            isconn = await KukaApiController.CheckConnect();
            kuka_api_check.Change = isconn;
            kuka_api_check.Visible = true;
            
            // 若成功連線則加入區域查詢，作為稍後綁定區域的依據
            // 反之，回朔暫存資料
            if (isconn) KukaApiController.AppendAreaTask();
            else Env.KukaApiUrl = kuka_url_temp;
            progressBar1.Value = 25;

            progress_msg.Text = "等待 iCAPS 伺服器連線...";
            progressBar1.Value = 40;
            bool IcapsServer = bool.TryParse(radio_button_group.Controls        // 判定是否指定為 iCaps 伺服器
                                                 .OfType<RadioButton>()
                                                 .FirstOrDefault(rb => rb.Checked)
                                                 .Tag.ToString(),
                                             out IcapsServer);
            if (IcapsServer)
            {
                //SocketDispatcher _icaps_socket = new SocketDispatcher();
                isconn = await SocketDispatcher.StartServer();
            }
            server_check.Change = isconn;
            server_check.Visible = true;
            progressBar1.Value = 50;

            progress_msg.Text = "等待 Modbus 連線...";
            progressBar1.Value = 65;
            ModbusTCPDispatcher.Enable = true;
            isconn = await ModbusTCPDispatcher.CheckConnect();
            sensor_check.Change = isconn;
            sensor_check.Visible = true;
            if (!isconn) Env.SensorModbusTcp = modbus_temp;
            progressBar1.Value = 75;


            progress_msg.Text = "等待 PMC API 連線...";
            progressBar1.Value = 90;
            await Task.Delay(1000);
            progressBar1.Value = 100;

            progress_msg.Text = "已完成";            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            KukaParm.BindArea = KukaAreaModel.Find(comboBox1.Text, KukaParm.KukaAreaModels);
        }
    }
}
