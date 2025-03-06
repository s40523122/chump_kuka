using Chump_kuka;
using Chump_kuka.Controls;
using CookComputing.XmlRpc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

/// <summary>
/// KukaParm 類別 (全域設定管理)
/// 
/// 這是一個靜態類別 (static class)，用來存放關於 Kuka api 的全域參數。
/// 當參數變更時，透過 PropertyChanged 事件通知所有訂閱者 (例如 UI) 來更新顯示。
/// 
/// - 主要功能:
///   1. 當設定變更時，會觸發 PropertyChanged 事件，以便 UI 或其他物件即時更新。
///   2. 使用 static 來確保全域唯一，不需要透過 Instance 存取。
/// 
/// - 使用方式:
///   直接透過 `KukaParm.參數名稱` 存取。
/// </summary>

public static class KukaParm
{
    public static event PropertyChangedEventHandler RobotStatusChanged;
    public static event PropertyChangedEventHandler AreaChanged;

    public static string StartNode;     // 手動派車起點
    public static string GoalNode;      // 手動派車終點

    //private static string _volume;
    private static string _robot_status_feedback_time = "--";
    private static JArray _robot_status_infos = new JArray();
    private static List<KukaAreaModel> _kuka_area_models = new List<KukaAreaModel>();

    public static List<KukaAreaControl> AreaControls = new List<KukaAreaControl>();     // 已記錄的區域控制項

    public static string RobotStatusFeedbackTime
    {
        get => _robot_status_feedback_time;
        set
        {
            if (_robot_status_feedback_time != value)
            {
                _robot_status_feedback_time = value;
                OnRobotChanged(nameof(RobotStatusFeedbackTime));
            }
        }
    }

    public static JArray RobotStatusInfos
    {
        get => _robot_status_infos;
        set
        {
            if (_robot_status_infos != value)
            {
                _robot_status_infos = value;
                OnRobotChanged(nameof(RobotStatusInfos));
            }
        }
    }

    public static List<KukaAreaModel> KukaAreaModels
    {
        get => _kuka_area_models;
        set
        {
            if (_kuka_area_models != value)
            {
                _kuka_area_models = value;
                if (_kuka_area_models[0].NodeList.Count != 0) OnAreaChanged(nameof(KukaAreaModels));
            }
        }
    }

    private static void OnRobotChanged(string propertyName)
    {
        RobotStatusChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
    }

    private static void OnAreaChanged(string propertyName)
    {
        AreaChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
    }
}

public class KukaAreaModel : ICloneable
{
    /// <summary>
    /// 區域編碼 ex: area001
    /// </summary>
    public string AreaCode { get; set; }

    /// <summary>
    /// 區域名稱 ex: 加工區
    /// </summary>
    public string AreaName { get; set; }

    /// <summary>
    /// 區域類型 {1: 庫區, 2: 作業區, 3: 暫存區, 4: 緩存區}
    /// </summary>
    public int AreaType { get; set; }

    /// <summary>
    /// 點位集合
    /// </summary>
    public List<string> NodeList { get; set; } = new List<string>();

    /// <summary>
    /// 貨架狀態 {0: 空, 1: 空貨架, 2: 滿貨架}
    /// </summary>
    public List<int> NodeStatus { get; set; } = new List<int>();

    public KukaAreaModel(JObject json_object = null)
    {
        if (json_object == null) return;
                
        AreaCode = json_object["areaCode"].ToString();
        AreaName = json_object["areaName"].ToString();
        NodeList = json_object["nodeList"].ToObject<List<string>>();      // 集合查詢到的區域代碼為列表

    }

    public object Clone() => new KukaAreaModel { AreaCode = this.AreaCode, AreaName = this.AreaName, AreaType = this.AreaType, NodeList = this.NodeList };

    /// <summary>
    /// 找尋列表中符合區域名稱的模型
    /// </summary>
    /// <param name="target_area"></param>
    /// <param name="areas"></param>
    /// <returns></returns>
    public static KukaAreaModel Find(string target_area, List<KukaAreaModel> areas) => areas.FirstOrDefault(area => area.AreaName == target_area);
}