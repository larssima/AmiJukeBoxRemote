#!/usr/bin/python
import RPi.GPIO as GPIO 
import time 

pin = 17
first = 1
gap = 1
second = 1

# F Letter
firstCount = 7
secondCount = 8

firstSleepTimeOn = 0.02
firstSleepTimeOff = 0.04

secondSleepTimeOn = 0.04
secondSleepTimeOff = 0.06

gapSleepTime = 0.2

GPIO.setmode(GPIO.BCM)
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

