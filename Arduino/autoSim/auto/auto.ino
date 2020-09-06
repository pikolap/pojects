
#include <Joystick.h>
 int bitJedna = 2;
int bitDva = 5;
int bitTri = 4;
int bitCtyri = 3;
void setup()

{pinMode(A0, INPUT);
pinMode(9,OUTPUT);
pinMode(8,OUTPUT);
 digitalWrite(9,LOW);
  digitalWrite(8,LOW);
   Joystick.begin();
     Serial.begin(9600);
  pinMode(bitJedna, OUTPUT);
  pinMode(bitDva, OUTPUT);
  pinMode(bitTri, OUTPUT);
  pinMode(bitCtyri, OUTPUT);
  
    digitalWrite(bitJedna, LOW);
  digitalWrite(bitDva, LOW);
  digitalWrite(bitTri, LOW);
  digitalWrite(bitCtyri, LOW);
  delay(1000);
          digitalWrite(bitJedna, HIGH);
digitalWrite(bitDva, LOW);
digitalWrite(bitTri, LOW);
digitalWrite(bitCtyri, LOW);
delay(1000);
        digitalWrite(bitJedna, LOW);
digitalWrite(bitDva, HIGH);
digitalWrite(bitTri, LOW);
digitalWrite(bitCtyri, LOW);
delay(1000);
            digitalWrite(bitJedna, HIGH);
digitalWrite(bitDva, HIGH);
digitalWrite(bitTri, LOW);
digitalWrite(bitCtyri, LOW); 
delay(1000);
        digitalWrite(bitJedna, LOW);
digitalWrite(bitDva, LOW);
digitalWrite(bitTri, HIGH);
digitalWrite(bitCtyri, LOW);
delay(1000);
        digitalWrite(bitJedna, HIGH);
digitalWrite(bitDva, LOW);
digitalWrite(bitTri, HIGH);
digitalWrite(bitCtyri, LOW);
 delay(1000);
digitalWrite(bitJedna, LOW);
digitalWrite(bitDva, HIGH);
digitalWrite(bitTri, HIGH);
digitalWrite(bitCtyri, LOW);
delay(1000);
        digitalWrite(bitJedna, HIGH);
digitalWrite(bitDva, HIGH);
digitalWrite(bitTri, HIGH);
digitalWrite(bitCtyri, LOW);
   delay(1000);
        digitalWrite(bitJedna, LOW);
digitalWrite(bitDva, LOW);
digitalWrite(bitTri, LOW);
digitalWrite(bitCtyri, HIGH);
   delay(1000);
        digitalWrite(bitJedna, HIGH);
digitalWrite(bitDva, LOW);
digitalWrite(bitTri, LOW);
digitalWrite(bitCtyri, HIGH);}

const int pinToButtonMap = A0;

void loop()

{int pot = analogRead(A0);
Joystick.releaseButton(1);
Joystick.releaseButton(2);
int mapped = map(pot,0,1023,0,255);
{Joystick.setThrottle(mapped);}
  if(digitalRead(8) == HIGH){
  Joystick.pressButton(1);
  }
  if(digitalRead(9) == HIGH){
  Joystick.pressButton(2);
  }
  Serial.flush();
  if(Serial.available()){
  switch(Serial.read()){
    case 48:
  digitalWrite(bitJedna, LOW);
  digitalWrite(bitDva, LOW);
  digitalWrite(bitTri, LOW);
  digitalWrite(bitCtyri, LOW);
    break;
        case 49: //1
        digitalWrite(bitJedna, HIGH);
digitalWrite(bitDva, LOW);
digitalWrite(bitTri, LOW);
digitalWrite(bitCtyri, LOW);
    break;
            case 50: //2
        digitalWrite(bitJedna, LOW);
digitalWrite(bitDva, HIGH);
digitalWrite(bitTri, LOW);
digitalWrite(bitCtyri, LOW);
    break;
            case 51: //3
            digitalWrite(bitJedna, HIGH);
digitalWrite(bitDva, HIGH);
digitalWrite(bitTri, LOW);
digitalWrite(bitCtyri, LOW);   
    break;
            case 52: //4
        digitalWrite(bitJedna, LOW);
digitalWrite(bitDva, LOW);
digitalWrite(bitTri, HIGH);
digitalWrite(bitCtyri, LOW);
    break;
            case 53: //5
        digitalWrite(bitJedna, HIGH);
digitalWrite(bitDva, LOW);
digitalWrite(bitTri, HIGH);
digitalWrite(bitCtyri, LOW);
    break;
            case 54: //6
digitalWrite(bitJedna, LOW);
digitalWrite(bitDva, HIGH);
digitalWrite(bitTri, HIGH);
digitalWrite(bitCtyri, LOW);
    break;
            case 55:
        digitalWrite(bitJedna, HIGH);
digitalWrite(bitDva, HIGH);
digitalWrite(bitTri, HIGH);
digitalWrite(bitCtyri, LOW);
    break;
            case 56:
        digitalWrite(bitJedna, LOW);
digitalWrite(bitDva, LOW);
digitalWrite(bitTri, LOW);
digitalWrite(bitCtyri, HIGH);
    break;
            case 57:
        digitalWrite(bitJedna, HIGH);
digitalWrite(bitDva, LOW);
digitalWrite(bitTri, LOW);
digitalWrite(bitCtyri, HIGH);

    break;
            case 58:
        digitalWrite(bitJedna, LOW);
digitalWrite(bitDva, HIGH);
digitalWrite(bitTri, HIGH);
digitalWrite(bitCtyri, HIGH);
    break;

}}}
