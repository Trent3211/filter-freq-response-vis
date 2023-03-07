

// These define's must be placed at the beginning before #include "SAMDUETimerInterrupt.h"
#define TIMER_INTERRUPT_DEBUG         0
#define _TIMERINTERRUPT_LOGLEVEL_     0
#include "SAMDUETimerInterrupt.h"

#include <EF_AD9850.h>


#define TIMER_INTERVAL_US        50000   //  50 ms
#define START_FREQ_HZ            20
#define DISCHARGE_PIN            12


EF_AD9850 AD9850(11, 10, 8, 9);  

uint16_t sweepInterruptID = 0;
volatile double freq = START_FREQ_HZ;


void setup() {
  Serial.begin(9600);
  AD9850.init();
  AD9850.reset();
  pinMode(DISCHARGE_PIN, OUTPUT);
  digitalWrite(DISCHARGE_PIN, 0);

  DueTimerInterrupt frequencySweepInterrupt = DueTimer.getAvailable();
  frequencySweepInterrupt.attachInterruptInterval(TIMER_INTERVAL_US, sweepStep);
  sweepInterruptID = frequencySweepInterrupt.getTimerNumber();

}

void loop() {
  Serial.print("freq = ");
  Serial.println(freq);
  delay(25);
}


void sweepStep() {
  if (freq > 30000){
    freq = START_FREQ_HZ;
  }
  digitalWrite(DISCHARGE_PIN, 1);
  AD9850.wr_serial(0, freq);
  freq = freq*pow(10, 0.04);
  digitalWrite(DISCHARGE_PIN, 0);
}
