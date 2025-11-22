#!/usr/bin/python
import RPi.GPIO as GPIO 
import time 

GPIO.setmode(GPIO.BCM) 

pin = 17 
first = 1 
gap = 1 
second = 1
endpulse = 0 
stopbit = 0


firstSleepTimeOn = 0.02 
firstSleepTimeOff = 0.04

secondSleepTimeOn = 0.02 
secondSleepTimeOff = 0.04 

endPulseTimeOn = 0.1
endPulseTimeOff = 0.6

stopbitTimeOn = 0.02
stopbitTimeOff = 0.06

gapSleepTime = 0.1

def a1():
	firstCount = 1 
	secondCount = 2 

	GPIO.setup(pin,GPIO.OUT) 

	if (first == 1):
		for x in xrange(firstCount):
			GPIO.output(pin, GPIO.LOW)
			time.sleep(firstSleepTimeOn);
			GPIO.output(pin, GPIO.HIGH)
			time.sleep(firstSleepTimeOff);
	if (stopbit == 1):
		GPIO.output(pin, GPIO.LOW)
		time.sleep(stopbitTimeOn);
		GPIO.output(pin, GPIO.HIGH)
		time.sleep(stopbitTimeOff);
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

	if (endpulse == 1):
		GPIO.output(pin, GPIO.LOW)
		time.sleep(endPulseTimeOn);
		GPIO.output(pin, GPIO.HIGH)
		time.sleep(endPulseTimeOff);


	GPIO.cleanup()
