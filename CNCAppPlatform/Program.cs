using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chump_kuka
{
    static class Program
    {
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

        private const int SW_RESTORE = 9;
        private const uint FLASHW_ALL = 3;
        private const uint FLASHW_TIMERNOFG = 12;

        [StructLayout(LayoutKind.Sequential)]
        public struct FLASHWINFO
        {
            public uint cbSize;
            public IntPtr hwnd;
            public uint dwFlags;
            public uint uCount;
            public uint dwTimeout;
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            // 取得目前處理程序的名稱
            Process currentProcess = Process.GetCurrentProcess();
            string processName = currentProcess.ProcessName;

            // 尋找所有同名的處理程序
            Process[] processes = Process.GetProcessesByName(processName);

            // 如果找到超過一個同名的處理程序，表示應用程式已在執行
            if (processes.Length > 1)
            {
                // 尋找已存在的執行個體並使其閃爍
                foreach (Process p in processes)
                {
                    if (p.Id != currentProcess.Id)
                    {
                        // 取得已存在視窗的控制代碼
                        IntPtr hWnd = p.MainWindowHandle; // p 是已存在的 Process

                        FLASHWINFO fInfo = new FLASHWINFO();
                        fInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(fInfo));
                        fInfo.hwnd = hWnd;
                        fInfo.dwFlags = FLASHW_ALL | FLASHW_TIMERNOFG;
                        fInfo.uCount = 3;          // 閃爍 5 次
                        fInfo.dwTimeout = 5;     // 每 5 毫秒閃爍一次

                        FlashWindowEx(ref fInfo);
                        break;
                    }
                }
                // 結束目前的 (新開啟的) 應用程式
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            
        }
    }
}
