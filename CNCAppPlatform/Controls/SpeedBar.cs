using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Net.Http.Headers;
using System.Net.Http;

namespace Chump_kuka
{
    public partial class SpeedBar : Panel
    {
        [Description("是否允許雙擊後調整指針百分比"), Category("自訂值")]
        public bool IsAdjustment
        {
            get { return _isAdjustment; }
            set
            {
                _isAdjustment = value;
                if (_isAdjustment) DoubleClick += SpeedBar_DoubleClick;
                else DoubleClick -= SpeedBar_DoubleClick;
            }
        }
        private bool _isAdjustment = false;

        private Color ringColor = Color.Green;
        [Description("圓環顏色"), Category("自訂值")]
        public Color RingColor
        {
            get { return ringColor; }
            set
            {
                ringColor = value;
                Invalidate();
            }
        }

        private int nowPercentage = 0;
        [Description("指針百分比"), Category("自訂值")]
        public int NowPercentage
        {
            get { return nowPercentage; }
            set
            {
                if (value >= 0 && value <= 100)
                {
                    nowPercentage = value;
                    Invalidate();
                }
                else
                {
                    throw new ArgumentException("Percentage must be between 0 and 100.");
                }
            }
        }

        private int _ratioPinLength = 0;
        [Description("指針長度比例"), Category("自訂值")]
        public int RatioPinLength
        {
            get { return _ratioPinLength; }
            set
            {
                    _ratioPinLength = value;
                    Invalidate();
            }
        }

        private int _ratioInnerCircle = 0;
        [Description("指針轉軸尺寸比例"), Category("自訂值")]
        public int RatioInnerCircle
        {
            get { return _ratioInnerCircle; }
            set
            {
                _ratioInnerCircle = value;
                Invalidate();
            }
        }

        private int _ratioLabelHeight = 0;
        [Description("指針轉軸尺寸比例"), Category("自訂值")]
        public int RatioLabelHeight
        {
            get { return _ratioLabelHeight; }
            set
            {
                _ratioLabelHeight = value;
                Invalidate();
            }
        }

        private int _percentageSafe = 0;
        [Description("安全值百分比"), Category("自訂值")]
        public int PercentageSafe
        {
            get { return _percentageSafe; }
            set
            {
                if (value >= 0 && value <= 100)
                {
                    _percentageSafe = value;
                    Invalidate();
                }
                else
                {
                    throw new ArgumentException("Percentage must be between 0 and 100.");
                }
            }
        }

        private int _percentageWarn = 0;
        [Description("警告值百分比"), Category("自訂值")]
        public int PercentageWarn
        {
            get { return _percentageWarn; }
            set
            {
                if (value >= 0 && value <= 100)
                {
                    _percentageWarn = value;
                    Invalidate();
                }
                else
                {
                    throw new ArgumentException("Percentage must be between 0 and 100.");
                }
            }
        }

        [Description("警告反轉"), Category("自訂值")]
        public bool Reverse
        {
            get { return _reverse; }
            set
            {
                _reverse = value;
                if (value)
                {
                    LEFT_COLOR = RED;
                    LEFT_BORDER = RED_BORDER;
                    RIGHT_COLOR = GREEN;
                    RIGHT_BORDER = GREEN_BORDER;
                }
                else
                {
                    LEFT_COLOR = GREEN;
                    LEFT_BORDER = GREEN_BORDER;
                    RIGHT_COLOR = RED;
                    RIGHT_BORDER = RED_BORDER;
                }

                Invalidate();
            }
        }
        private bool _reverse = false;

        static Color GREEN = Color.FromArgb(112, 173, 71);
        static Color GREEN_BORDER = Color.FromArgb(97, 150, 60);
        static Color YELLOW = Color.FromArgb(255, 192, 0);
        static Color YELLOW_BORDER = Color.FromArgb(255, 169, 0);
        static Color RED = Color.FromArgb(255, 0, 0);
        static Color RED_BORDER = Color.FromArgb(231, 0, 0);

        Color LEFT_COLOR = GREEN;
        Color LEFT_BORDER = GREEN_BORDER;
        Color MIDDLE_COLOR = YELLOW;
        Color MIDDLE_BORDER = YELLOW_BORDER;
        Color RIGHT_COLOR = RED;
        Color RIGHT_BORDER = RED_BORDER;
        //Color Disable_Color = Color.DarkGray;

        private bool _isMove = false;

        public SpeedBar()
        {
            this.ResizeRedraw = true; // 當控制項大小改變時重繪
            this.DoubleBuffered = true; // 啟用雙緩衝以減少閃爍
            SizeChanged += ProgressRingChart_SizeChanged;
            //DoubleClick += SpeedBar_DoubleClick;
        }

