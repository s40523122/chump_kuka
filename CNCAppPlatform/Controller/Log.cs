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

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Chump_kuka
{
    public static class Log 
    {
        public static event PropertyChangedEventHandler LogChanged;

        private static LogMsg _log_info;

        public static LogMsg LogInfo
        {
            get => _log_info;
            set
            {
                _log_info = value;
                OnLogChanged(nameof(LogInfo));
            }
        }

        public static void Append(string message, string status, string section)
        {
            LogInfo = new LogMsg(message, status, section, DateTime.Now);
        }

        private static void OnLogChanged(string propertyName)
        {
            LogChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }

        public class LogMsg
        {
            public int ID { get; set; } = 0;
            public string Message { get; set; }
            public string Status { get; set; }
            public string Section { get; set; }
            public DateTime CreateDate { get; set; }

            public LogMsg(string message, string status, string section, DateTime createDate)
            {
                Message = message;
                Status = status;
                Section = section;
                CreateDate = createDate;
            }
        }
    }
}
