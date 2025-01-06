using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chump_kuka
{
    public class ControlClickEventArgs : EventArgs
    {
        public string ControlName { get; }
        public object Control { get; }

        public ControlClickEventArgs(string controlName, object control)
        {
            ControlName = controlName;
            Control = control;
        }
    }
}
