#include <EEPROM.h>
#include <Adafruit_BME280.h>
#include <Adafruit_Sensor.h>
#include <DS3231.h>
#include <Nextion.h>
#include <Wire.h>

bool EnableRefresh = true;                            //Define some variables
int CurrentSaveAddr = 1;                              
int StoreCounter = 0;
byte RTCBuffer = 0;
int Timer = 0;;
int Temp[336];
int Press[336];
int Hum[336];
int MaxTemp = 40;
int MinTemp = -40;
int MaxPress = 0;
int MinPress = 0;
uint32_t NormalPress = 0;
uint32_t GaugePos = 0;
uint32_t Buffer = 0;

Adafruit_BME280 BME280;                               //Define BME280 I2C sensor

DS3231 RTC;                                           //Define Real Time Clock   
bool h12;
bool PM; 
bool Century;                 
int IndoorTemp;                                       //Define climate data variables

int IndoorPress;
int IndoorHum;
int OutdoorTemp;  
int OutdoorPress;                       
int OutdoorPressH;
int OutdoorPressL;
int OutdoorHum;         

NexNumber InTemp    =  NexNumber(0, 8, "n0");         //Define touchscreen objects
NexNumber InPress   =  NexNumber(0, 9, "n1");  
NexNumber InHum     =  NexNumber(0, 10, "n2");   
NexNumber OutTemp   =  NexNumber(0, 12, "n3"); 
NexNumber OutPress  =  NexNumber(0, 13, "n4");
NexNumber OutHum    =  NexNumber(0, 16, "n5");
NexNumber Year      =  NexNumber(1, 6, "n6");
NexNumber Month     =  NexNumber(1, 8, "n7");
NexNumber Day       =  NexNumber(1, 10, "n8");
NexNumber Hour      =  NexNumber(1, 3, "n9");
NexNumber Min       =  NexNumber(1, 5, "n10");
NexNumber DispHour  =  NexNumber(0, 19, "n11");
NexNumber DispMin   =  NexNumber(0, 18, "n12");
NexNumber MinValue  =  NexNumber(2, 11, "n13"); 
NexNumber MidValue  =  NexNumber(2, 12, "n14");  
NexNumber MaxValue  =  NexNumber(2, 13, "n15");
NexNumber DefaultPress        =  NexNumber(1, 20, "n16");
NexGauge Forecast             =  NexGauge(0, 2, "z0");   
NexWaveform Statistics        =  NexWaveform(2, 3, "s0");
NexHotspot ButtonSettings     =  NexHotspot(0, 3, "m0");
NexHotspot ButtonStatistics   =  NexHotspot(0, 4, "m1");
NexHotspot ButtonSaveSettings =  NexHotspot(1, 17, "m5");
NexHotspot ButtonCancel       =  NexHotspot(1, 2, "m9");
NexHotspot ButtonTemp         =  NexHotspot(2, 4, "m6");
NexHotspot ButtonPressure     =  NexHotspot(2, 5, "m7");
NexHotspot ButtonHum          =  NexHotspot(2, 6, "m8");
NexHotspot ButtonBack         =  NexHotspot(2, 2, "m10");

NexTouch *nex_listen_list[] =                         //Define touchscreen objects to listen
{
  &ButtonSettings,
  &ButtonStatistics,
  &ButtonSaveSettings,
  &ButtonCancel,
  &ButtonTemp,
  &ButtonPressure,
  &ButtonHum,
  &ButtonBack,
  NULL
};

void ButtonSettingsPushCallback(void *ptr)            //Display settings on touchscreen when push the button "Settings"
{
  RTCBuffer = 0;
  EnableRefresh = false;
  Serial3.write(240);
  sendCommand("page 1");
  delay(200);
  RTCBuffer = RTC.getYear();
  Year.setValue(RTCBuffer + 2000);
  RTCBuffer = RTC.getMonth(Century);
  Month.setValue(RTCBuffer);
  RTCBuffer = RTC.getDate();
  Day.setValue(RTCBuffer);
  RTCBuffer = RTC.getHour(h12, PM);
  Hour.setValue(RTCBuffer);
  RTCBuffer = RTC.getMinute();
  Min.setValue(RTCBuffer);
  NormalPress = EEPROM.read(4000) * 256 + EEPROM.read(4001);
  DefaultPress.setValue(NormalPress);
}

void ButtonSaveSettingsPushCallback(void *ptr)        //Save date and time to RTC
{  
  Buffer = 0;
  Year.getValue(&Buffer);
  RTC.setYear(Buffer - 2000);
  Month.getValue(&Buffer);
  RTC.setMonth(Buffer);
  Day.getValue(&Buffer);
  RTC.setDate(Buffer);
  Hour.getValue(&Buffer);
  RTC.setHour(Buffer);
  Min.getValue(&Buffer);
  RTC.setMinute(Buffer);
  RTC.setSecond(0);    
  DefaultPress.getValue(&NormalPress);                //Save local normal air pressure to EEPROM
  EEPROM.write(4000, NormalPress / 256);
  EEPROM.write(4001, NormalPress % 256);
  delay(200);
  sendCommand("page 0");
  EnableRefresh = true;
  Serial3.write(15);
}

