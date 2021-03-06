import socket
import sys
import os
from enum import Enum;
import pyttsx3

# Code From Client python
# C# is Server



class Unisex (Enum):
    Male=0
    FeMale=1
def Process():
    voiceSex=Unisex.FeMale
    engineer =pyttsx3.init()
    rate =120
    voices = engineer.getProperty('voices')

    HOST ='127.0.0.1'
    PORT= 8000
    s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server_address =(HOST,PORT)
    print('Connecting to %s port ... ' + str(server_address))
    try:
        s.connect(server_address)
        
        print('Connect Successfully ... ' + str(server_address))
    except:
        print('Can not connect ' + str(server_address))
        s.close()
        sys.exit()    
    try: 
        while True:
            data =s.recv(1024)
            print     ('Server sent : ',data.decode('utf8'))
            dataNew =data.decode('utf8')[:4]
            if  dataNew =="read":
                engineer.setProperty('rate',rate)
                engineer.setProperty('voice', voices[voiceSex.value].id)
                engineer.say("This is example about T C P I P communicationpython and C sharp")
                engineer.runAndWait()  
                s.sendall(bytes("Finish", "utf8"))     
    except:
        s.close()
        sys.exit()
    finally:
        s.close()
        
def Main():
    Process()
   
if __name__== "__main__":
    Main()