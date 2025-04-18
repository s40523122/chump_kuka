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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    public partial class AreaBindingForm : Form
    {
        private List<string> _areas;
        private List<AreaBinding> _bindings = new List<AreaBinding>();

        private string _selectedSource = null;
        private string _selectedTarget = null;

        public List<AreaBinding> Bindings
        {
            get { return _bindings; }
        }

        public AreaBindingForm(string[] areas)
        {
            InitializeComponent();
            _areas = areas.ToList();
            PopulateAreaList();
        }

        // 將所有區域轉為按鈕顯示
        private void PopulateAreaList()
        {
            foreach (var area in _areas)
            {
                var btn = new Button();
                btn.Text = area;
                btn.Width = 80;
                btn.Height = 30;
                btn.Margin = new Padding(5);
                btn.Click += AreaButton_Click;
                flowLayoutPanelAreas.Controls.Add(btn);
            }
        }

        // 點選區域按鈕時處理
        private void AreaButton_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            string area = btn.Text;

            // 取消選取：若再次點選已選的項目則取消
            if (_selectedSource == area)
            {
                _selectedSource = null;
            }
            else if (_selectedTarget == area)
            {
                _selectedTarget = null;
            }
            else if (_selectedSource == null)
            {
                _selectedSource = area;
            }
            else if (_selectedTarget == null)
            {
                _selectedTarget = area;
            }

            UpdateSelectedLabel();
        }

        private void btnAddLogic_Click(object sender, EventArgs e)
        {
            if (_selectedSource != null && _selectedTarget != null)
            {
                var binding = new AreaBinding();
                binding.Source = _selectedSource;
                binding.Target = _selectedTarget;
                binding.WaitForCall = false;

                _bindings.Add(binding);
                AddBindingToListView(binding);

                _selectedSource = null;
                _selectedTarget = null;
                UpdateSelectedLabel();
            }
        }

        private void AddBindingToListView(AreaBinding binding)
        {
            var item = new ListViewItem(binding.Source);
            item.SubItems.Add(binding.Target);
            item.SubItems.Add(binding.WaitForCall ? "是" : "否"); // 顯示勾選狀態文字

            item.Checked = binding.WaitForCall; // 維持 CheckBox 顯示
            item.Tag = binding;                // 對應到資料物件
            listViewBindings.Items.Add(item);
        }

        // 更新畫面上「目前選擇」提示
        private void UpdateSelectedLabel()
        {
            lblSelected.Text = "目前選擇：" + (_selectedSource ?? "-") + " → " + (_selectedTarget ?? "-");
        }

        // 單筆勾選等待呼叫：同步到對應資料物件
        private void listViewBindings_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            var binding = e.Item.Tag as AreaBinding;
            if (binding != null)
            {
                binding.WaitForCall = e.Item.Checked;
                e.Item.SubItems[2].Text = binding.WaitForCall ? "是" : "否"; // 更新欄位顯示
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        public class AreaBinding
        {
            public string Source { get; set; }      // 綁定來源區域
            public string Target { get; set; }      // 綁定目標區域
            public bool WaitForCall { get; set; }   // 是否等待呼叫（由使用者決定）
        }
    }
}