void ButtonCancelPushCallback(void *ptr)              //Leave the Settings page
{
  sendCommand("page 0");
  delay(200);
  EnableRefresh = true;
  Serial3.write(15);
}

void ButtonBackPushCallback(void *ptr)                //Leave the Statistics page
{
  sendCommand("page 0");
  delay(200);
  EnableRefresh = true;
  Serial3.write(15);
}

void ButtonStatisticsPushCallback(void *ptr)          //Display Statistics page
{
  EnableRefresh = false;
  Serial3.write(240);
  delay(200);
  sendCommand("page 2");
  delay(200);
  sendCommand("tsw m6,0");
  sendCommand("tsw m7,0");
  sendCommand("tsw m8,0");
  sendCommand("tsw m10,0");
  LoadStatistics();
  delay(200);  
  DrawTempDiag();
  delay(200);
  sendCommand("tsw m6,1");
  sendCommand("tsw m7,1");
  sendCommand("tsw m8,1");
  sendCommand("tsw m10,1");
}

void LoadStatistics()
{  
  int Offset = 0;
  for (int i = 0; i <= 335; i++)                     //Load climate data from EEPROM sorted by the order of the measurement
    {
      if ((CurrentSaveAddr - 1 - i) > -1) Offset = 0;
      else Offset = 336;
      Temp[335 - i] = EEPROM.read(Offset + CurrentSaveAddr - 1 - i) - 100;
      Press[335 - i] = (EEPROM.read(Offset + CurrentSaveAddr - 1 - i + 500) * 256) + EEPROM.read(Offset + CurrentSaveAddr - 1 - i + 1000);
      Hum[335 - i] = EEPROM.read(Offset + CurrentSaveAddr - 1 - i + 1500);
    } 
  MaxPress = NormalPress + 35;
  MinPress = NormalPress - 35;
}

void ButtonTempPushCallback(void *ptr)           //Display temperature statistics on touchscreen
{
  delay(300);
  sendCommand("tsw m6,0");
  sendCommand("tsw m7,0");
  sendCommand("tsw m8,0");
  sendCommand("tsw m10,0");
  DrawTempDiag();
  delay(200);
  sendCommand("tsw m6,1");
  sendCommand("tsw m7,1");
  sendCommand("tsw m8,1");
  sendCommand("tsw m10,1");  
}

void ButtonPressurePushCallback(void *ptr)       //Display air pressure statistics on touchscreen
{
  delay(300);
  sendCommand("tsw m6,0");
  sendCommand("tsw m7,0");
  sendCommand("tsw m8,0");
  sendCommand("tsw m10,0");  
  DrawPressureDiag(); 
  delay(200);
  sendCommand("tsw m6,1");
  sendCommand("tsw m7,1");
  sendCommand("tsw m8,1");
  sendCommand("tsw m10,1");   
}

void ButtonHumPushCallback(void *ptr)            //Display humidity statistics on touchscreen
{
  delay(300);
  sendCommand("tsw m6,0");
  sendCommand("tsw m7,0");
  sendCommand("tsw m8,0");
  sendCommand("tsw m10,0");  
  DrawHumDiag();
  delay(200);
  sendCommand("tsw m6,1");
  sendCommand("tsw m7,1");
  sendCommand("tsw m8,1");
  sendCommand("tsw m10,1");  
}

void DrawTempDiag()                                                         //Draw temperature diagram on touchscreen beetween lower and upper boundary line
{
  sendCommand("n13.val=-40");
  sendCommand("n14.val=0");
  sendCommand("n15.val=40");
  for (int i = 0; i <= 335; i++)                
    {
    Buffer = ((232 * (Temp[i] - MinTemp)) / (MaxTemp - MinTemp)) + 12;      //Transform the range of the values to the range of the displaying
    Statistics.addValue(0, Buffer);
    Statistics.addValue(0, Buffer);
    }    
}

void DrawPressureDiag()                                                     //Draw air pressure diagram on touchscreen beetween lower and upper boundary line
{    
  MinValue.setValue(MinPress);
  MidValue.setValue(NormalPress);
  MaxValue.setValue(MaxPress);  
  for (int i = 0; i <= 335; i++)                
    {
    Buffer = ((232 * (Press[i] - MinPress)) / (MaxPress - MinPress)) + 12;  //Transform the range of the values to the range of the displaying 
    Statistics.addValue(0, Buffer);
    Statistics.addValue(0, Buffer);
    }                                          
}

void DrawHumDiag()                                                          //Draw humidity diagram on touchscreen beetween lower and upper boundary line
{
  sendCommand("n13.val=5");
  sendCommand("n14.val=50");
  sendCommand("n15.val=95");  
  for (int i = 0; i <= 335; i++)                
    {
    Buffer = 2.56 * Hum[i];                                                 //Transform the range of the values to the range of the displaying 
    Statistics.addValue(0, Buffer);
    Statistics.addValue(0, Buffer);
    }                                          
}

