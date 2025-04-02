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
        private ListBox listBox;
        private Button btnMoveUp, btnMoveDown, btnOK;
        public List<string> SortedItems { get; private set; }

        public SortableListForm(List<string> items)
        {
            this.Text = "排序清單";
            this.Size = new System.Drawing.Size(300, 400);

            listBox = new ListBox { Dock = DockStyle.Top, Height = 250 };
            listBox.Items.AddRange(items.ToArray());

            btnMoveUp = new Button { Text = "上移", Dock = DockStyle.Top };
            btnMoveUp.Click += (s, e) => MoveItem(-1);

            btnMoveDown = new Button { Text = "下移", Dock = DockStyle.Top };
            btnMoveDown.Click += (s, e) => MoveItem(1);

            btnOK = new Button { Text = "確定", Dock = DockStyle.Bottom };
            btnOK.Click += (s, e) => { SortedItems = listBox.Items.Cast<string>().ToList(); this.DialogResult = DialogResult.OK; this.Close(); };

            this.Controls.Add(btnOK);
            this.Controls.Add(btnMoveDown);
            this.Controls.Add(btnMoveUp);
            this.Controls.Add(listBox);
        }

        private void MoveItem(int direction)
        {
            if (listBox.SelectedItem == null || listBox.SelectedIndex < 0)
                return;

            int newIndex = listBox.SelectedIndex + direction;
            if (newIndex < 0 || newIndex >= listBox.Items.Count)
                return;

            object selectedItem = listBox.SelectedItem;
            listBox.Items.RemoveAt(listBox.SelectedIndex);
            listBox.Items.Insert(newIndex, selectedItem);
            listBox.SelectedIndex = newIndex;
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
