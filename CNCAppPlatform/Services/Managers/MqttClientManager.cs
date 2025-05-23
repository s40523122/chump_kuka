using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Protocol;
using MQTTnet.Server;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using static System.Windows.Forms.Design.AxImporter;

namespace Chump_kuka.Services
{
    public class MqttClientManager
    {
        //private readonly IMqttClient _client;
        private readonly IManagedMqttClient _client;
        private readonly MqttClientOptions _options;
        private readonly ManagedMqttClientOptions _managed_options;
        private string _clientId;

        // 接收到訊息時的事件
        public event Action<string, string> OnMessageReceived;

        // 建構函式：初始化 MQTT 客戶端與連線設定
        public MqttClientManager(string broker, int port = 1883)
        {
            var factory = new MqttFactory();
            // _client = factory.CreateMqttClient();
            _client = new MqttFactory().CreateManagedMqttClient();

            _clientId = $"client_{DateTime.Now.Ticks % 1000000:D6}";

            _options = new MqttClientOptionsBuilder()
                .WithClientId(_clientId)
                .WithTcpServer(broker, port)
                .WithCleanSession(false)     // 若為 true，重新連線後，所有訂閱都會被 Broker 清除，需要重新訂閱
                .Build();

            _managed_options = new ManagedMqttClientOptionsBuilder()
                .WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
                .WithClientOptions(_options)
                .Build();

            // 設定接收訊息事件處理
            _client.ApplicationMessageReceivedAsync += _client_ApplicationMessageReceivedAsync;

            // 設定連線與中斷事件（可視需求擴充）
            _client.ConnectedAsync += (_arg) =>
            {
                Console.WriteLine("MQTT 已連線");
                return Task.CompletedTask;
            };
            _client.DisconnectedAsync += (_arg) =>
            {
                Console.WriteLine("MQTT 已斷線");
                return Task.CompletedTask;
            };
        }

        private Task _client_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs arg)
        {
            string topic = arg?.ApplicationMessage?.Topic;
            string payload = Encoding.UTF8.GetString(arg?.ApplicationMessage.PayloadSegment.Array ?? Array.Empty<byte>());

            string pub_id = payload.Substring(0, 13);               // 發布者id
            string pub_msg = payload.Substring(13);                  // 發布內容

            // 若接收自己發布的資料，則返回
            if (pub_id == _clientId)
                return Task.CompletedTask;

            OnMessageReceived?.Invoke(topic, pub_msg);

            // Console.WriteLine($"Received: Topic:{topic}, Payload:{pub_msg}");
            return Task.CompletedTask;
        }

        // 非同步連線
        public async Task ConnectAsync()
        {
            if (!_client.IsStarted)
            {
                // await _client.ConnectAsync(_options);
                await _client.StartAsync(_managed_options);
            }
        }

        // 非同步斷線
        public async Task DisconnectAsync()
        {
            if (_client.IsStarted)
            {
                // await _client.DisconnectAsync();
                await _client.StopAsync();
            }
        }

        // 訂閱指定主題與 QoS（QoS: 0, 1, 2）
        public async Task SubscribeAsync(string topic, int qosLevel = 0)
        {
            // 0: 只發布一次（不保證送達）
            // 1: 可能重複發布，但至少會送達一次 (可能重複接收)
            // 2: 可能重複發布，保證只送達一次 (不重複接收)
            if (!_client.IsStarted)
                throw new InvalidOperationException("MQTT 尚未連線");

            var qos = (MqttQualityOfServiceLevel)qosLevel;

            //var topicFilter = new MqttTopicFilterBuilder()
            //    .WithTopic(topic)
            //    .WithQualityOfServiceLevel(qos)
            //    .Build();

            await _client.SubscribeAsync(topic, qos);
            // Console.WriteLine($"已訂閱主題：{topic}（QoS {qosLevel}）");
        }

        // 發布訊息至指定主題與 QoS
        public async Task PublishAsync(string topic, string payload, int qosLevel = 0, bool retain = false)
        {
            // 0: 只發布一次（不保證送達）
            // 1: 可能重複發布，但至少會送達一次 (可能重複接收)
            // 2: 可能重複發布，保證只送達一次 (不重複接收)

            if (!_client.IsStarted)
                throw new InvalidOperationException("MQTT 尚未連線");

            var qos = (MqttQualityOfServiceLevel)qosLevel;

            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(_clientId + payload)
                .WithQualityOfServiceLevel(qos)
                .WithRetainFlag(retain)
                .Build();

            // await _client.PublishAsync(message);
            await _client.EnqueueAsync(message);
            // Console.WriteLine($"已發布至主題：{topic}，內容：{payload}（QoS {qosLevel}）");
        }
    }
}