        private void SpeedBar_DoubleClick(object sender, EventArgs e)
        {
            if (_isMove)
            {
                MouseMove -= PercentagePieControl_MouseMove;
            }
            else
            {
                MouseMove += PercentagePieControl_MouseMove;
            }
            _isMove = !_isMove;
        }

        private void ProgressRingChart_SizeChanged(object sender, EventArgs e)
        {
            int small_size = Math.Min(Width, Height);
            Size = new Size(small_size, small_size);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (GraphicsPath path = new GraphicsPath())
            {
                Rectangle rect = ClientRectangle;

                path.AddEllipse(rect);
                this.Region = new Region(path);

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                DrawPie(e.Graphics);
            }

            //e.Graphics.Dispose();
        }

        private void DrawArc(Graphics g, Rectangle rect, Color bodyColor, Color borderColor, int startAngle, int swipeAngle, int offset)
        {
            rect.Inflate(-offset, -offset);
            using (SolidBrush brush = new SolidBrush(bodyColor))        // Body
            {
                g.FillPie(brush, rect, startAngle, swipeAngle);
            }

            rect.Inflate(offset - 1, offset - 1);
            using (SolidBrush brush = new SolidBrush(borderColor))        // Border
            {
                g.DrawPie(new Pen(brush, 4), rect, startAngle, swipeAngle);
                rect.Inflate(-offset * 11, -offset * 11);
                g.DrawPie(new Pen(brush, 4), rect, startAngle, swipeAngle);
            }
        }

        private void DrawPie(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int offset = Math.Max(ClientSize.Width / 65, 1);
            int diameter = ClientSize.Width - offset;
            int startAngle = 150;
            int endAngle = 390;
            int safeAngle = (int)(240 * ((float)_percentageSafe / 100));
            int warnAngle = (int)(240 * ((float)_percentageWarn / 100)) - safeAngle;
            int nowAngle = (int)(240 * ((float)nowPercentage / 100)) - 120;
            int dangerAngle;

            Rectangle rect = new Rectangle((Width - diameter) / 2, (Height - diameter) / 2, diameter, diameter);

            //using (SolidBrush brush = new SolidBrush(Disable_Color))
            //{
            //    g.SmoothingMode = SmoothingMode.AntiAlias;
            //    g.FillEllipse(brush, rect);
            //}

            DrawArc(g, rect, LEFT_COLOR, LEFT_BORDER, startAngle, safeAngle, offset);
            startAngle += safeAngle;
            DrawArc(g, rect, MIDDLE_COLOR, MIDDLE_BORDER, startAngle, warnAngle, offset);
            startAngle += warnAngle;
            dangerAngle = endAngle - startAngle;
            DrawArc(g, rect, RIGHT_COLOR, RIGHT_BORDER, startAngle, dangerAngle, offset);

            rect.Inflate(-offset * 12 - 1, -offset * 12 - 1);
            using (SolidBrush brush = new SolidBrush(BackColor))        // 內圈
            {
                //g.SmoothingMode = SmoothingMode.AntiAlias;
                g.FillEllipse(brush, rect);
            }

            rect.Inflate(-offset * 5, -offset * 5);
            using (SolidBrush brush = new SolidBrush(RingColor))    // 內框線
            {
                //g.SmoothingMode = SmoothingMode.AntiAlias;
                g.DrawArc(new Pen(brush, 4), rect, 150, 240);
            }

            // 三角形頂點
            Point[] points = new Point[]
            {
            new Point(-offset*3, 0),
            new Point(0, -offset * _ratioPinLength),
            new Point(offset*3, 0)
            };

            rect.Inflate(-offset * _ratioInnerCircle, -offset * _ratioInnerCircle);
            g.FillEllipse(Brushes.Black, rect);

            // 保存當前 Graphics
            Matrix originalTransform = g.Transform.Clone();

            // 旋轉畫布
            g.TranslateTransform(Width/2, Height/2);
            Matrix rotationMatrix = new Matrix();
            rotationMatrix.Rotate(nowAngle);
            g.MultiplyTransform(rotationMatrix);

            // 畫三角形
            g.FillPolygon(Brushes.Black, points);

            // 恢復旋轉
            g.Transform = originalTransform;

            string percentageText = nowPercentage.ToString() + "%";
            SizeF textSize = g.MeasureString(percentageText, Font);

            PointF textLocation = new PointF(rect.X + rect.Width / 2 - textSize.Width / 2, Height - (offset * _ratioLabelHeight));
            g.DrawString(percentageText, Font, new SolidBrush(ForeColor), textLocation);
        }

        private void PercentagePieControl_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePosition = Control.MousePosition;
            int pos = PointToClient(mousePosition).X;
            //label1.Text = percentage.ToString();
            nowPercentage = pos * 100 / Width;
            //Refresh();
            Invalidate();
        }
    }
}
