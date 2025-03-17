﻿using CefSharp.DevTools.Accessibility;
using Chump_kuka.Controls;
using iCAPS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chump_kuka.Controller
{
    internal class BindAreaController
    {
        private static KukaAreaModel _bind_area = null;
        private static List<int> RecordNodeStatus = null;      // 紀錄的區域狀態
        
        public static event PropertyChangedEventHandler BindChanged;

        public static int BindStationNo { get; set; } = 0;
        public static KukaAreaModel BindArea
        {
            get => _bind_area;
            set
            {
                if (_bind_area != null && Env.BindAreaName == value.AreaName) return;        // 非首次綁定時，跳過資料相同的處理

                Env.BindAreaName = value.AreaName;
                _bind_area = value;

                switch (value.AreaName)
                {
                    case "产线作业区":
                        BindStationNo = 1;
                        break;
                    case "产线上料区":
                        BindStationNo = 2;
                        break;
                    default:
                        BindStationNo = 0;
                        break;
                }
                BindChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(BindArea)));
            }
        }

        public static void UpdateControl(KukaAreaControl bind_control)
        {
            // 判定綁定區域是否存在/更新
            if (_bind_area == null) return;
            
            // 如果控制項已綁定，不做後續動作
            if (_bind_area.UserControls.Contains(bind_control)) return;

            // 更新控制項為綁定區域資訊
            bind_control.Dock = DockStyle.Fill;
            bind_control.Margin = new Padding(10);
            bind_control.AreaName = _bind_area.AreaName;
            bind_control.AreaCode = _bind_area.AreaCode;
            bind_control.AreaNode = _bind_area.NodeList.ToArray();
            bind_control.UpdateContainerImage(_bind_area.NodeStatus.ToArray());        // 初次建立，更新圖片
            _bind_area.UserControls.Add(bind_control);
        }

        public static bool GetTaskNode()
        {
            List<int> current_status = _bind_area.NodeStatus;

            if (RecordNodeStatus == null)
            {
                RecordNodeStatus = current_status;
                return false;
            }

            if (RecordNodeStatus.SequenceEqual(current_status))
            {
                MsgBox.Show("沒有變化", "區域貨架異常");
                return false;
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
            }
            else if (result.Contains(1))
            {
                // return _bind_area.NodeList[result.IndexOf(1)];
                string carry_node = _bind_area.NodeList[result.IndexOf(1)];
                // TODO: 怎麼判定目前區域
                // 1. 獲取當前節點 OK
                // 2. 獲取目標區域
                // 3. 透過 laser 判定目標區域是否滿載 

                KukaParm.StartNode = new CarryNode()
                {
                    Code = carry_node,
                    Name = carry_node,
                    Type = "NODE_POINT"
                };
                KukaParm.GoalNode = new CarryNode()
                {
                    Code = "A000000002",
                    Name = "倉庫區",
                    Type = "NODE_AREA"
                };
                return true;
            }

            return false;
        }

        public static void PubRobotIn()
        {
            if (BindStationNo != 0)
                SocketDispatcher.Send($"station{BindAreaController.BindStationNo}_agv_star");
        }

        public static void PubRobotOut()
        {
            if (BindStationNo != 0)
                SocketDispatcher.Send($"station{BindAreaController.BindStationNo}_agv_begin");
        }

        public static void PubCarryOver()
        {
            if (BindStationNo != 0)
                SocketDispatcher.Send($"station{BindAreaController.BindStationNo}_agv_end");
        }

        /// <summary>
        /// 找尋列表中符合區域名稱的模型
        /// </summary>
        /// <param name="target_area"></param>
        /// <param name="areas"></param>
        /// <returns></returns>
        public KukaAreaControl Find(List<KukaAreaControl> controls) => controls.FirstOrDefault(control => control.AreaName == _bind_area?.AreaName);

    }
}
