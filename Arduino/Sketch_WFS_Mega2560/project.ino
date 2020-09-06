
#include <Wire.h>
#include <LiquidCrystal_I2C.h> //https://bitbucket.org/fmalpartida/new-liquidcrystal/downloads/
#include <Adafruit_Sensor.h> //https://github.com/adafruit/Adafruit_Sensor
#include <Adafruit_BME280.h> //https://github.com/adafruit/Adafruit_BME280_Library
#include <ds3231.h> //https://github.com/rodan/ds3231


float temperature;
float humidity;
float pressure;

#define ALTITUDE 270.0 // Altitude in Havířov

Adafruit_BME280 bme; // I2C

LiquidCrystal_I2C lcd(0x27,2,1,0,4,5,6,7,3,POSITIVE);


uint8_t time[8];
unsigned int recv_size = 0;
unsigned long prev, interval = 1000;

void setup(void) {
  Serial.begin(9600);
  Wire.begin();
  lcd.begin(16, 2);
  lcd.print("Reading sensor");

  bool status;
    
    // default settings
    status = bme.begin(0x76);  //The I2C address of the BME280 sensor is 0x76
    if (!status) {
        lcd.clear();
        lcd.print("Error. Check");
        lcd.setCursor(0,1);
        lcd.print("connections");
        while (1);
    }
}

void loop() {
  
 delay(2000);

 getPressure();
 getHumidity();
 getTemperature();
 
 lcd.clear(); 
 
 //Printing Temperature
 String temperatureString = String(temperature,1);
 lcd.print("T:"); 
 lcd.write(temperatureString);
 lcd.print((char)223);
 lcd.print("C ");
 
 //Printing Humidity
 String humidityString = String(humidity,0); 
 lcd.print("H: ");
 lcd.print(humidityString);
 lcd.print("%");
 
 //Printing Pressure
 lcd.setCursor(0,1);
 lcd.print("P: ");
 String pressureString = String(pressure,2);
 lcd.print(pressureString);
 lcd.print(" hPa");


    unsigned long now = millis();
    struct ts t;

    // show time
    if ((now - prev > interval) && (Serial.available() <= 0)) {
        DS3231_get(&t); //Get time

        lcd.setCursor(23,0);
        
        lcd.print(t.mday);
        
        printMonth(t.mon);
        
        lcd.print(t.year);
        
        lcd.setCursor(27,1);
        lcd.print(t.hour);
        lcd.print(":");
        if(t.min<10)
        {
          lcd.print("0");
        }
        lcd.print(t.min);
        prev = now;
    }
 
}

float getTemperature()
{
  temperature = bme.readTemperature();
}

float getHumidity()
{
  humidity = bme.readHumidity();
}

float getPressure()
{
  pressure = bme.readPressure();
  pressure = bme.seaLevelForAltitude(ALTITUDE,pressure);
  pressure = pressure/100.0F;
}

void printMonth(int month)
{
  switch(month)
  {
    case 1: lcd.print(" January ");break;
    case 2: lcd.print(" February ");break;
    case 3: lcd.print(" March ");break;
    case 4: lcd.print(" April ");break;
    case 5: lcd.print(" May ");break;
    case 6: lcd.print(" June ");break;
    case 7: lcd.print(" July ");break;
    case 8: lcd.print(" August ");break;
    case 9: lcd.print(" September ");break;
    case 10: lcd.print(" October ");break;
    case 11: lcd.print(" November ");break;
    case 12: lcd.print(" December ");break;
    default: lcd.print(" Error ");break;
  } 
}
