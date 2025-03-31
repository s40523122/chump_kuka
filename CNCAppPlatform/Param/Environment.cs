using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows.Forms;
using iCAPS;

namespace Chump_kuka
{
    public class Env : iCAPS.Env
    {
        private static readonly string layout_path = Path.Combine(Application.StartupPath, "config/layout.ini");        // UI設定檔位址
        
        private static bool? _icaps_server;
        private static string _kuka_api_url;
        private static IPEndPoint _sensor_modbus_tcp;
        private static string _area_name = "";

        public static event PropertyChangedEventHandler EnvChanged;

        public static bool ICapsServer
        {
            get
            {
                if (_icaps_server != null)
                    return _icaps_server.Value;
                string text = INiReader.ReadINIFile(layout_path, "Control", "icaps_server");
                if (text == "") text = "true";
                return bool.Parse(text);
            }
            set
            {
                _icaps_server = value;
                INiReader.WriteINIFile(layout_path, "Control", "icaps_server", value.ToString());
            }
        }

        public static string KukaApiUrl
        {
            get
            {
                string url_txt = INiReader.ReadINIFile(layout_path, "Control", "kuka_api_url");
                // KukaApiController.kuka_api_server = new HttpRequest(url_txt);
                return url_txt;
            }
            set
            {
                _kuka_api_url = value;
                // KukaApiController.kuka_api_server = new HttpRequest(value);
                INiReader.WriteINIFile(layout_path, "Control", "kuka_api_url", value);
            }
        }

        public static IPEndPoint SensorModbusTcp        // IO 模組 Modbus TCP 連線參數
        {
            get
            {
                if (_sensor_modbus_tcp == null)
                {
                    string address = INiReader.ReadINIFile(layout_path, "Control", "sensor_modbus_ip");
                    string port = INiReader.ReadINIFile(layout_path, "Control", "sensor_modbus_port");
                    if (address != "" && port != "") _sensor_modbus_tcp = new IPEndPoint(IPAddress.Parse(address), int.Parse(port));
                }

                return _sensor_modbus_tcp;
            }
            set
            {
                _sensor_modbus_tcp = value;

                INiReader.WriteINIFile(layout_path, "Control", "sensor_modbus_ip", _sensor_modbus_tcp?.Address.ToString());
                INiReader.WriteINIFile(layout_path, "Control", "sensor_modbus_port", _sensor_modbus_tcp?.Port.ToString());
            }
        }

        public static string IcapsServerUdpIp
        {
            get
            {
                string text = INiReader.ReadINIFile(layout_path, "Control", "icaps_server_udp_ip");
                return string.IsNullOrEmpty(text) ? null : text;
            }
            set
            {
                INiReader.WriteINIFile(layout_path, "Control", "icaps_server_udp_ip", value);
            }
        }

        public static string IcapsServerUdpPort
        {
            get
            {
                string text = INiReader.ReadINIFile(layout_path, "Control", "icaps_server_udp_port");
                return string.IsNullOrEmpty(text) ? null : text;
            }
            set
            {
                INiReader.WriteINIFile(layout_path, "Control", "icaps_server_udp_port", value);
            }
        }

        public static string RecordLogTcpPort
        {
            get
            {
                string text = INiReader.ReadINIFile(layout_path, "Control", "record_log_tcp_port");
                return string.IsNullOrEmpty(text) ? null : text;
            }
            set
            {
                INiReader.WriteINIFile(layout_path, "Control", "record_log_tcp_port", value);
            }
        }

        public static string KukaResponseUrl
        {
            get
            {
                string text = INiReader.ReadINIFile(layout_path, "Control", "kuka_response_url");
                return text;
            }
            set
            {
                INiReader.WriteINIFile(layout_path, "Control", "kuka_response_url", value);
            }
        }

        /// <summary>
        /// 綁定區域名稱(僅為字串資訊，供下拉式選單用)
        /// </summary>
        public static string BindAreaName
        {
            get
            {
                // 首次訪問才需要讀取文件，降低文件讀寫頻率
                if (string.IsNullOrEmpty(_area_name))
                    _area_name = INiReader.ReadINIFile(layout_path, "Control", "bind_area_name");

                return _area_name;
            }
            set
            {
                // 如果更新資料同原始，不做任動作
                if (_area_name != value) 
                    _area_name = value;

                EnvChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(BindAreaName)));
                INiReader.WriteINIFile(layout_path, "Control", "bind_area_name", value);
            }
        }
    }
}
