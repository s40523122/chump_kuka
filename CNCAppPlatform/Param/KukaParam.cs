using Chump_kuka;
using Chump_kuka.Controls;
using CookComputing.XmlRpc;
using iCAPS;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
    private static KukaAreaModel _target_area = null;

    // public static List<KukaAreaControl> AreaControls = new List<KukaAreaControl>();     // 已記錄的區域控制項


    public static event PropertyChangedEventHandler RobotStatusChanged;
    public static event PropertyChangedEventHandler AreaChanged;
    //public static event PropertyChangedEventHandler AreaStatusChanged;
    public static event PropertyChangedEventHandler CarryChanged;
    public static event PropertyChangedEventHandler BindChanged;        // 當綁定區域改變後


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

    //public static string RobotStatusFeedbackTime
    //{
    //    get => _robot_status_feedback_time;
    //    set
    //    {
    //        if (!_robot_status_feedback_time.Equals(value))
    //        {
    //            _robot_status_feedback_time = value;
    //            OnRobotChanged(nameof(RobotStatusFeedbackTime));
    //        }
    //    }
    //}

    public static JArray RobotStatusInfos
    {
        get => _robot_status_infos;
        set
        {
            if (!JToken.DeepEquals(_robot_status_infos, value)) 
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
            bool change = false;

            // 遍歷現有列表資料，將不存在於輸入列表的物件移除，並更新存在物件
            // 當物件存在且修改後，從輸入列表中移除
            foreach (KukaAreaModel model in _kuka_area_models)
            {
                // 判段原始區域列表是否需要增減
                KukaAreaModel find_in_value = value.FirstOrDefault(m => m.AreaCode == model.AreaCode);
                if ( find_in_value == null)
                {
                    _kuka_area_models.Remove(model);
                    change = true;      // 紀錄需更新
                    continue;
                }

                if ( find_in_value.NodeList != null)
                {
                    change |= model.CheckAndUpdate(find_in_value);      // 判定資料內容是否變更
                }

                value.Remove(find_in_value);        // 從輸入列表中移除
            }
            if (value.Count > 0)
            {
                foreach (KukaAreaModel model in value)
                {
                    // 新資料，尚未處理控制項
                    _kuka_area_models.Add(model);
                }
                change = true;
            }

            if (change) 
            {
                AreaChanged?.Invoke(_kuka_area_models, new PropertyChangedEventArgs(nameof(KukaAreaModels)));
            }

            // origin
            //if (KukaAreaModel.CompareData(_kuka_area_models, value)) return;
            
            //_kuka_area_models = value;
            ////if (_kuka_area_models[0].NodeList.Count != 0)
            ////    AreaChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(KukaAreaModels)));
            
            //foreach (KukaAreaModel model in _kuka_area_models)
            //{
            //    model.ModelChanged += (sender, e) =>
            //    {
            //        AreaChanged?.Invoke(sender, e);  // sender 直接就是 Model
            //    };

            //    // 訂閱模型的 PropertyChanged 事件，並轉發給 ModelManager 的事件
            //    model.NodeStatusChanged += (sender, e) =>
            //    {
            //        AreaStatusChanged?.Invoke(sender, e);  // sender 直接就是 Model
            //    };

            //}
            //try
            //{
            //    AreaChanged?.Invoke(null, null);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //}
        }
    }

    public static KukaAreaModel BindAreaModel
    {
        get => _bind_area;
        set
        {
            if (value == null) return;
            if (_bind_area != null && Chump_kuka.Env.BindAreaName == value.AreaName) return;        // 非首次綁定時，跳過資料相同的處理

            Chump_kuka.Env.BindAreaName = value.AreaName;
            _bind_area = value;

            BindChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(BindAreaModel)));
        }
    }

    public static KukaAreaModel TargetAreaModel
    {
        get => _target_area;
        set
        {
            if (value == null) return;
            if (_target_area != null && Chump_kuka.Env.BindAreaName == value.AreaName) return;        // 非首次綁定時，跳過資料相同的處理

            Chump_kuka.Env.TargetAreaName = value.AreaName;
            _target_area = value;
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
    //public event PropertyChangedEventHandler NodeStatusChanged;
    //public event PropertyChangedEventHandler ModelChanged;

    private int[] _node_status;
    private string[] _node_list;

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

    [JsonIgnore]        // 避免序列化循環引用
    public KukaAreaControl ControlUI { get; set; }

    /// <summary>
    /// 點位集合
    /// </summary>
    public string[] NodeList
    {
        get => _node_list;
        set
        {
            if (_node_list == null || !_node_list.SequenceEqual(value))
            {
                _node_list = value;
                //ModelChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NodeList)));

                _node_status = new int[_node_list.Length % 2];
            }
        }
    }

    /// <summary>
    /// 貨架狀態 {0: 無貨架, 1: 空貨架, 2: 滿貨架}
    /// </summary>
    public int[] NodeStatus 
    { 
        get => _node_status;
        set
        {
            if (_node_status == null || !_node_status.SequenceEqual(value))
            {
                _node_status = value;
                // NodeStatusChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NodeStatus)));

                ControlUI?.UpdateContainerImage(value);     // 更新圖片

                //NodeStatusChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NodeStatus)));
            }
        }
    }
    public KukaAreaModel(JObject json_object = null)
    {
        if (json_object == null) return;
                
        AreaCode = json_object["areaCode"].ToString();
        AreaName = json_object["areaName"].ToString();
        NodeList = json_object["nodeList"].ToObject<string[]>();      // 集合查詢到的區域代碼為陣列
    }

    public bool CheckAndUpdate(KukaAreaModel param)
    {
        bool change = false;
        if (AreaName != param.AreaName)
        {
            AreaName = param.AreaName;
            change = true;
        }
        if (AreaType != param.AreaType)
        {
            AreaType = param.AreaType;
            change = true;
        }
        if (!Enumerable.SequenceEqual(NodeList, param.NodeList))
        {
            NodeList = param.NodeList;
            change = true;
        }
        
        return change;
    }

    public void CompareAndUpdate(KukaAreaModel source_model)
    {
        this.AreaName = source_model.AreaName;
        this.AreaType = source_model.AreaType;
        this.NodeList = source_model.NodeList;
        this.NodeStatus = source_model.NodeStatus;
    }

    /// <summary>
    /// 找尋列表中符合區域名稱的模型
    /// </summary>
    /// <param name="target_area"></param>
    /// <param name="areas"></param>
    /// <returns></returns>
    public static KukaAreaModel Find(string target_area_name, List<KukaAreaModel> areas) => areas.FirstOrDefault(area => area.AreaName == target_area_name);

    public static bool CompareData(List<KukaAreaModel> sourceData, List<KukaAreaModel> targetData) => sourceData.Select(m => m.AreaName).SequenceEqual(targetData.Select(m => m.AreaName));

}
public class CarryNode
{
    public string Code { get; set; }
    public string Type { get; set; }
    public string Name { get; set; } = "null";
}