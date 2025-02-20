using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chump_kuka.Controls
{
    public partial class SidePanel1 : UserControl
    {
        private bool isExpanded = false; // 側邊欄展開狀態
        private float MaxSidebarWidth = 200; // 側邊欄最大寬度
        private const int AnimationSpeed = 30; // 動畫速度 (越小越快)
        private int ParentWidth = 200;
        public bool Start
        {
            set 
            { 
                SidebarTimer.Start(); 
            }
        }
        

        public SidePanel1()
        {
            InitializeComponent();
            Load += SidePanel_Load;
            Resize += SidePanel_Resize;
            
        }

        private void SidePanel_Resize(object sender, EventArgs e)
        {
            if (ParentWidth != Parent.Width)
            {
                ParentWidth = Parent.Width;
                MaxSidebarWidth = ParentWidth * 0.3f;

                this.Location = new Point(ParentWidth, 0);
                this.Width = (int)MaxSidebarWidth;
                this.Height = Parent.Height; // 讓側邊欄高度等於視窗高度
            }
        }

        private void SidePanel_Load(object sender, EventArgs e)
        {
            InitializeSidebar();
            this.BringToFront(); // 確保在最上層
        }

        private void InitializeSidebar()
        {
            // 設定 SidePanel 初始狀態
            this.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            
            
        }

        private void SidebarTimer_Tick(object sender, EventArgs e)
        {
            if (isExpanded)
            {
                // 收回動畫
                if (this.Left <= ParentWidth)
                {
                    this.Left += AnimationSpeed;
                }
                else
                {
                    SidebarTimer.Stop();
                    isExpanded = false;
                }
            }
            else
            {
                // 展開動畫
                if (this.Left > ParentWidth - (int)MaxSidebarWidth)
                {
                    int expectLeft = this.Left - AnimationSpeed;
                    if (expectLeft < ParentWidth - MaxSidebarWidth) this.Left = ParentWidth - (int)MaxSidebarWidth;
                    else this.Left = expectLeft;

                }
                else
                {
                    SidebarTimer.Stop();
                    isExpanded = true;
                }
            }

        }
    }
}
