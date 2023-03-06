

// These define's must be placed at the beginning before #include "SAMDUETimerInterrupt.h"
#define TIMER_INTERRUPT_DEBUG         0
#define _TIMERINTERRUPT_LOGLEVEL_     0
#include "SAMDUETimerInterrupt.h"

#include <EF_AD9850.h>
#include <DueFlashStorage.h>


#define SWEEP_INTERVAL_US        50000   //  50 ms
#define READ_INTERVAL_US         50      //  50 us
#define START_FREQ_HZ            20
#define DISCHARGE_PIN            12
#define NUM_SAMPLES              8
#define MAS_PIN                  7      // A0
#define PAS_PIN                  6      // A1
#define MODE_WAITING             0 
#define MODE_SWEEPING            1
#define MODE_SWEPT               2
#define TEST_PIN                 3
    


EF_AD9850 AD9850(11, 10, 8, 9);  // D_CLK, FR_UQ, RST, DATA

uint16_t sweepInterruptID = 0;
uint16_t readInterruptID = 0;
volatile float freq = START_FREQ_HZ;
volatile bool dischargeSet = false;
volatile uint32_t masBufferReg[NUM_SAMPLES];
volatile uint32_t pasBufferReg[NUM_SAMPLES];
volatile uint8_t sampleCount = 0;
volatile uint32_t memAddress = 0;
volatile uint8_t mode = 0;
String buffer = "";
bool tempBuffFlag = false;

DueFlashStorage dueFlashStorage;



void setup() {
  Serial.begin(115200);
  AD9850.init();
  AD9850.reset();
  pinMode(DISCHARGE_PIN, OUTPUT);
  digitalWrite(DISCHARGE_PIN, 0);

  pinMode(TEST_PIN, OUTPUT);
  digitalWrite(TEST_PIN, 0);

  mode = MODE_WAITING;


  DueTimerInterrupt frequencySweepInterrupt = DueTimer.getAvailable();
  frequencySweepInterrupt.attachInterruptInterval(SWEEP_INTERVAL_US, sweepStep);
  sweepInterruptID = frequencySweepInterrupt.getTimerNumber();

  DueTimerInterrupt samplingInterrupt = DueTimer.getAvailable();
  samplingInterrupt.attachInterruptInterval(READ_INTERVAL_US, readAnalog);
  readInterruptID = samplingInterrupt.getTimerNumber();

  analogSetup();
  //Serial.println("Are you ready ;)");
}

void loop() {
  if (mode == MODE_WAITING && Serial.available() > 0){
    char charIn = Serial.read();
    if (charIn == 's'){
      mode = MODE_SWEEPING;
    }

  }
  else if (mode == MODE_SWEPT){ // temporary condition change later
    // Serial.println("Good job on finishing your sweep babe :)");
    // uint32_t memI = 0;
    // while (memI < memAddress){
    //   uint32_t dataDWord = 0;
    //   if (memI % 17 == 0){
    //     byte floatBin[4];
    //     floatBin[0] = dueFlashStorage.read(memI);
    //     floatBin[1] = dueFlashStorage.read(memI+1);
    //     floatBin[2] = dueFlashStorage.read(memI+2);
    //     floatBin[3] = dueFlashStorage.read(memI+3);
    //     memI += 4;
    //     float f = *( (float*) floatBin);
    //     Serial.println(" ");
    //     Serial.print(f);
    //     continue;
    //   }
    //   memI = read32BytesFromMem(&dueFlashStorage, &dataDWord, memI);
    //   Serial.print(",");
    //   Serial.print(dataDWord);
    //   Serial.print(",");
    //   Serial.print(dataDWord);
    // }
    // mode = 0;
    Serial.println("e");
    mode = MODE_WAITING;
  }
  else if (mode == MODE_SWEEPING && tempBuffFlag == false){
    Serial.print(buffer);
    if (tempBuffFlag == false){
      buffer = "";
    }
  }
}


void sweepStep() {
  if (mode != MODE_SWEEPING){
    return;
  }
  
  dischargeSet = true;   // I'm not sure if there's a better way; maybe set dischargepin bidirectional?
  digitalWrite(DISCHARGE_PIN, 1);
  
  // set new frequency
  if (freq > 30000){
    freq = START_FREQ_HZ;
    mode = MODE_SWEPT;
  }
  AD9850.wr_serial(0, freq);
  freq = freq*pow(10, 0.04);

  // load values to memory (serial)
  tempBuffFlag = true;
  for (uint8_t i = 0; i < NUM_SAMPLES; i++){
    buffer += String(freq);
    buffer += ",";
    buffer += String(masBufferReg[i]);
    buffer += ",";
    buffer += String(pasBufferReg[i]) + "\n";
  }
  tempBuffFlag = false;
  sampleCount = 0;

  digitalWrite(DISCHARGE_PIN, 0);
  dischargeSet = false;
}

void readAnalog(){
  if (mode != MODE_SWEEPING){
    return;
  }
  if (!dischargeSet && sampleCount < NUM_SAMPLES){
    masBufferReg[sampleCount] = ADC->ADC_CDR[MAS_PIN];
    pasBufferReg[sampleCount] = ADC->ADC_CDR[PAS_PIN];
    sampleCount++;
  }
}

void analogSetup(){
  // SOURCE: https://forum.arduino.cc/t/direct-accessing-of-adc-registers/468806/4

  ADC->ADC_MR |= 0x80;  // Mode FREERUN
  ADC->ADC_CR=2;        // Start converter                       
  ADC->ADC_CHER=0xC0;   // Enabling channels 6 and 7 (A0 and A1)
}


void loadRegistersToFlash(float frequency){
  digitalWrite(TEST_PIN, 1);
  memAddress = write32BytesToMem(&dueFlashStorage, *(uint32_t*)&frequency, memAddress);
  for(uint8_t i = 0; i < NUM_SAMPLES; i++){
    memAddress = write32BytesToMem(&dueFlashStorage, masBufferReg[i], memAddress);
    memAddress = write32BytesToMem(&dueFlashStorage, pasBufferReg[i], memAddress);
  }
  digitalWrite(TEST_PIN, 0);
}
  

uint32_t write32BytesToMem(DueFlashStorage *dfs, uint32_t data, uint32_t memStart){
  dfs->write(memStart, (0xff & data));
  dfs->write(memStart+1, (0xff & (data >> 8)));
  dfs->write(memStart+2, (0xff & (data >> 16)));
  dfs->write(memStart+3, (0xff & (data >> 24)));
  return memStart + 4;
}

uint32_t read32BytesFromMem(DueFlashStorage *dfs, uint32_t *data, uint32_t memStart){
  *data = dfs->read(memStart);
  *data = dfs->read(memStart+1) << 8;
  *data = dfs->read(memStart+2) << 16;
  *data = dfs->read(memStart+3) << 24;
  return memStart + 4;
}
  