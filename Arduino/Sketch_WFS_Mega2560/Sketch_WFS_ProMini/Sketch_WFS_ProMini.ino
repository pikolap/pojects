#include <Adafruit_BME280.h>
#include <Adafruit_Sensor.h>
#include <Wire.h>

Adafruit_BME280 BME280;                        //Define BME280 I2C sensor                              

int OutdoorTemp;                               //Define variables
int OutdoorPress;
int OutdoorHum;
int EnableSerial = 15;

void setup(void)
{ 
  Serial.begin(9600);                          //Initialize serial-1 port   
  BME280.begin(0x76);                          //Start BME280 sensor
}

void loop(void)
{
  OutdoorTemp = int(BME280.readTemperature());            //Get climate data from indoor sensor
  OutdoorPress = int(BME280.readPressure() / 100);
  OutdoorHum = int(BME280.readHumidity());
  if (Serial.available()) EnableSerial = Serial.read();   //Read EnableSerial value
  if (EnableSerial = 15)
    {
    Serial.write(OutdoorTemp + 100);                      //Send climate data over RS232 port                 
    Serial.write(OutdoorPress / 256);
    Serial.write(OutdoorPress % 256);
    Serial.write(OutdoorHum);
    Serial.flush();
    }
  delay(1200); 
}
