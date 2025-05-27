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
    public partial class SortableListForm : Form
    {
        private ListBox listBox_leave, listBox_out;
        private Button btnMoveUp, btnMoveDown, btnOK, btnDel, btnBack;
        public List<string> SortedItems { get; private set; }

        public SortableListForm(List<string> items)
        {
            this.Text = "排序清單";
            this.Size = new System.Drawing.Size(300, 400);

            listBox_leave = new ListBox { Dock = DockStyle.Top, Height = 125 };
            listBox_leave.Items.AddRange(items.ToArray());

            listBox_out = new ListBox { Dock = DockStyle.Top, Height = 125 };

            btnMoveUp = new Button { Text = "上移", Dock = DockStyle.Top };
            btnMoveUp.Click += (s, e) => MoveItem(-1);

            btnMoveDown = new Button { Text = "下移", Dock = DockStyle.Top };
            btnMoveDown.Click += (s, e) => MoveItem(1);

            btnDel = new Button { Text = "排除", Dock = DockStyle.Top };
            btnDel.Click += (s, e) => LeaveItem();

            btnBack = new Button { Text = "取消排除", Dock = DockStyle.Top };
            btnBack.Click += (s, e) => BackItem();

            btnOK = new Button { Text = "確定", Dock = DockStyle.Bottom };
            btnOK.Click += (s, e) => { SortedItems = listBox_leave.Items.Cast<string>().ToList(); this.DialogResult = DialogResult.OK; this.Close(); };


            this.Controls.Add(listBox_out);
            this.Controls.Add(btnOK);
            this.Controls.Add(btnBack);
            this.Controls.Add(btnDel);
            this.Controls.Add(btnMoveDown);
            this.Controls.Add(btnMoveUp);
            this.Controls.Add(listBox_leave);
        }

        private void MoveItem(int direction)
        {
            if (listBox_leave.SelectedItem == null || listBox_leave.SelectedIndex < 0)
                return;

            int newIndex = listBox_leave.SelectedIndex + direction;
            if (newIndex < 0 || newIndex >= listBox_leave.Items.Count)
                return;

            object selectedItem = listBox_leave.SelectedItem;
            listBox_leave.Items.RemoveAt(listBox_leave.SelectedIndex);
            listBox_leave.Items.Insert(newIndex, selectedItem);
            listBox_leave.SelectedIndex = newIndex;
        }

        private void LeaveItem()
        {
            if (listBox_leave.SelectedItem == null || listBox_leave.SelectedIndex < 0)
                return;

            object selectedItem = listBox_leave.SelectedItem;
            listBox_leave.Items.RemoveAt(listBox_leave.SelectedIndex);
            listBox_out.Items.Insert(0, selectedItem);
        }

        private void BackItem()
        {
            if (listBox_out.SelectedItem == null || listBox_out.SelectedIndex < 0)
                return;

            object selectedItem = listBox_out.SelectedItem;
            listBox_out.Items.RemoveAt(listBox_out.SelectedIndex);
            listBox_leave.Items.Insert(0, selectedItem);
        }

        public static List<string> ShowDialogAndSort(List<string> items)
        {
            using (var form = new SortableListForm(items))
            {
                return form.ShowDialog() == DialogResult.OK ? form.SortedItems : items;
            }
        }
    }
}
