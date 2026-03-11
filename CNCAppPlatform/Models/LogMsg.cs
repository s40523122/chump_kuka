using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chump_kuka.Models
{
    public class LogMsg
    {
        public int ID { get; set; } = 0;
        public string Message { get; set; }
        public string Status { get; set; }
        public string Section { get; set; }
        public string CreateDate { get; set; }
        private string Method { get; set; }

        public LogMsg(int id, string message, string status, string section, string method = "")
        {
            ID = id;
            Message = message;
            Status = status;
            Section = section;
            Method = method;
            CreateDate = DateTime.Now.ToString(@"MM/dd HH:mm:ss");
        }

        public string GetMethod() => Method;
        
    }
}
