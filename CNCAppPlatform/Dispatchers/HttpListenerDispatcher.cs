using iCAPS.Managers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using Chump_kuka.Services.Managers;
using Chump_kuka.Controller;
using System.Messaging;
using Chump_kuka;
using LiveCharts.Wpf;
using Chump_kuka.Services;
using Chump_kuka.Models.Msgs;
using static Chump_kuka.KukaModel;

namespace Chump_kuka.Dispatchers
{
    public class HttpListenerDispatcher
    {
        private HttpListenerManager _kuka_listener;

        public event Action<MissionStatusMsg> HeardKMRES;     // 接收 KMRES 回應並前處理事件
        //private IKukaService _kuka_service;

        public HttpListenerDispatcher()
        {
        }

        public async Task<bool> StartKukaListener(string url)
        {
            if (_kuka_listener != null && _kuka_listener.IsRunning) return true;

            _kuka_listener = new HttpListenerManager(url);
            bool success = _kuka_listener.Start();
            if (success)
            {
                await _kuka_listener.WaitForServerToStartAsync();
                _kuka_listener.MessageReceived += _kuka_listener_MessageReceived;
            }

            return _kuka_listener.IsRunning;
        }

        

        //static string area_code = "";      // 任務起始區域編碼

        private void _kuka_listener_MessageReceived(object sender, HttpMessageEventArgs e)
        {            
            // 將 JSON 解析為 JObject
            JObject jsonObj = JObject.Parse(e.Message);

            //// 設定一個映射字典，鍵是原來的值，值是要替換的值
            //var valueMapping = new Dictionary<string, string>
            //    {
            //        { "MOVE_BEGIN", "开始移动" },
            //        { "ARRIVED", "到达任务节点" },
            //        { "UP_CONTAINER", "顶升完成" },
            //        { "DOWN_CONTAINER", "放下完成" },
            //        { "COMPLETED", "任务完成" },
            //        { "CANCELED", "任务取消完成" },
            //        { "ERROR", "任务执行报错" }
            //    };

            //// 設定一個鍵名映射字典
            //var keyMapping = new Dictionary<string, string>
            //    {
            //        { "missionCode", "作业id " },
            //        { "viewBoardType", "作业类型 " },
            //        { "containerCode", "容器编号" },
            //        { "currentPosition", "容器当前位置 " },
            //        { "slotCode", "当前所在槽位" },
            //        { "robotId", "执行当前任务的机器人id " },
            //        { "missionStatus", "作业当前状态 " },
            //        { "message", "说明信息" },
            //        { "missionData", "需要上报的定制信息对象" }
            //    };

            // 解析重點資訊
            string task_status = jsonObj["missionStatus"].ToString();
            string mission_code = jsonObj["missionCode"].ToString();
            string remark = jsonObj["message"].ToString();

            // 透過事件傳遞資訊
            MissionStatusMsg msg = new MissionStatusMsg(mission_code, task_status, remark);
            this.HeardKMRES.Invoke(msg);

            // 強制回應完成訊息
            string response_json = "{ \"code\": \"0\", \"message\": \"\", \"success\": true, \"data\":[] }";
            _kuka_listener.MessageResponse(e.Context, response_json);
        }

        public void ManualHeardEvent(string mission_code, string start_area_code, KukaMissionStep step)
        {
            EventBus.PublishMissionStepChanged(new HeardEventArgs(mission_code, start_area_code, step));
        }
        
    }

    public class HeardEventArgs : EventArgs
    {
        public string MissionCode { get; private set; }
        public string StartAreaCode { get; set; }

        public KukaMissionStep Step { get; set; }

        public HeardEventArgs(string mission_code, string start_area_code, KukaMissionStep step)
        {
            MissionCode = mission_code;
            StartAreaCode = start_area_code;
            Step = step;
        }
    }
}
