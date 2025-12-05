using Chump_kuka.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Chump_kuka
{
    internal class MqttDispatcher
    {
        private MqttBrokerService broker;
        private MqttClientManager mqttService;
        private Dictionary<string, Action<string>> callback_funcs = new Dictionary<string, Action<string>>();

        /// <summary>
        /// 開啟 MQTT Broker
        /// </summary>
        /// <returns></returns>
        public async Task<bool> StartBroker(int port=1883)
        {
            broker = new MqttBrokerService();
            bool success = await broker.StartAsync(port);
            Console.WriteLine("MQTT Broker is open");
            return success;
        }

        /// <summary>
        /// 開啟 MQTT Broker
        /// </summary>
        /// <returns></returns>
        public async Task StopBroker()
        {
            if (broker != null) 
                await broker?.StopAsync();
        }

        /// <summary>
        /// 建立 MQTT 客戶端連線
        /// </summary>
        /// <returns></returns>
        public async Task<bool> InitClient(string broker_ip="127.0.0.1", int port=1883)
        {
            mqttService = new MqttClientManager(broker_ip);
            
            mqttService.OnMessageReceived += (topic, message) =>
            {
                // Console.WriteLine($"[接收] Topic: {topic}, Message: {message}");
                try
                {
                    callback_funcs[topic](message);
                }
                catch (KeyNotFoundException no_key)
                {
                    // MessageBox.Show(ex.Message);

                    // 若找不到，可能原因為使用萬用字元，
                    // 導致回覆主題為'topic/test'，但登記主題為'topic/#'，
                    // 無法使用字典進行 callback function 查詢
                    string[] split_topic = topic.Split('/');
                    callback_funcs[$"{split_topic[0]}/#"](message);     // 強制改為使用萬用符號

                    // 無法適用所有情形，還是建議不要使用萬用符號
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            };
            try
            {
                await mqttService.ConnectAsync();
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
                MessageBox.Show("請檢查伺服器是否開啟");
                return false;
            }
            return true;
        }

        public async Task CloseClient()
        {
            if (mqttService != null)
                await mqttService?.DisconnectAsync();
        }


        #region std
        /// <summary>
        /// 標準發布器
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="payload"></param>
        public async void Publisher(string topic, string payload, int qos=2)
        {
            if (mqttService == null)
            {
                MessageBox.Show("尚未建立客戶端連線");
                return;
            }

            await mqttService.PublishAsync(topic, payload, qos);
        }

        /// <summary>
        /// 標準訂閱器
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="qosLevel"></param>
        public async void Subscriber(string topic, Action<string> func, int qos=2)
        {
            if (mqttService == null)
            {
                MessageBox.Show("尚未建立客戶端連線");
                return;
            }

            await mqttService.SubscribeAsync(topic, qos);

            callback_funcs[topic] = func;
        }
        #endregion
    }
}
