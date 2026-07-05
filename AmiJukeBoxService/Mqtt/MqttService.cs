using System.Text;
using AmiJukeBoxService.Models;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace AmiJukeBoxService.Mqtt;

public class MqttService : IDisposable
{
    private readonly string _raspberryIp;
    private readonly object _lock = new();
    private MqttClient? _client;

    public MqttService(IConfiguration config)
    {
        _raspberryIp = config["AppSettings:RaspberryPiAddress"]!;
    }

    public void SendCancel()
    {
        Publish("cancel record");
    }

    public void PlaySelection(JukeboxModel model)
    {
        Publish(model.JbLetter + model.JbNumber);
    }

    private void Publish(string message)
    {
        lock (_lock)
        {
            var client = GetConnectedClient();
            client.Publish("amiJukebox", Encoding.UTF8.GetBytes(message),
                MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        }
    }

    private MqttClient GetConnectedClient()
    {
        if (_client is not { IsConnected: true })
        {
            _client = new MqttClient(_raspberryIp);
            _client.Connect(Guid.NewGuid().ToString());
        }
        return _client;
    }

    public void Dispose()
    {
        if (_client is { IsConnected: true })
            _client.Disconnect();
    }
}
