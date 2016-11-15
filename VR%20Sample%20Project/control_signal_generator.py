import time
import math
from pylsl import StreamInfo, StreamOutlet

info = StreamInfo('BioControlSignal','Sine',1,100,'float32','uid98765')

outlet = StreamOutlet(info)

initialTime = time.time()
print("Beginning to send data...")

while True:
    startTime = time.time()
    timeA = startTime-initialTime
    ampSineVal = [math.sin(0.5*timeA)]
    #print(ampSineVal)
    outlet.push_sample(ampSineVal)
    time.sleep(0.01)