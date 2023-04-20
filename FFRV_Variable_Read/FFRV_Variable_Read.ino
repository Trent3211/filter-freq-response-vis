

// These define's must be placed at the beginning before #include "SAMDUETimerInterrupt.h"
#define TIMER_INTERRUPT_DEBUG         0
#define _TIMERINTERRUPT_LOGLEVEL_     0
#include "SAMDUETimerInterrupt.h"

#include <EF_AD9850.h>


#define SWEEP_DEC1_US        150000   //  150 ms
#define SWEEP_DEC2_US         15000   //  15  ms
#define SWEEP_DEC3_US          6000   //  1.5 ms

#define SAMP_DEC1_US          15000
#define SAMP_DEC2_US           1500
#define SAMP_DEC3_US            500 

#define START_FREQ               20
#define NUM_SAMPLES              10

#define DISCHARGE_PIN            12
#define TEST_PIN                  3
#define MAS_PIN                   7      // A0
#define PAS_PIN                   6      // A1

// led definitions
#define READY                     5
#define SWEEPING                  4
#define FAULT                     3

#define MODE_FINI                 2
#define MODE_SWEEP                1
#define MODE_WAIT                 0



EF_AD9850 AD9850(11, 10, 8, 9);    // D_CLK, FR_UQ, RST, DATA

uint16_t sweepInterruptID = 0;
uint16_t readInterruptID = 0;
uint8_t decadeNum = 0;
uint8_t mode = MODE_WAIT;


volatile uint32_t masBufferReg[NUM_SAMPLES];
volatile uint32_t pasBufferReg[NUM_SAMPLES];
volatile uint8_t sampleCount = 0;
volatile double freq = 0;
volatile bool dischargeSet = false;

String serialBuffer = "";
bool serialBufferFlag = false;




void setup() {
  Serial.begin(115200);
  AD9850.init();
  AD9850.reset();
  pinMode(DISCHARGE_PIN, OUTPUT);
  pinMode(TEST_PIN, OUTPUT);
  // led pin modes
  pinMode(READY, OUTPUT);
  pinMode(SWEEPING, OUTPUT);
  pinMode(FAULT, OUTPUT);
  
  digitalWrite(DISCHARGE_PIN, 0);
  digitalWrite(TEST_PIN, 0);
  digitalWrite(FAULT, HIGH);

  analogSetup(); // god why

  

  DueTimerInterrupt frequencySweepInterrupt = DueTimer.getAvailable();
  frequencySweepInterrupt.attachInterruptInterval(SWEEP_DEC1_US, sweepStep);
  sweepInterruptID = frequencySweepInterrupt.getTimerNumber();
  Serial.print("SweepID_START = ");
  Serial.println(sweepInterruptID);

  DueTimerInterrupt samplingInterrupt = DueTimer.getAvailable();
  samplingInterrupt.attachInterruptInterval(SAMP_DEC1_US, readAnalog);
  readInterruptID = samplingInterrupt.getTimerNumber();
  Serial.print("SampID_START = ");
  Serial.println(readInterruptID);

  while (1) {
    if (Serial.available() > 0){
      char charIn = Serial.read();
      if (charIn == '!'){
        digitalWrite(FAULT, LOW);
        digitalWrite(READY, HIGH);
        return;
      } 
    } 
  } 
}


