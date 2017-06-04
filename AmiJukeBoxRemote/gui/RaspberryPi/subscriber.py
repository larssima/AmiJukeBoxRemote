#!/usr/bin/env python
import paho.mqtt.client as mqtt
import cancel2
import playjba
import playjbb
import playjbc
import playjbd
import playjbe
import playjbf
import playjbg
import playjbh
import playjbj
import playjbk
import subprocess

# Define Variables
MQTT_BROKER = "127.0.0.1"
MQTT_PORT = 1883
MQTT_KEEPALIVE_INTERVAL = 45
MQTT_TOPIC = "amiJukebox"


# Define on_connect event Handler
def on_connect(mosq, obj, rc):
	#Subscribe to a the Topic
	mqttc.subscribe(MQTT_TOPIC, 0)

# Define on_subscribe event Handler
def on_subscribe(mosq, obj, mid, granted_qos):
    print "Subscribed to MQTT Topic"

# Define on_message event Handler
#def on_message(mosq, obj, msg):
#	print msg.payload

def on_message(mosq, obj, msg):
	if msg.payload.decode() == "cancel record":
		cancel2.cancelrecord()
		print "Funkar"
	elif msg.payload.decode() == "A1":
		playjba.play(1)
		print "Playing A1"
        elif msg.payload.decode() == "A2":
                playjba.play(2)
                print "Playing A2"
        elif msg.payload.decode() == "A3":
                playjba.play(3)
                print "Playing A3"
        elif msg.payload.decode() == "A4":
                playjba.play(4)
                print "Playing A4"
        elif msg.payload.decode() == "A5":
                playjba.play(5)
                print "Playing A5"
        elif msg.payload.decode() == "A6":
                playjba.play(6)
                print "Playing A6"
        elif msg.payload.decode() == "A7":
                playjba.play(7)
                print "Playing A7"
        elif msg.payload.decode() == "A8":
                playjba.play(8)
                print "Playing A8"
        elif msg.payload.decode() == "A9":
                playjba.play(9)
                print "Playing A9"
        elif msg.payload.decode() == "A10":
                playjba.play(10)
                print "Playing A10"
        elif msg.payload.decode() == "A11":
                playjba.play(11)
                print "Playing A11"
        elif msg.payload.decode() == "A12":
                playjba.play(12)
                print "Playing A12"
        elif msg.payload.decode() == "A13":
                playjba.play(13)
                print "Playing A13"
        elif msg.payload.decode() == "A14":
                playjba.play(14)
                print "Playing A14"
        elif msg.payload.decode() == "A15":
                playjba.play(15)
                print "Playing A15"
        elif msg.payload.decode() == "A16":
                playjba.play(16)
                print "Playing A16"
        elif msg.payload.decode() == "A17":
                playjba.play(17)
                print "Playing A17"
        elif msg.payload.decode() == "A18":
                playjba.play(18)
                print "Playing A18"
        elif msg.payload.decode() == "A19":
                playjba.play(19)
                print "Playing A19"
        elif msg.payload.decode() == "A20":
                playjba.play(20)
                print "Playing A20"
        elif msg.payload.decode() == "B1":
                playjbb.play(1)
                print "Playing B1"
        elif msg.payload.decode() == "B2":
                playjbb.play(2)
                print "Playing B2"
        elif msg.payload.decode() == "B3":
                playjbb.play(3)
                print "Playing B3"
        elif msg.payload.decode() == "B4":
                playjbb.play(4)
                print "Playing B4"
        elif msg.payload.decode() == "B5":
                playjbb.play(5)
                print "Playing B5"
        elif msg.payload.decode() == "B6":
                playjbb.play(6)
                print "Playing B6"
        elif msg.payload.decode() == "B7":
                playjbb.play(7)
                print "Playing B7"
        elif msg.payload.decode() == "B8":
                playjbb.play(8)
                print "Playing B8"
        elif msg.payload.decode() == "B9":
                playjbb.play(9)
                print "Playing B9"
        elif msg.payload.decode() == "B10":
                playjbb.play(10)
                print "Playing B10"
        elif msg.payload.decode() == "B11":
                playjbb.play(11)
                print "Playing B11"
        elif msg.payload.decode() == "B12":
                playjbb.play(12)
                print "Playing B12"
        elif msg.payload.decode() == "B13":
                playjbb.play(13)
                print "Playing B13"
        elif msg.payload.decode() == "B14":
                playjbb.play(14)
                print "Playing B14"
        elif msg.payload.decode() == "B15":
                playjbb.play(15)
                print "Playing B15"
        elif msg.payload.decode() == "B16":
                playjbb.play(16)
                print "Playing B16"
        elif msg.payload.decode() == "B17":
                playjbb.play(17)
                print "Playing B17"
        elif msg.payload.decode() == "B18":
                playjbb.play(18)
                print "Playing B18"
        elif msg.payload.decode() == "B19":
                playjbb.play(19)
                print "Playing B19"
        elif msg.payload.decode() == "B20":
                playjbb.play(20)
                print "Playing B20"
        elif msg.payload.decode() == "C1":
                playjbc.play(1)
                print "Playing C1"
        elif msg.payload.decode() == "C2":
                playjbc.play(2)
                print "Playing C2"
        elif msg.payload.decode() == "C3":
                playjbc.play(3)
                print "Playing C3"
        elif msg.payload.decode() == "C4":
                playjbc.play(4)
                print "Playing C4"
        elif msg.payload.decode() == "C5":
                playjbc.play(5)
                print "Playing C5"
        elif msg.payload.decode() == "C6":
                playjbc.play(6)
                print "Playing B6"
        elif msg.payload.decode() == "C7":
                playjbc.play(7)
                print "Playing C7"
        elif msg.payload.decode() == "C8":
                playjbc.play(8)
                print "Playing C8"
        elif msg.payload.decode() == "C9":
                playjbc.play(9)
                print "Playing C9"
        elif msg.payload.decode() == "C10":
                playjbc.play(10)
                print "Playing C10"
        elif msg.payload.decode() == "C11":
                playjbc.play(11)
                print "Playing C11"
        elif msg.payload.decode() == "C12":
                playjbc.play(12)
                print "Playing C12"
        elif msg.payload.decode() == "C13":
                playjbc.play(13)
                print "Playing C13"
        elif msg.payload.decode() == "C14":
                playjbb.play(14)
                print "Playing C14"
        elif msg.payload.decode() == "C15":
                playjbc.play(15)
                print "Playing C15"
        elif msg.payload.decode() == "C16":
                playjbc.play(16)
                print "Playing C16"
        elif msg.payload.decode() == "C17":
                playjbc.play(17)
                print "Playing C17"
        elif msg.payload.decode() == "C18":
                playjbc.play(18)
                print "Playing C18"
        elif msg.payload.decode() == "C19":
                playjbc.play(19)
                print "Playing C19"
        elif msg.payload.decode() == "C20":
                playjbc.play(20)
                print "Playing C20"
        elif msg.payload.decode() == "D1":
                playjbd.play(1)
                print "Playing D1"
        elif msg.payload.decode() == "D2":
                playjbd.play(2)
                print "Playing D2"
        elif msg.payload.decode() == "D3":
                playjbd.play(3)
                print "Playing D3"
        elif msg.payload.decode() == "D4":
                playjbd.play(4)
                print "Playing D4"
        elif msg.payload.decode() == "D5":
                playjbd.play(5)
                print "Playing D5"
        elif msg.payload.decode() == "D6":
                playjbd.play(6)
                print "Playing B6"
        elif msg.payload.decode() == "D7":
                playjbd.play(7)
                print "Playing D7"
        elif msg.payload.decode() == "D8":
                playjbd.play(8)
                print "Playing D8"
        elif msg.payload.decode() == "D9":
                playjbd.play(9)
                print "Playing D9"
        elif msg.payload.decode() == "D10":
                playjbd.play(10)
                print "Playing D10"
        elif msg.payload.decode() == "D11":
                playjbd.play(11)
                print "Playing D11"
        elif msg.payload.decode() == "D12":
                playjbd.play(12)
                print "Playing D12"
        elif msg.payload.decode() == "D13":
                playjbd.play(13)
                print "Playing D13"
        elif msg.payload.decode() == "D14":
                playjbb.play(14)
                print "Playing D14"
        elif msg.payload.decode() == "D15":
                playjbd.play(15)
                print "Playing D15"
        elif msg.payload.decode() == "D16":
                playjbd.play(16)
                print "Playing D16"
        elif msg.payload.decode() == "D17":
                playjbd.play(17)
                print "Playing D17"
        elif msg.payload.decode() == "D18":
                playjbd.play(18)
                print "Playing D18"
        elif msg.payload.decode() == "D19":
                playjbd.play(19)
                print "Playing D19"
        elif msg.payload.decode() == "D20":
                playjbd.play(20)
                print "Playing D20"  
        elif msg.payload.decode() == "E1":
                playjbe.play(1)
                print "Playing E1"
        elif msg.payload.decode() == "E2":
                playjbe.play(2)
                print "Playing E2"
        elif msg.payload.decode() == "E3":
                playjbe.play(3)
                print "Playing E3"
        elif msg.payload.decode() == "E4":
                playjbe.play(4)
                print "Playing E4"
        elif msg.payload.decode() == "E5":
                playjbe.play(5)
                print "Playing E5"
        elif msg.payload.decode() == "E6":
                playjbe.play(6)
                print "Playing B6"
        elif msg.payload.decode() == "E7":
                playjbe.play(7)
                print "Playing E7"
        elif msg.payload.decode() == "E8":
                playjbe.play(8)
                print "Playing E8"
        elif msg.payload.decode() == "E9":
                playjbe.play(9)
                print "Playing E9"
        elif msg.payload.decode() == "E10":
                playjbe.play(10)
                print "Playing E10"
        elif msg.payload.decode() == "E11":
                playjbe.play(11)
                print "Playing E11"
        elif msg.payload.decode() == "E12":
                playjbe.play(12)
                print "Playing E12"
        elif msg.payload.decode() == "E13":
                playjbe.play(13)
                print "Playing E13"
        elif msg.payload.decode() == "E14":
                playjbb.play(14)
                print "Playing E14"
        elif msg.payload.decode() == "E15":
                playjbe.play(15)
                print "Playing E15"
        elif msg.payload.decode() == "E16":
                playjbe.play(16)
                print "Playing E16"
        elif msg.payload.decode() == "E17":
                playjbe.play(17)
                print "Playing E17"
        elif msg.payload.decode() == "E18":
                playjbe.play(18)
                print "Playing E18"
        elif msg.payload.decode() == "E19":
                playjbe.play(19)
                print "Playing E19"
        elif msg.payload.decode() == "E20":
                playjbe.play(20)
                print "Playing E20"     
        elif msg.payload.decode() == "F1":
                playjbf.play(1)
                print "Playing F1"
        elif msg.payload.decode() == "F2":
                playjbf.play(2)
                print "Playing F2"
        elif msg.payload.decode() == "F3":
                playjbf.play(3)
                print "Playing F3"
        elif msg.payload.decode() == "F4":
                playjbf.play(4)
                print "Playing F4"
        elif msg.payload.decode() == "F5":
                playjbf.play(5)
                print "Playing F5"
        elif msg.payload.decode() == "F6":
                playjbf.play(6)
                print "Playing B6"
        elif msg.payload.decode() == "F7":
                playjbf.play(7)
                print "Playing F7"
        elif msg.payload.decode() == "F8":
                playjbf.play(8)
                print "Playing F8"
        elif msg.payload.decode() == "F9":
                playjbf.play(9)
                print "Playing F9"
        elif msg.payload.decode() == "F10":
                playjbf.play(10)
                print "Playing F10"
        elif msg.payload.decode() == "F11":
                playjbf.play(11)
                print "Playing F11"
        elif msg.payload.decode() == "F12":
                playjbf.play(12)
                print "Playing F12"
        elif msg.payload.decode() == "F13":
                playjbf.play(13)
                print "Playing F13"
        elif msg.payload.decode() == "F14":
                playjbb.play(14)
                print "Playing F14"
        elif msg.payload.decode() == "F15":
                playjbf.play(15)
                print "Playing F15"
        elif msg.payload.decode() == "F16":
                playjbf.play(16)
                print "Playing F16"
        elif msg.payload.decode() == "F17":
                playjbf.play(17)
                print "Playing F17"
        elif msg.payload.decode() == "F18":
                playjbf.play(18)
                print "Playing F18"
        elif msg.payload.decode() == "F19":
                playjbf.play(19)
                print "Playing F19"
        elif msg.payload.decode() == "F20":
                playjbf.play(20)
                print "Playing F20"          
        elif msg.payload.decode() == "G1":
                playjbg.play(1)
                print "Playing G1"
        elif msg.payload.decode() == "G2":
                playjbg.play(2)
                print "Playing G2"
        elif msg.payload.decode() == "G3":
                playjbg.play(3)
                print "Playing G3"
        elif msg.payload.decode() == "G4":
                playjbg.play(4)
                print "Playing G4"
        elif msg.payload.decode() == "G5":
                playjbg.play(5)
                print "Playing G5"
        elif msg.payload.decode() == "G6":
                playjbg.play(6)
                print "Playing B6"
        elif msg.payload.decode() == "G7":
                playjbg.play(7)
                print "Playing G7"
        elif msg.payload.decode() == "G8":
                playjbg.play(8)
                print "Playing G8"
        elif msg.payload.decode() == "G9":
                playjbg.play(9)
                print "Playing G9"
        elif msg.payload.decode() == "G10":
                playjbg.play(10)
                print "Playing G10"
        elif msg.payload.decode() == "G11":
                playjbg.play(11)
                print "Playing G11"
        elif msg.payload.decode() == "G12":
                playjbg.play(12)
                print "Playing G12"
        elif msg.payload.decode() == "G13":
                playjbg.play(13)
                print "Playing G13"
        elif msg.payload.decode() == "G14":
                playjbb.play(14)
                print "Playing G14"
        elif msg.payload.decode() == "G15":
                playjbg.play(15)
                print "Playing G15"
        elif msg.payload.decode() == "G16":
                playjbg.play(16)
                print "Playing G16"
        elif msg.payload.decode() == "G17":
                playjbg.play(17)
                print "Playing G17"
        elif msg.payload.decode() == "G18":
                playjbg.play(18)
                print "Playing G18"
        elif msg.payload.decode() == "G19":
                playjbg.play(19)
                print "Playing G19"
        elif msg.payload.decode() == "G20":
                playjbg.play(20)
                print "Playing G20"          
        elif msg.payload.decode() == "H1":
                playjbh.play(1)
                print "Playing H1"
        elif msg.payload.decode() == "H2":
                playjbh.play(2)
                print "Playing H2"
        elif msg.payload.decode() == "H3":
                playjbh.play(3)
                print "Playing H3"
        elif msg.payload.decode() == "H4":
                playjbh.play(4)
                print "Playing H4"
        elif msg.payload.decode() == "H5":
                playjbh.play(5)
                print "Playing H5"
        elif msg.payload.decode() == "H6":
                playjbh.play(6)
                print "Playing B6"
        elif msg.payload.decode() == "H7":
                playjbh.play(7)
                print "Playing H7"
        elif msg.payload.decode() == "H8":
                playjbh.play(8)
                print "Playing H8"
        elif msg.payload.decode() == "H9":
                playjbh.play(9)
                print "Playing H9"
        elif msg.payload.decode() == "H10":
                playjbh.play(10)
                print "Playing H10"
        elif msg.payload.decode() == "H11":
                playjbh.play(11)
                print "Playing H11"
        elif msg.payload.decode() == "H12":
                playjbh.play(12)
                print "Playing H12"
        elif msg.payload.decode() == "H13":
                playjbh.play(13)
                print "Playing H13"
        elif msg.payload.decode() == "H14":
                playjbb.play(14)
                print "Playing H14"
        elif msg.payload.decode() == "H15":
                playjbh.play(15)
                print "Playing H15"
        elif msg.payload.decode() == "H16":
                playjbh.play(16)
                print "Playing H16"
        elif msg.payload.decode() == "H17":
                playjbh.play(17)
                print "Playing H17"
        elif msg.payload.decode() == "H18":
                playjbh.play(18)
                print "Playing H18"
        elif msg.payload.decode() == "H19":
                playjbh.play(19)
                print "Playing H19"
        elif msg.payload.decode() == "H20":
                playjbh.play(20)
                print "Playing H20"       
        elif msg.payload.decode() == "J1":
                playjbj.play(1)
                print "Playing J1"
        elif msg.payload.decode() == "J2":
                playjbj.play(2)
                print "Playing J2"
        elif msg.payload.decode() == "J3":
                playjbj.play(3)
                print "Playing J3"
        elif msg.payload.decode() == "J4":
                playjbj.play(4)
                print "Playing J4"
        elif msg.payload.decode() == "J5":
                playjbj.play(5)
                print "Playing J5"
        elif msg.payload.decode() == "J6":
                playjbj.play(6)
                print "Playing B6"
        elif msg.payload.decode() == "J7":
                playjbj.play(7)
                print "Playing J7"
        elif msg.payload.decode() == "J8":
                playjbj.play(8)
                print "Playing J8"
        elif msg.payload.decode() == "J9":
                playjbj.play(9)
                print "Playing J9"
        elif msg.payload.decode() == "J10":
                playjbj.play(10)
                print "Playing J10"
        elif msg.payload.decode() == "J11":
                playjbj.play(11)
                print "Playing J11"
        elif msg.payload.decode() == "J12":
                playjbj.play(12)
                print "Playing J12"
        elif msg.payload.decode() == "J13":
                playjbj.play(13)
                print "Playing J13"
        elif msg.payload.decode() == "J14":
                playjbb.play(14)
                print "Playing J14"
        elif msg.payload.decode() == "J15":
                playjbj.play(15)
                print "Playing J15"
        elif msg.payload.decode() == "J16":
                playjbj.play(16)
                print "Playing J16"
        elif msg.payload.decode() == "J17":
                playjbj.play(17)
                print "Playing J17"
        elif msg.payload.decode() == "J18":
                playjbj.play(18)
                print "Playing J18"
        elif msg.payload.decode() == "J19":
                playjbj.play(19)
                print "Playing J19"
        elif msg.payload.decode() == "J20":
                playjbj.play(20)
                print "Playing J20"    
        elif msg.payload.decode() == "K1":
                playjbk.play(1)
                print "Playing K1"
        elif msg.payload.decode() == "K2":
                playjbk.play(2)
                print "Playing K2"
        elif msg.payload.decode() == "K3":
                playjbk.play(3)
                print "Playing K3"
        elif msg.payload.decode() == "K4":
                playjbk.play(4)
                print "Playing K4"
        elif msg.payload.decode() == "K5":
                playjbk.play(5)
                print "Playing K5"
        elif msg.payload.decode() == "K6":
                playjbk.play(6)
                print "Playing B6"
        elif msg.payload.decode() == "K7":
                playjbk.play(7)
                print "Playing K7"
        elif msg.payload.decode() == "K8":
                playjbk.play(8)
                print "Playing K8"
        elif msg.payload.decode() == "K9":
                playjbk.play(9)
                print "Playing K9"
        elif msg.payload.decode() == "K10":
                playjbk.play(10)
                print "Playing K10"
        elif msg.payload.decode() == "K11":
                playjbk.play(11)
                print "Playing K11"
        elif msg.payload.decode() == "K12":
                playjbk.play(12)
                print "Playing K12"
        elif msg.payload.decode() == "K13":
                playjbk.play(13)
                print "Playing K13"
        elif msg.payload.decode() == "K14":
                playjbb.play(14)
                print "Playing K14"
        elif msg.payload.decode() == "K15":
                playjbk.play(15)
                print "Playing K15"
        elif msg.payload.decode() == "K16":
                playjbk.play(16)
                print "Playing K16"
        elif msg.payload.decode() == "K17":
                playjbk.play(17)
                print "Playing K17"
        elif msg.payload.decode() == "K18":
                playjbk.play(18)
                print "Playing K18"
        elif msg.payload.decode() == "K19":
                playjbk.play(19)
                print "Playing K19"
        elif msg.payload.decode() == "K20":
                playjbk.play(20)
                print "Playing K20"                                                                          
	else:
		print msg.payload


# Initiate MQTT Client
mqttc = mqtt.Client()

# Register Event Handlers
mqttc.on_message = on_message
mqttc.on_connect = on_connect
mqttc.on_subscribe = on_subscribe

# Connect with MQTT Broker
mqttc.connect(MQTT_BROKER, MQTT_PORT, MQTT_KEEPALIVE_INTERVAL )

# Continue the network loop
mqttc.loop_forever()
