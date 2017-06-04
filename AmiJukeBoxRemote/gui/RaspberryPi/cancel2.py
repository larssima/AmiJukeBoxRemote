#!/usr/bin/python
import RPi.GPIO as GPIO 
import time 


def cancelrecord():
	GPIO.setmode(GPIO.BCM)
	# init list with pin numbers
	pinList = [4]

	for i in pinList:
	    GPIO.setup(i, GPIO.OUT)
	    GPIO.output(i, GPIO.LOW)


	# time to sleep between operations in the main loop
	SleepTimeL = 2
	# main loop
	try:
	  GPIO.output(4, GPIO.HIGH)
	  time.sleep(SleepTimeL);
	  print "CANCEL"
	  GPIO.output(4, GPIO.LOW)
	  GPIO.cleanup()
	except KeyboardInterrupt:
	  print "QUIT"