void setup(void)
{
  Serial3.begin(9600);                                              //Initialize serial-3 port for transfer data from outdoor unit
  Serial2.begin(9600);                                              //Initialize serial-2 port for touchscreen                                        
   
  nexInit();                                                        //Initialize touchscreen
  ButtonSettings.attachPush(ButtonSettingsPushCallback, &ButtonSettings);
  ButtonStatistics.attachPush(ButtonStatisticsPushCallback, &ButtonStatistics);
  ButtonSaveSettings.attachPush(ButtonSaveSettingsPushCallback, &ButtonSaveSettings);
  ButtonTemp.attachPush(ButtonTempPushCallback, &ButtonTemp);
  ButtonPressure.attachPush(ButtonPressurePushCallback, &ButtonPressure);
  ButtonHum.attachPush(ButtonHumPushCallback, &ButtonHum);
  ButtonCancel.attachPush(ButtonCancelPushCallback, &ButtonCancel);
  ButtonBack.attachPush(ButtonBackPushCallback, &ButtonBack);
  
  BME280.begin(0x76);                                               //Start BME280 sensor at I2C address 0x76

  RTC.setClockMode(false);                                          //Set RTC to 24h mode

  CurrentSaveAddr = EEPROM.read(2000) * 256 + EEPROM.read(2001);    //Set CurrentSaveAddress
  if (CurrentSaveAddr >= 336)
    {
      CurrentSaveAddr = 0;
      EEPROM.write(2000, 0);              
      EEPROM.write(2001, 0);
    }
    
  NormalPress = 1013;                                               //Set local normal air pressure
  EEPROM.write(4000, 3);
  EEPROM.write(4001, 245);
  DefaultPress.setValue(1013);
}

void loop(void)
{
  nexLoop(nex_listen_list);                             //Listen touchscreen objects
  delay(10);  
  Timer++;
  if ((Timer >= 100) && (EnableRefresh == true))        //Speed up the reaction of the touchscreen
    { 
      Timer = 0;
      DispHour.setValue(RTC.getHour(h12, PM));          //Display time on screen
      DispMin.setValue(RTC.getMinute());

      IndoorTemp = int(BME280.readTemperature());       //Get climate data from indoor sensor
      IndoorPress = int(BME280.readPressure() / 100);
      IndoorHum = int(BME280.readHumidity());

      InTemp.setValue(IndoorTemp);                      //Display indoor climate data on screen
      InPress.setValue(IndoorPress);
      InHum.setValue(IndoorHum);

      if (Serial3.available())                          //Get climate data from outdoor sensor over RS232 port
        {
          OutdoorTemp = Serial3.read() - 100;                 
          OutdoorPressH = Serial3.read();
          OutdoorPressL = Serial3.read();
          OutdoorHum = Serial3.read();
          while (Serial3.available() > 0) Serial3.read();         //Erase serial buffer
          OutdoorPress = OutdoorPressH * 256 + OutdoorPressL;
        }
      else
        {
          OutdoorTemp = 0;             
          OutdoorPress = 0;
          OutdoorHum = 0;  
        }
      OutTemp.setValue(OutdoorTemp);                    //Display outdoor climate data on screen
      OutPress.setValue(OutdoorPress);
      OutHum.setValue(OutdoorHum);

      StoreCounter++;
      if (StoreCounter >= 3600)                         //Save outdoor climate data in every hour in EEPROM; a total of 336 hours (14 days)
        {
          StoreCounter = 0;
          CurrentSaveAddr = EEPROM.read(2000) * 256 + EEPROM.read(2001);          //Read current start address to save data  
          EEPROM.write(CurrentSaveAddr, OutdoorTemp + 100);                       //Save Temp in one byte
          EEPROM.write(CurrentSaveAddr + 500, OutdoorPressH);                     //Save Press in two byte
          EEPROM.write(CurrentSaveAddr + 1000, OutdoorPressL);
          EEPROM.write(CurrentSaveAddr + 1500, OutdoorHum);                       //Save Hum in one byte
          CurrentSaveAddr++;                                                      
          if (CurrentSaveAddr >= 336) CurrentSaveAddr = 0;                        //Save just 14 days of data                                            
          EEPROM.write(2000, CurrentSaveAddr / 256);                              //Save current start address due to power failure
          EEPROM.write(2001, CurrentSaveAddr % 256);
        } 
                                                                                  //Calculate weather forecast based on value of the air pressure                                                                  
      if (OutdoorPress < NormalPress - 2) GaugePos = 135;         
      else if ((OutdoorPress >= NormalPress - 2) && (OutdoorPress <= NormalPress - 1)) GaugePos = 113; 
      else if ((OutdoorPress > NormalPress - 1) && (OutdoorPress < NormalPress + 1)) GaugePos = 90;
      else if ((OutdoorPress >= NormalPress - 1) && (OutdoorPress <= NormalPress + 2)) GaugePos = 57;
      else if (OutdoorPress > NormalPress + 2) GaugePos = 45;
      Forecast.getValue(&Buffer);
      if (GaugePos != Buffer) Forecast.setValue(GaugePos);
    }
}
