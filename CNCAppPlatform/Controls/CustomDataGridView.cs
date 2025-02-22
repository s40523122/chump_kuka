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

    /// <summary>
    /// 初始化 CustomDataGridView，並建立指定的欄位。
    /// </summary>
    /// <param name="columns">欄位名稱列表</param>
    public CustomDataGridView()
    {
        AllowUserToAddRows = false;
        //InitializeColumns(columns);
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
