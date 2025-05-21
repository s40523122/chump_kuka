using MQTTnet;
using MQTTnet.Server;
using System;
using System.Threading.Tasks;

namespace Chump_kuka.Services
{
    public class MqttBrokerService
    {
        private MqttServer _mqttServer;

        public async Task<bool> StartAsync(int port = 1883)
        {
            try
            {
                var mqttFactory = new MqttFactory();

                var optionsBuilder = new MqttServerOptionsBuilder()
                    .WithDefaultEndpoint()
                    .WithDefaultEndpointPort(port); // 預設 1883

                _mqttServer = mqttFactory.CreateMqttServer(optionsBuilder.Build());
                await _mqttServer.StartAsync();

                Console.WriteLine($"MQTT Broker 已啟動（Port: {port}）");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public async Task StopAsync()
        {
            if (_mqttServer != null)
            {
                await _mqttServer.StopAsync();
                Console.WriteLine("MQTT Broker 已停止");
            }
        }
    }
}