

// These define's must be placed at the beginning before #include "SAMDUETimerInterrupt.h"
#define TIMER_INTERRUPT_DEBUG         0
#define _TIMERINTERRUPT_LOGLEVEL_     0
#include "SAMDUETimerInterrupt.h"

#include <EF_AD9850.h>


#define SWEEP_DEC1_US        150000   //  150 ms
#define SWEEP_DEC2_US         15000   //  15  ms
#define SWEEP_DEC3_US          1500   //  1.5 ms
#define START_FREQ               20
#define DISCHARGE_PIN            12
#define MODE_FINI                 2
#define MODE_SWEEP                1
#define MODE_WAIT                 0



EF_AD9850 AD9850(11, 10, 8, 9);    // D_CLK, FR_UQ, RST, DATA

uint16_t sweepInterruptID = 0;
volatile double freq = START_FREQ;
uint8_t decadeNum = 0;
uint8_t mode = MODE_WAIT;


void setup() {
  Serial.begin(115200);
  AD9850.init();
  AD9850.reset();
  pinMode(DISCHARGE_PIN, OUTPUT);
  digitalWrite(DISCHARGE_PIN, 0);

  //DueTimerInterrupt frequencySweepInterrupt = DueTimer.getAvailable();
  //frequencySweepInterrupt.attachInterruptInterval(SWEEP_DEC1_US, sweepStep);
  //sweepInterruptID = frequencySweepInterrupt.getTimerNumber();

}


void loop() {
  if (mode == MODE_WAIT){
    if (Serial.available() > 0){
      char charIn = Serial.read();
      if (charIn == 's'){
        mode = MODE_SWEEP;
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
    Serial.print("End Sweep: ");
    Serial.print(micros());
    Serial.print(", frequency: ");
    Serial.println(freq);
    sweepReset();
  }
  if (mode != MODE_SWEEP){
    return;
  }

  if (freq >= START_FREQ*100 && decadeNum < 3){
    updateTimerFrequency(SWEEP_DEC3_US);
    decadeNum = 3;
    Serial.print("Decade3 Start: ");
    Serial.print(micros());
    Serial.print(", frequency: ");
    Serial.println(freq);
  }
  else if (freq >= START_FREQ*10 && decadeNum < 2){
    updateTimerFrequency(SWEEP_DEC2_US);   
    decadeNum = 2;
    Serial.print("Decade2 Start: ");
    Serial.print(micros());
    Serial.print(", frequency: ");
    Serial.println(freq);
  }
  else if (freq >= START_FREQ*1 && decadeNum < 1){
    updateTimerFrequency(SWEEP_DEC1_US);
    decadeNum = 1;
    Serial.print("Decade1 Start: ");
    Serial.print(micros());
    Serial.print(", frequency: ");
    Serial.println(freq);
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
  digitalWrite(DISCHARGE_PIN, 1);
  AD9850.wr_serial(0, freq);
  freq = freq*pow(10, 0.04);
  digitalWrite(DISCHARGE_PIN, 0);
}


void updateTimerFrequency(double microseconds){
  DueTimerInterrupt dueTimerInterrupt(sweepInterruptID);
  dueTimerInterrupt.attachInterruptInterval(microseconds, sweepStep);
  sweepInterruptID = dueTimerInterrupt.getTimerNumber();
}

void sweepReset(){
  mode = MODE_WAIT;
  freq = START_FREQ;
  decadeNum = 0;
  AD9850.wr_serial(0, 0);
}
