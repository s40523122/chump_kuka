/* 
 * Log - Log 訊息管理
 *
 * 這是一個靜態類別(static class)，用來處理全域 Log 訊息。
 * 當訊息新增時，透過 LogChanged 事件通知所有訂閱者 (例如 UI) 來更新資訊。
 * 注意，更新訊息時，會直接覆蓋先前消息!
 * 
 * - 主要功能:
 *   1. 當設定變更時，會觸發 LogChanged 事件，以便 UI 或其他物件即時更新。
 *   2. 使用 static 來確保全域唯一。
 * 
 * - 使用方式:
 *   上傳時:
       Log.Append("Log 訊息", "Log 狀態", "發布 Log 的程序");
 *   讀取時:  
       Log.LogChanged += LogChanged;
 
       private void LogChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
       {
           List<string> log_info = Log.LogInfo;
       }
 */

using CefSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using WebSocketSharp;

namespace Chump_kuka
{
    public static class Log 
    {
        private static int _add_index = 1;      // 已記錄的 Log 數量
        private static string _filter_status = "";      // 紀錄當前篩選標籤名稱

        public static BindingList<LogMsg> LogData = new BindingList<LogMsg>();      // Log 列表
        public static BindingList<LogMsg> FilterData = new BindingList<LogMsg>();      // Log 列表

        public static void Append(string message, string status, string section)
        {
            LogMsg new_msg = new LogMsg(_add_index++, message, status, section);
            LogData.Add(new_msg);

            // 更新篩選資料
            if (status == _filter_status)
            {
                FilterData.Add(new_msg);
            }
        }

        /// <summary>
        /// 利用 Status 篩選資料，並記錄於 FilterData
        /// </summary>
        /// <param name="filter_status"></param>
        public static void FilterStatus(string filter_status)
        {
            _filter_status = filter_status;
            var aa = LogData.Where(c => c.Status == filter_status).ToList();
            FilterData = new BindingList<LogMsg>(aa);
        }

        public class LogMsg
        {
            public int ID { get; set; } = 0;
            public string Message { get; set; }
            public string Status { get; set; }
            public string Section { get; set; }
            public string CreateDate { get; set; }

            public LogMsg(int id, string message, string status, string section)
            {
                ID = id;
                Message = message;
                Status = status;
                Section = section;
                CreateDate = DateTime.Now.ToString(@"MM/dd HH:mm:ss");
            }
        }
    }
}
