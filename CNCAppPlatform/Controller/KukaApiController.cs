using Chump_kuka.Controller;
using Chump_kuka.Dispatchers;
using iCAPS;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chump_kuka
{
    internal class KukaApiController
    {
        private static bool _conn = false;
        private static KukaApiDispatcher _api_task;

        // 移動至CarryTask public static event PropertyChangedEventHandler CarryTaskPub;

        public static async Task<bool> StartListen(string url)
        {
            return await HttpListenerDispatcher.StartKukaListener(url);
        }

        public static async Task<bool> ConnectAndCheck(string url)
        {
            if (!_conn)
            {
                _api_task = new KukaApiDispatcher(url);
                _conn = await _api_task.Start();
            }
            return _conn;
        }

        /// <summary>
        /// 將機器人狀態查詢請求加入 API 等待列表
        /// </summary>
        public static void GetRobotStatus()
        {
            if (!_conn) return;
            _api_task.AppendRobotStatusTask();
        }

        /// <summary>
        /// 將區域狀態查詢請求加入 API 等待列表
        /// </summary>
        public static void GetAreaInfo()
        {
            _api_task?.AppendAreaTask();
            _api_task?.AppendNodesTask();
        }

        /// <summary>
        /// 將派車任務請求加入 API 等待列表
        /// </summary>
        public static void PubCarryTask()
        {
            _api_task.AppendCarryTask();        // 建立搬運任務

            // CarryTaskPub?.Invoke(null, null);       // 已建立任務，更新 UI
        }
    }
}