void loop() {
  if (mode == MODE_WAIT){
    if (Serial.available() > 0){
      char charIn = Serial.read();
      if (charIn == 's'){
        mode = MODE_SWEEP;
        digitalWrite(READY, LOW);
        digitalWrite(SWEEPING, HIGH);
        digitalWrite(DISCHARGE_PIN, 0);
      } 
      else {
        return;
      }
    } 
    else { 
      return;
    }
  }
  else if (mode == MODE_FINI){
    digitalWrite(SWEEPING, LOW);
    digitalWrite(READY, HIGH);
    ffrvReset();
  }
  if (mode != MODE_SWEEP){
    return;
  }

  if (freq >= START_FREQ*100 && decadeNum < 3){
    updateSweepFrequency(SWEEP_DEC3_US);
    updateSampleFrequency(SAMP_DEC3_US);   
    decadeNum = 3;
    Serial.print("");
    //printSerialDecadeInfo(decadeNum);
  }
  else if (freq >= START_FREQ*10 && decadeNum < 2){
    updateSweepFrequency(SWEEP_DEC2_US);
    updateSampleFrequency(SAMP_DEC2_US);   
    decadeNum = 2;
    Serial.print("");
    //printSerialDecadeInfo(decadeNum);
  }
  else if (freq < START_FREQ*1 && decadeNum < 1){
    updateSweepFrequency(SWEEP_DEC1_US);
    updateSampleFrequency(SAMP_DEC1_US);
    decadeNum = 1;
    Serial.print("");
    //printSerialDecadeInfo(decadeNum);
  }
  if (mode == MODE_SWEEP && serialBufferFlag == false){
    Serial.print(serialBuffer);
    if (serialBufferFlag == false){
      serialBuffer = "";
    }
  }
}


void sweepStep() {
  if (mode != MODE_SWEEP){
    return;
  }
  if (freq > 30000){
    mode = MODE_FINI;
    return;
  }
  else if (freq < START_FREQ){
    digitalWrite(DISCHARGE_PIN, 1);
    freq = START_FREQ;
    AD9850.wr_serial(0, freq);
    Serial.print("");
    sampleCount = 0;
    digitalWrite(DISCHARGE_PIN, 0);
    return;
  }

  dischargeSet = true;
  digitalWrite(DISCHARGE_PIN, 1);
  serialBufferFlag = true;
  float newFreq = freq*pow(10, 0.04);
  AD9850.wr_serial(0, newFreq);
  
  for (uint8_t i = 0; i < sampleCount-2; i++){
    serialBuffer += String(freq);
    serialBuffer += ",";
    serialBuffer += String(masBufferReg[i]);
    serialBuffer += ",";
    serialBuffer += String(pasBufferReg[i]) + "\n";
  }
  sampleCount = 0;
  freq = newFreq; 
  serialBufferFlag = false;
  
  digitalWrite(DISCHARGE_PIN, 0);
  dischargeSet = false;
}


void readAnalog(){
  if (mode != MODE_SWEEP){
    return;
  }
  if (sampleCount < 2){
    sampleCount++;
    return;
  }
  
  if (!dischargeSet && sampleCount < NUM_SAMPLES){
    digitalWrite(TEST_PIN, 1);
    masBufferReg[sampleCount-2] = ADC->ADC_CDR[MAS_PIN];
    pasBufferReg[sampleCount-2] = ADC->ADC_CDR[PAS_PIN];
    sampleCount++;
    digitalWrite(TEST_PIN, 0);
  }
  
}


void ffrvReset() {
  mode = MODE_WAIT;
  freq = 0;
  decadeNum = 0;
  sampleCount = 0;
  digitalWrite(DISCHARGE_PIN, 1);
  Serial.println("e");
}


void analogSetup(){
  // SOURCE: https://forum.arduino.cc/t/direct-accessing-of-adc-registers/468806/4

  ADC->ADC_MR |= 0x80;  // Mode FREERUN
  ADC->ADC_CR = 2;        // Start converter                       
  ADC->ADC_CHER = 0xC0;   // Enabling channels 6 and 7 (A0 and A1)
}


void updateSweepFrequency(double microseconds){
  DueTimerInterrupt dueTimerInterrupt(sweepInterruptID);
  dueTimerInterrupt.attachInterruptInterval(microseconds, sweepStep);
  sweepInterruptID = dueTimerInterrupt.getTimerNumber();
}


void updateSampleFrequency(double microseconds){
  DueTimerInterrupt dueTimerInterrupt(readInterruptID);
  dueTimerInterrupt.attachInterruptInterval(microseconds, readAnalog);
  readInterruptID = dueTimerInterrupt.getTimerNumber();
}


void printSerialDecadeInfo(uint8_t dec){
  Serial.print("Decade");
  Serial.print(dec);
  Serial.print(" Start: ");
  Serial.print(micros());
  Serial.print(", frequency: ");
  Serial.println(freq);
}
