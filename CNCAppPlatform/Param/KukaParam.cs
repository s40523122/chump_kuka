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
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Xml.Linq;
using System.IO;
using System.Collections.ObjectModel;

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

internal static class KukaParm
{
    private static KukaModel.CarryNode _start_node = new KukaModel.CarryNode();
    private static KukaModel.CarryNode _goal_node = new KukaModel.CarryNode();
    private static string _robot_status_feedback_time = "--";
    private static JArray _robot_status_infos = new JArray();
    private static List<KukaModel.Area> _kuka_area_models = new List<KukaModel.Area>();
    private static KukaModel.Area _bind_area = null;
    private static KukaModel.Area _target_area = null;

    // public static List<KukaAreaControl> AreaControls = new List<KukaAreaControl>();     // 已記錄的區域控制項

    public static event PropertyChangedEventHandler RobotStatusChanged;
    public static event PropertyChangedEventHandler AreaChanged;
    //public static event PropertyChangedEventHandler AreaStatusChanged;
    public static event PropertyChangedEventHandler CarryChanged;
    public static event PropertyChangedEventHandler BindChanged;        // 當綁定區域改變後

    public static string GetTodayTaskPath()
    {
        string file_name = "task" + DateTime.Today.ToString(@"yyyyMMdd") + ".ini";
        return Path.Combine(Application.StartupPath, "tasks\\" + file_name);
    }

    public static KukaModel.CarryNode StartNode       // 手動派車起點
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
    public static KukaModel.CarryNode GoalNode      // 手動派車終點
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

    /// <summary>
    /// 原始 API 回應區域資料
    /// </summary>
    public static List<KukaModel.Area> KukaOriginAreaModels { get; set; } = new List<KukaModel.Area>();

    /// <summary>
    /// 手動排序後區域資料
    /// </summary>
    public static List<KukaModel.Area> KukaAreaModels
    {
        get => _kuka_area_models;
        set
        {
            bool change = false;

            List<KukaModel.Area> rm_temp = new List<KukaModel.Area>();
            // 遍歷現有列表資料，將不存在於輸入列表的物件移除，並更新存在物件
            // 當物件存在且修改後，從輸入列表中移除
            foreach (KukaModel.Area model in _kuka_area_models)
            {
                // 判段原始區域列表是否需要增減
                KukaModel.Area find_in_value = value.FirstOrDefault(m => m.AreaCode == model.AreaCode);
                if ( find_in_value == null)
                {
                    rm_temp.Add(model);
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
                foreach (KukaModel.Area model in value)
                {
                    // 新資料，尚未處理控制項
                    _kuka_area_models.Add(model);
                }
                change = true;
            }
            foreach(KukaModel.Area model in rm_temp)
            {
                _kuka_area_models.Remove(model);
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

    public static KukaModel.Area BindAreaModel
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

    private static void OnRobotChanged(string propertyName)
    {
        RobotStatusChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
    }

    private static void OnCarryChanged(string propertyName)
    {
        CarryChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
    }
}




