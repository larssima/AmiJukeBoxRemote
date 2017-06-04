#!/usr/bin/python
import RPi.GPIO as GPIO 
import time 

GPIO.setmode(GPIO.BCM) 

pin = 17 
first = 1 
gap = 1 
second = 1


firstSleepTimeOn = 0.06 
firstSleepTimeOff = 0.06

secondSleepTimeOn = 0.06 
secondSleepTimeOff = 0.06 


stopbitTimeOn = 0.02
stopbitTimeOff = 0.06

gapSleepTime = 0.1

firstCount = 3 
secondCount = 3 

GPIO.setup(pin,GPIO.OUT) 

if (first == 1):
	for x in xrange(firstCount):
		GPIO.output(pin, GPIO.LOW)
		time.sleep(firstSleepTimeOn);
		GPIO.output(pin, GPIO.HIGH)
		time.sleep(firstSleepTimeOff);


# wait for second string
if (gap==1):
	GPIO.output(pin, GPIO.LOW)
	time.sleep(gapSleepTime); 

if (second == 1):
	for x in xrange(secondCount):
		GPIO.output(pin, GPIO.LOW)
		time.sleep(secondSleepTimeOn);
		GPIO.output(pin, GPIO.HIGH)
		time.sleep(secondSleepTimeOff); 


GPIO.cleanup()
