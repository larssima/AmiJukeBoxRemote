using System.Net;
using System.Text;
using AmiJukeBoxService.Models;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace AmiJukeBoxService.Mqtt;

public class MqttService
{
    private readonly string _raspberryIp;

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
        var client = new MqttClient(IPAddress.Parse(_raspberryIp));
        client.Connect(Guid.NewGuid().ToString());
        client.Publish("amiJukebox", Encoding.UTF8.GetBytes(message),
            MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
    }
}
