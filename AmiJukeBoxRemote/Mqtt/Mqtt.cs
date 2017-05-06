using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace AmiJukeBoxRemote.Mqtt
{
    public class Mqtt
    {
        public void SendCancelToSubscriber()
        {
            var mqttClient = new MqttClient(IPAddress.Parse("192.168.0.110"));
            string clientId = Guid.NewGuid().ToString();
            mqttClient.Connect(clientId);
            mqttClient.Publish("amiJukebox", Encoding.UTF8.GetBytes("cancel record"),
                MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE,true);
        }
    }
}