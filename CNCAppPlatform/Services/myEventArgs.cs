using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chump_kuka
{
    public class ContainerClickEventArgs : EventArgs
    {
        public string ContainerName { get; }
        public object Container { get; }

        public ContainerClickEventArgs(string controlName, object control)
        {
            ContainerName = controlName;
            Container = control;
        }
    }
}
