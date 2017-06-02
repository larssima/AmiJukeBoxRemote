using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using AmiJukeboxRemote.webapi;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace AmiJukeBoxRemote.Mqtt
{
    public class Mqtt
    {
        private string _raspberryip = ConfigurationManager.AppSettings["RaspberryPiAddress"];

        public void SendCancelToSubscriber()
        {
            var mqttClient = new MqttClient(IPAddress.Parse(_raspberryip));
            string clientId = Guid.NewGuid().ToString();
            mqttClient.Connect(clientId);
            mqttClient.Publish("amiJukebox", Encoding.UTF8.GetBytes("cancel record"),
                MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE,false);
        }

        public void PlaySelectionOnJukebox(JukeboxController.JukeboxModel jbModel)
        {
            var mqttClient = new MqttClient(IPAddress.Parse(_raspberryip));
            string clientId = Guid.NewGuid().ToString();
            mqttClient.Connect(clientId);
            mqttClient.Publish("amiJukebox", Encoding.UTF8.GetBytes(jbModel.JbLetter+jbModel.JbNumber),
                MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        }
    }
}