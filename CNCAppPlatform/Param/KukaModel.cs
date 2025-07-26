using Chump_kuka.Controls;
using iCAPS;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chump_kuka
{
    internal class KukaModel
    {
        public class Node : INotifyPropertyChanged
        {
            private int _rack_status = -1;      // 貨架狀態 {0: 無貨架, 1: 空貨架, 2: 滿貨架}
            private int _node_status = -1;      // 節點狀態 {0: 普通, 1: 上鎖, 2: 已建立任務}

            // 建立屬性值發生變化的通知事件
            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged(string name) =>
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

            public string NodeCode { get; set; }
            public string NodeName { get => NodeCode; }
            public int RackStatus
            {
                get => _rack_status;
                set
                {
                    if (_rack_status == value) return;
                    _rack_status = value;
                    OnPropertyChanged(nameof(RackStatus));
                }
            }

            /// <summary>
            /// 節點狀態 {0: 普通, 1: 上鎖, 2: 已建立任務}
            /// </summary>
            public int NodeStatus
            {
                get => _node_status;
                set
                {
                    if (_node_status == value) return;

                    if (_node_status == 2)
                    {
                        MsgBox.Show("選用貨架已佔用!");
                        return;
                    }

                    if (value == 1)
                    {
                        if (_rack_status != 1)
                        {
                            // 若不是空貨架無法上鎖
                            MsgBox.Show("僅能上鎖空貨架!");
                            return;
                        }
                    }

                    _node_status = value;
                    OnPropertyChanged(nameof(NodeStatus));
                }
            }

            public Node(string nodeCode)
            {
                NodeCode = nodeCode;
            }

            public override string ToString() =>
                Newtonsoft.Json.JsonConvert.SerializeObject(this);

            public Node Clone()
            {
                return new Node(this.NodeCode)
                {
                    RackStatus = this.RackStatus,
                    NodeStatus = this.NodeStatus
                };
            }

            // 為了比較內容，要實作 Equals 與 GetHashCode
            public override bool Equals(object obj)
            {
                if (obj is Node other)
                    return NodeCode == other.NodeCode;

                return false;
            }

            public override int GetHashCode()
            {
                // 使用 .NET Framework 安全寫法
                unchecked
                {
                    int hash = 17;
                    hash = hash * 23 + (NodeCode?.GetHashCode() ?? 0);
                    hash = hash * 23 + RackStatus.GetHashCode();
                    hash = hash * 23 + NodeStatus.GetHashCode();
                    return hash;
                }
            }

        }

        public class Area : INotifyPropertyChanged
        {
            //public event PropertyChangedEventHandler NodeStatusChanged;
            //public event PropertyChangedEventHandler ModelChanged;

            private string _name;
            private int[] _node_status = new int[0];
            private Node[] _node_list = new Node[0];

            // 建立屬性值發生變化的通知事件
            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged(string name) =>
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

            /// <summary>
            /// 區域編碼 ex: area001
            /// </summary>
            public string AreaCode { get; set; }

            /// <summary>
            /// 區域名稱 ex: 加工區
            /// </summary>
            public string AreaName
            {
                get => _name;
                set
                {
                    _name = value;
                    OnPropertyChanged(nameof(AreaName));        // 屬性發生變化
                }
            }

            /// <summary>
            /// 區域類型 {1: 庫區, 2: 作業區, 3: 暫存區, 4: 緩存區}
            /// </summary>
            public int AreaType { get; set; }

            [JsonIgnore]        // 避免序列化循環引用
            public KukaAreaControl ControlUI { get; set; }

            /// <summary>
            /// 點位集合
            /// </summary>
            public Node[] NodeList
            {
                get => _node_list;
                set
                {
                    if (_node_list == null || !_node_list.SequenceEqual(value))
                    {
                        _node_list = value;
                        //ModelChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NodeList)));

                        //_node_status = new int[_node_list.Length / 2];

                        foreach (Node node in value)
                        {
                            // node.PropertyChanged += Node_PropertyChanged;
                        }

                        OnPropertyChanged(nameof(NodeList));        // 屬性發生變化
                    }
                }
            }

            private void Node_PropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                OnPropertyChanged(nameof(NodeList));
            }

            /// <summary>
            /// 鎖定節點，允許區域自動搬運
            /// </summary>
            public List<string> LockNodes { get; set; } = new List<string>();

            public Area(JObject json_object = null)
            {
                if (json_object == null) return;

                AreaCode = json_object["areaCode"].ToString();
                AreaName = json_object["areaName"].ToString();
                //NodeList = json_object["nodeList"].ToObject<string[]>();      // 集合查詢到的區域代碼為陣列
            }

            /// <summary>
            /// 取得指定節點模型
            /// </summary>
            /// <param name="node_code"></param>
            /// <returns></returns>
            public Node GetNode(string node_code)
            {
                Node node = NodeList.FirstOrDefault(_node => _node.NodeCode == node_code);
                return node;
            }

            public bool CheckAndUpdate(Area param)
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

            public void CompareAndUpdate(Area source_model)
            {
                this.AreaName = source_model.AreaName;
                this.AreaType = source_model.AreaType;
                this.NodeList = source_model.NodeList;
                //this.NodeStatus = source_model.NodeStatus;
            }

            /// <summary>
            /// 找尋列表中符合區域名稱的模型
            /// </summary>
            /// <param name="target_area"></param>
            /// <param name="areas"></param>
            /// <returns></returns>
            public static Area Find(string target_area_name, List<Area> areas) => areas.FirstOrDefault(area => area.AreaName == target_area_name);

            public static bool CompareData(List<Area> sourceData, List<Area> targetData) => sourceData.Select(m => m.AreaName).SequenceEqual(targetData.Select(m => m.AreaName));

            public Area Next()
            {
                int index = KukaParm.KukaAreaModels.IndexOf(this);
                if (index == -1)
                {
                    return this;
                }
                else if (index == KukaParm.KukaAreaModels.Count - 1)
                {
                    // 該筆資料為最後一筆，下一筆回到首筆資料
                    return KukaParm.KukaAreaModels[0];
                }
                else
                {
                    return KukaParm.KukaAreaModels[index + 1];
                }
            }

            public override string ToString() => AreaName;
        }

        public class CarryNode
        {
            public string Code { get; set; }
            public string Type { get; set; }
            public string Name { get; set; } = "null";

            public CarryNode(KukaModel.Area node_model = null)
            {
                if (node_model != null)
                {
                    Code = node_model.AreaCode;
                    Type = "NODE_AREA";
                    Name = node_model.AreaName;
                }
            }
        }

        public class SimpleCarryTask
        {
            public string Called { get; set; }
            public int ID { get; set; }
            public string StartNode { get; set; }
            public string GoalNode { get; set; }
            public string CreateTime { get; set; }
            public string FinishTime { get; set; }
            public string LogMsg { get; set; }

            public SimpleCarryTask() { }
            public SimpleCarryTask(CarryTask task)
            {
                ID = task.ID;
                StartNode = task.StartNode.Name;
                GoalNode = task.GoalNode.Name;
                CreateTime = task.CreateTime.ToString(@"MM/dd tt hh:mm");
                FinishTime = task.FinishTime?.ToString(@"MM/dd tt hh:mm");
                Called = task.Called ? "🔔" : "🔕";
                LogMsg = task.LogMsg;

                if (FinishTime == null)
                {
                    FinishTime = "";
                }
            }
        }

        public class CarryTask
        {
            private bool _called = false;
            private DateTime? _finish_time;
            private string _log_msg = "";


            public int ID { get; set; }
            public bool Called
            {
                get => _called;
                set
                {
                    _called = value;
                    WriteIni();
                }
            }
            public string AreaCode { get; set; }
            public CarryNode StartNode { get; set; }
            public CarryNode GoalNode { get; set; }
            public DateTime CreateTime { get; set; }
            public DateTime? FinishTime
            {
                get => _finish_time;
                set
                {
                    _finish_time = value;
                    WriteIni();
                }
            }
            public string LogMsg
            {
                get => _log_msg;
                set
                {
                    _log_msg = value;
                    WriteIni();
                }
            }

            public CarryTask(int task_id, bool called, CarryNode start_node, CarryNode goal_node, string areaCode)
            {
                ID = task_id;
                Called = called;
                StartNode = start_node;
                GoalNode = goal_node;
                CreateTime = DateTime.Now;
                AreaCode = areaCode;
                FinishTime = null;
            }

            private void WriteIni()
            {
                string file_path = KukaParm.GetTodayTaskPath();
                string task_msg = Newtonsoft.Json.JsonConvert.SerializeObject(this);
                INiReader.WriteINIFile(file_path, "tasks", ID.ToString(), task_msg);       //單筆任務寫入
            }
        }

        public class KukaOriginAreaModel
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
            public string[] NodeList { get; set; }

            public int[] NodeStatus { get; set; }

        }

    }
}
