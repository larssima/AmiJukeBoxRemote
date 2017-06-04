#!/usr/bin/python
import RPi.GPIO as GPIO
import time

GPIO.setmode(GPIO.BCM)

# init list with pin numbers


pinList = [17]

for i in pinList:
    GPIO.setup(i, GPIO.OUT)
    GPIO.output(i, GPIO.LOW)

# time to sleep between operations in the main loop

SleepTimeL = 0.4


try:
  GPIO.output(17, GPIO.HIGH)
  time.sleep(SleepTimeL);
  GPIO.output(17, GPIO.LOW)
  GPIO.output(17, GPIO.HIGH)
  time.sleep(SleepTimeL);
  GPIO.output(17, GPIO.LOW)
  GPIO.output(17, GPIO.HIGH)
  time.sleep(SleepTimeL);
  GPIO.output(17, GPIO.LOW)
  GPIO.output(17, GPIO.HIGH)
  time.sleep(SleepTimeL);
  GPIO.output(17, GPIO.LOW)

  GPIO.cleanup()
except KeyboardInterrupt:
  print "QUIT"


# End program cleanly with keyboard
except KeyboardInterrupt:
  print "  Quit"

  # Reset GPIO settings
  GPIO.cleanup()

# find more information on this script at
# http://youtu.be/WpM1aq4B8-A
