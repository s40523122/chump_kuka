/*
 * CustomDataGridView - 可擴展的 DataGridView，支援 CSV 資料存取
 * 
 * 功能介紹：
 * 1. 透過 List<string> 初始化欄位。
 * 2. 可動態新增資料列 (AddRow)。
 * 3. 可修改特定儲存格內容 (UpdateCell)。
 * 4. 支援 CSV 檔案的自動儲存與讀取。
 * 5. 透過 AutoSave 屬性決定是否自動儲存變更。
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;

public class CustomDataGridView : DataGridView
{
    private bool autoSave = false; // 是否自動儲存 CSV
    private string csvFilePath = "data.csv"; // 預設 CSV 檔案名稱
    private string[] columns = new string[] {};
    private float[] columnWidthRatios = new float[] {}; // 欄位寬度比例

    [Description("資料存放 CSV 位置"), Category("自訂值")]
    public string CsvFilePath
    {
        get => csvFilePath;
        set => csvFilePath = value;
    }

    [Description("是否在資料變更時自動儲存至 CSV"), Category("自訂值")]
    public bool AutoSave
    {
        get => autoSave;
        set => autoSave = value;
    }

    [Description("自定義欄位列表"), Category("自訂值")]
    public string[] UserColumns
    {
        get => columns;
        set
        {
            columns = value;
            InitializeColumns(columns);
        }
    }

    [Description("設定或取得欄位寬度比例。\n每個數值代表對應欄位所佔的相對寬度。"), Category("自訂值")]
    public float[] UserColumnWidthRatios
    {
        get => columnWidthRatios;
        set
        {
            //if (value.Length != columns.Length)
            //{
            //    MessageBox.Show("設定值與欄位數量不符合");
            //    return;
            //}
            columnWidthRatios = value;
            AdjustColumnWidths();
        }
    }

    /// <summary>
    /// 初始化 CustomDataGridView，並建立指定的欄位。
    /// </summary>
    /// <param name="columns">欄位名稱列表</param>
    public CustomDataGridView()
    {
        AllowUserToAddRows = false;
        //InitializeColumns(columns);
        Resize += CustomDataGridView_Resize;

        //DataSource = new List<string[]>()
        //{
        //    new string[] {"Test1", "INFO", "Source" },
        //    new string[] {"Test2", "NOTICE", "Source2" }
        //};
    }

    private void CustomDataGridView_Resize(object sender, EventArgs e)
    {
        AdjustColumnWidths();
    }

    /// <summary>
    /// 初始化欄位。
    /// </summary>
    /// <param name="columns">欄位名稱列表</param>
    private void InitializeColumns(string[] columns)
    {
        Columns.Clear();
        foreach (var col in columns)
        {
            Columns.Add(col, col);
        }
    }

    /// <summary>
    /// 調整欄位寬度，使其符合設定的比例。
    /// </summary>
    private void AdjustColumnWidths()
    {
        if (Columns.Count == 0 || columnWidthRatios.Length < Columns.Count) return;

        int totalWidth = ClientSize.Width;
        for (int i = 0; i < Columns.Count; i++)
        {
            Columns[i].Width = (int)(totalWidth * columnWidthRatios[i]);
        }
    }

    /// <summary>
    /// 新增一行資料。
    /// </summary>
    /// <param name="rowData">該行的資料內容</param>
    public void AddRow(List<string> rowData)
    {
        if (rowData.Count != ColumnCount) return; // 確保欄位數量相符
        Rows.Add(rowData.ToArray());
        if (AutoSave) SaveToCsv();
    }

    /// <summary>
    /// 修改指定儲存格的內容。
    /// </summary>
    /// <param name="rowIndex">行索引</param>
    /// <param name="colIndex">列索引</param>
    /// <param name="newValue">新值</param>
    public void UpdateCell(int rowIndex, int colIndex, string newValue)
    {
        if (rowIndex < 0 || rowIndex >= Rows.Count ||
            colIndex < 0 || colIndex >= ColumnCount) return;

        Rows[rowIndex].Cells[colIndex].Value = newValue;
        if (AutoSave) SaveToCsv();
    }

    /// <summary>
    /// 將目前的資料儲存至 CSV。
    /// </summary>
    public void SaveToCsv()
    {
        using (StreamWriter writer = new StreamWriter(csvFilePath))
        {
            // 儲存標題列
            writer.WriteLine(string.Join(",", Columns.Cast<DataGridViewColumn>().Select(c => c.HeaderText)));

            // 儲存資料列
            foreach (DataGridViewRow row in Rows)
            {
                if (!row.IsNewRow)
                {
                    writer.WriteLine(string.Join(",", row.Cells.Cast<DataGridViewCell>().Select(c => c.Value?.ToString() ?? "")));
                }
            }
        }
    }

    /// <summary>
    /// 從 CSV 讀取資料，並載入至表格。
    /// </summary>
    public void LoadFromCsv()
    {
        if (!File.Exists(csvFilePath)) return;

        using (StreamReader reader = new StreamReader(csvFilePath))
        {
            var headers = reader.ReadLine()?.Split(',');
            if (headers == null) return;

            InitializeColumns(headers.ToArray()); // 初始化欄位

            while (!reader.EndOfStream)
            {
                var rowData = reader.ReadLine()?.Split(',').ToList();
                if (rowData != null) AddRow(rowData);
            }
        }
    }
}
