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
using System.Reactive;
using Chump_kuka.Controller;

namespace Chump_kuka
{
    internal class KukaModel
    {
        public class Node : INotifyPropertyChanged
        {
            private int _rack_status = -1;      // 貨架狀態 {0: 無貨架, 1: 空貨架, 2: 滿貨架}
            private int _node_status = 0;      // 節點狀態 {0: 普通, 1: 已建立任務}
            private bool _is_lock = false;

            // 建立屬性值發生變化的通知事件
            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged(string name) =>
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

            public string NodeCode { get; set; }
            public string NodeName { get => NodeCode; }

            public bool IsLock
            {
                get => _is_lock;
                set
                {
                    if (_is_lock == value) return;
                    _is_lock = value;
                    OnPropertyChanged(nameof(IsLock));
                    KukaParm.WriteParamHistory();
                }
            }

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
            /// 節點狀態 {0: 普通, 1: 已建立任務}
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

                    _node_status = value;
                    OnPropertyChanged(nameof(NodeStatus));
                    KukaParm.WriteParamHistory();
                }
            }

            [JsonIgnore]
            public Area Parent 
            { 
                get; 
                set; 
            }

            [JsonConstructor]
            public Node(string nodeCode, bool isLock, int rackStatus, int nodeStatus)
            {
                NodeCode = nodeCode;
                _is_lock = isLock;
                _rack_status = rackStatus;
                _node_status = nodeStatus;
            }

            public Node(string jj)
            {
                NodeCode = jj;
            }

            public bool IsEmpty() => NodeStatus == 0 && RackStatus == 0;

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
                    return NodeCode == other.NodeCode && IsLock == other.IsLock && NodeStatus == other.NodeStatus;

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

            #region 屬性
            /// <summary>
            /// 區域所在順序 (從 0 開始; -1 表示為定義)
            /// </summary>
            public int Index { get; private set; } = -1;

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

            /// <summary>
            /// 點位集合
            /// </summary>
            public Node[] NodeList
            {
                get => _node_list;
                set
                {
                    if (value == null) return;
                    if (_node_list == null || !_node_list.SequenceEqual(value))
                    {
                        _node_list = null;
                        _node_list = value;
                        //ModelChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NodeList)));

                        //_node_status = new int[_node_list.Length / 2];

                        foreach (Node node in value)
                        {
                            // node.PropertyChanged += Node_PropertyChanged;
                            node.Parent = this;
                        }

                        OnPropertyChanged(nameof(NodeList));        // 屬性發生變化
                        KukaParm.WriteParamHistory();
                    }
                }
            }

            private void Node_PropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                OnPropertyChanged(nameof(NodeList));
            }

            #endregion 屬性

            [JsonConstructor]
            public Area(string areaCode, string areaName, int areaType, Node[] nodeList)
            {
                AreaCode = areaCode;
                _name = areaName;
                AreaType = areaType;
                NodeList = nodeList;
            }
            public Area(JObject json_object = null)
            {
                if (json_object == null) return;

                AreaCode = json_object["areaCode"].ToString();
                AreaName = json_object["areaName"].ToString();
                //NodeList = json_object["nodeList"].ToObject<string[]>();      // 集合查詢到的區域代碼為陣列
            }

            /// <summary>
            /// 設定區域所在順序
            /// </summary>
            public int SetIndex(int index) => Index = index;

            /// <summary>
            /// 取得指定節點模型
            /// </summary>
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
                //int index = KukaParm.KukaAreaModels.IndexOf(this);
                //if (index == -1)
                //{
                //    return this;
                //}
                //else if (index == KukaParm.KukaAreaModels.Count - 1)
                //{
                //    // 該筆資料為最後一筆，下一筆回到首筆資料
                //    return KukaParm.KukaAreaModels[0];
                //}
                //else
                //{
                //    return KukaParm.KukaAreaModels[index + 1];
                //}
                return KukaParm.GetAreaModelByIndex(Index+1);
            }

            public Node GetEmptyNode() => _node_list.FirstOrDefault(node => node.IsEmpty());
            public Node GetLockNode() => _node_list.FirstOrDefault(node => node.IsLock);

            public override string ToString() => AreaName;
        }

        public class CarryModel
        {
            public string Name { get; set; } = "null";

            public string AreaCode { get; set; }

            public Node NodeModel { get; set; }

            public CarryModel(string name, string area_code, Node node_model)
            {
                Name = name;
                AreaCode = area_code;
                NodeModel = node_model;
                //IsArea = node_model == null ? true : false;
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
                Called = task.IsCalled ? "🔔" : "🔕";
                LogMsg = task.LogMsg;

                if (task.FinishTime == null)
                {
                    FinishTime = "";
                }
                else if (task.FinishTime == DateTime.MinValue)
                {
                    FinishTime = "已取消";
                }
                else
                {
                    FinishTime = task.FinishTime?.ToString(@"MM/dd tt hh:mm");
                }
            }
        }

        public class CarryTask : INotifyPropertyChanged
        {
            private bool _called = false;
            private DateTime? _finish_time;
            private string _log_msg = "";


            public int ID { get; set; }

            public string MissionCode { get; set; }

            public bool IsCalled
            {
                get => _called;
                set
                {
                    _called = value;
                    WriteIni();
                    OnPropertyChanged(nameof(IsCalled));
                }
            }

            public bool IsPlan { get; set; } = false;

            public CarryModel StartNode { get; set; }
            public CarryModel GoalNode { get; set; }
            public DateTime CreateTime { get; set; }
            public DateTime? FinishTime
            {
                get => _finish_time;
                set
                {
                    _finish_time = value;
                    WriteIni();
                    OnPropertyChanged(nameof(FinishTime));
                }
            }

            public string LogMsg
            {
                get => _log_msg;
                set
                {
                    _log_msg = value;
                    WriteIni();
                    OnPropertyChanged(nameof(LogMsg));
                }
            }

            /// <summary>
            /// 資料軟刪除時間，若未刪除則為 string.Empty
            /// </summary>
            [JsonProperty]
            public bool IsDeleted { get; private set; } = false;

            public CarryTask(int task_id, bool called, CarryModel start_node, CarryModel goal_node)
            {
                if (task_id != 0)       // 防止 Json 因序列化時，自動實作，出現錯誤
                {
                    ID = task_id;
                    MissionCode = $"mission{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}";
                    IsCalled = called;
                    StartNode = start_node;
                    GoalNode = goal_node;
                    CreateTime = DateTime.Now;
                    FinishTime = null;
                }
            }

            private void WriteIni()
            {
                string file_path = KukaParm.GetTodayTaskPath();
                string task_msg = Newtonsoft.Json.JsonConvert.SerializeObject(this);
                INiReader.WriteINIFile(file_path, "tasks", ID.ToString(), task_msg);       //單筆任務寫入
            }

            public void SoftDelete()
            {
                IsDeleted = true;
                LogMsg += $"[{DateTime.Now.ToString(@"MM/dd tt hh:mm:ss")}] 已刪除任務\n";
                OnPropertyChanged(nameof(IsDeleted));
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected void OnPropertyChanged(string propertyName)
                => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
