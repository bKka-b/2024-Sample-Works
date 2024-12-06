#include <IRremote.hpp>

const int RECV_PIN = 3;
int LED1 = 9;
int LED2 = 12;
int LED3 = 11;
int LED4 = 10;

String address = "";
String motionCode = "";
int numberOfRepeats;


decode_results results;

void setup(){
  Serial.begin(9600);
  IrReceiver.begin(RECV_PIN, ENABLE_LED_FEEDBACK);
  pinMode(LED1, OUTPUT);
  pinMode(LED2, OUTPUT);
  pinMode(LED3, OUTPUT);
  pinMode(LED4, OUTPUT);
}

void loop(){
  if (IrReceiver.decode()){
    Serial.println(IrReceiver.decodedIRData.decodedRawData, HEX); // Print "old" raw data

    IrReceiver.printIRResultShort(&Serial); // Print complete received data in one line
    IrReceiver.printIRSendUsage(&Serial); // Print the statement required to send this data
    
    delay(500);
    if (IrReceiver.decodedIRData.protocol == MAGIQUEST) {
      motionCode = IrReceiver.decodedIRData.command;
      Serial.print("Motion Code is ");
      Serial.print(motionCode);
      if (motionCode == "259") {
          power();
      }
      else if (motionCode == "1") {
          swishFlick();
      }
      else if (motionCode == "258") {
          outies();
      }
      else if (motionCode == "2580") {
          response();
      }
      else if (motionCode == "258ED3EE5B7") {
          response();
      }
      else if (motionCode == "10") {
          response();
      }
      else {
          response();
      }
    }
  }


        IrReceiver.resume();
  }


void swishFlick() {
  digitalWrite(LED1, HIGH);
  digitalWrite(LED4, HIGH);
   delay(250);

  digitalWrite(LED4, LOW);
  digitalWrite(LED1, LOW);


   digitalWrite(LED2, HIGH);
   digitalWrite(LED3, HIGH);
   delay(250);

   digitalWrite(LED2, LOW);
   digitalWrite(LED3, LOW);
}

void outies() {
  digitalWrite(LED1, HIGH);
  digitalWrite(LED4, HIGH);

    delay(250);
  
  digitalWrite(LED1, LOW);
  digitalWrite(LED4, LOW);
}

void response() {
  digitalWrite(LED1, HIGH);
  digitalWrite(LED2, HIGH);
  digitalWrite(LED3, HIGH);
  digitalWrite(LED4, HIGH);

  delay(250);

  digitalWrite(LED1, LOW);
  digitalWrite(LED2, LOW);
  digitalWrite(LED3, LOW);
  digitalWrite(LED4, LOW);

  delay(250);

}

int i = 0;
void onOff() {
  if (i == 0) {
  digitalWrite(LED1, HIGH);
  digitalWrite(LED2, HIGH);
  digitalWrite(LED3, HIGH);
  digitalWrite(LED4, HIGH);

  i++;
  }
  else {
  digitalWrite(LED1, LOW);
  digitalWrite(LED2, LOW);
  digitalWrite(LED3, LOW);
  digitalWrite(LED4, LOW);
  i=0;

  delay(500);
  }
}

void power() //trigger the optocoupleur to shorten the power-switch and then turn ON/OFF the computer
{
   digitalWrite(LED1, HIGH);
   delay(500);
   digitalWrite(LED2, HIGH);
   delay(500);
   digitalWrite(LED3, HIGH);
   delay(500);
   digitalWrite(LED4, HIGH);
   delay(1000);

   digitalWrite(LED4, LOW);
   delay(500);
   digitalWrite(LED3, LOW);
   delay(500);
   digitalWrite(LED2, LOW);
   delay(500);
   digitalWrite(LED1, LOW);
   delay(500);
}