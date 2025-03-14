using Chump_kuka;
using Chump_kuka.Controls;
using CookComputing.XmlRpc;
using iCAPS;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Xml.Linq;

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
    private static CarryNode _start_node = new CarryNode();
    private static CarryNode _goal_node = new CarryNode();
    private static string _robot_status_feedback_time = "--";
    private static JArray _robot_status_infos = new JArray();
    private static List<KukaAreaModel> _kuka_area_models = new List<KukaAreaModel>();
    private static KukaAreaModel _bind_area = null;

    // public static List<KukaAreaControl> AreaControls = new List<KukaAreaControl>();     // 已記錄的區域控制項

    
    public static event PropertyChangedEventHandler RobotStatusChanged;
    public static event PropertyChangedEventHandler AreaChanged;
    public static event PropertyChangedEventHandler NodeStatusChanged;
    public static event PropertyChangedEventHandler CarryChanged;


    public static KukaAreaModel BindArea
    {
        get => _bind_area;
        set
        {
            Chump_kuka.Env.BindAreaName = value.AreaName;
            _bind_area = value;
        }
    }


    public static CarryNode StartNode       // 手動派車起點
    {
        get => _start_node;
        set
        {
            if (_start_node != value)
            {
                _start_node = value;
                OnCarryChanged(nameof(StartNode));
            }
        }
    }
    public static CarryNode GoalNode      // 手動派車終點
    {
        get => _goal_node;
        set
        {
            if (_goal_node != value)
            {
                _goal_node = value;
                OnCarryChanged(nameof(GoalNode));
            }
        }
    }

    public static string RobotStatusFeedbackTime
    {
        get => _robot_status_feedback_time;
        set
        {
            if (!_robot_status_feedback_time.Equals(value))
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
            if (!_robot_status_infos.SequenceEqual(value)) 
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
            if (KukaAreaModel.CompareData(_kuka_area_models, value)) return;
            
            _kuka_area_models = value;
            //if (_kuka_area_models[0].NodeList.Count != 0)
            //    AreaChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(KukaAreaModels)));
            
            foreach (KukaAreaModel model in _kuka_area_models)
            {
                model.AreaChanged += (sender, e) =>
                {
                    AreaChanged?.Invoke(sender, e);  // sender 直接就是 Model
                };

                // 訂閱模型的 PropertyChanged 事件，並轉發給 ModelManager 的事件
                model.NodeStatusChanged += (sender, e) =>
                {
                    NodeStatusChanged?.Invoke(sender, e);  // sender 直接就是 Model
                };
                
            }
        }
    }
    private static void OnRobotChanged(string propertyName)
    {
        RobotStatusChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
    }

    private static void OnCarryChanged(string propertyName)
    {
        CarryChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
    }
}

public class KukaAreaModel
{
    public event PropertyChangedEventHandler NodeStatusChanged;
    public event PropertyChangedEventHandler AreaChanged;

    private List<int> _node_status = new List<int>();
    private List<string> _node_list = new List<string>();

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
    public List<string> NodeList
    {
        get => _node_list;
        set
        {
            if (!_node_list.SequenceEqual(value))
            {
                _node_list = value;
                AreaChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NodeList)));
            }
        }
    }

    /// <summary>
    /// 貨架狀態 {0: 無貨架, 1: 空貨架, 2: 滿貨架}
    /// </summary>
    public List<int> NodeStatus 
    { 
        get => _node_status;
        set
        {
            if (!_node_status.SequenceEqual(value))
            {
                _node_status = value;
                NodeStatusChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NodeStatus)));
            }
        }
    }


    public static List<int> RecordNodeStatus { get; set; }

    public KukaAreaModel(JObject json_object = null)
    {
        if (json_object == null) return;
                
        AreaCode = json_object["areaCode"].ToString();
        AreaName = json_object["areaName"].ToString();
        NodeList = json_object["nodeList"].ToObject<List<string>>();      // 集合查詢到的區域代碼為列表
    }

    // public object Clone() => new KukaAreaModel { AreaCode = this.AreaCode, AreaName = this.AreaName, AreaType = this.AreaType, NodeList = this.NodeList };

    /// <summary>
    /// 找尋列表中符合區域名稱的模型
    /// </summary>
    /// <param name="target_area"></param>
    /// <param name="areas"></param>
    /// <returns></returns>
    public static KukaAreaModel Find(string target_area, List<KukaAreaModel> areas) => areas.FirstOrDefault(area => area.AreaName == target_area);

    public static bool CompareData(List<KukaAreaModel> sourceData, List<KukaAreaModel> targetData) => sourceData.Select(m => m.AreaName).SequenceEqual(targetData.Select(m => m.AreaName));

    public string GetTaskNode()
    {
        List<int> current_status = NodeStatus;

        if (RecordNodeStatus == null)
        {
            RecordNodeStatus = current_status;
            return "";
        }

        if (RecordNodeStatus.SequenceEqual(current_status)) 
        {
            MsgBox.Show("沒有變化", "區域貨架異常");
            return "";
        }

        var rules = new Dictionary<(int, int), int>
        {
            { (0, 0), 0 },      // 無變化
            { (0, 1), 2 },      // 錯誤
            { (0, 2), 0 },      // 貨架進站
            { (1, 0), 2 },      // 錯誤
            { (1, 1), 0 },      // 無變化
            { (1, 2), 1 },      // 入貨
            { (2, 0), 0 },      // 貨架出站
            { (2, 1), 0 },      // 取貨
            { (2, 2), 0 }       // 無變化
        };

        List<int> result = new List<int>();

        for (int i = 0; i < current_status.Count; i++)
        {
            if (rules.TryGetValue((RecordNodeStatus[i], current_status[i]), out int value))
                result.Add(value);
            else
                result.Add(0);
        }

        RecordNodeStatus = current_status;

        if (result.Contains(2) || result.Count(n => n == 1) >= 2)
        {
            MsgBox.Show("資料異常", "區域貨架異常");
            return "";
        }
        else if (result.Contains(1))
        {
            return NodeList[result.IndexOf(1)];
        }

        
        return "";
        //Console.WriteLine("不同的索引：" + string.Join(", ", diffIndexes));
    }
}
public class CarryNode
{
    public string Code { get; set; }
    public string Type { get; set; }
    public string Name { get; set; } = "null";

    public void Exchange(CarryNode node1, CarryNode node2) 
    { 
        // TODO 確認是否有指標功能
    }
}