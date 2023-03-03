#include <EF_AD9850.h>
#define TRIGGER 12  // the pin used for triggering the oscilloscope

EF_AD9850 AD9850(11, 10, 8, 9);

double freq = 100;
bool freq_switch = false;

void setup() {
  AD9850.init();
  AD9850.reset();
  pinMode(TRIGGER, OUTPUT);
  digitalWrite(TRIGGER, 0);
}

void loop() {
  digitalWrite(TRIGGER, 1);
  if (freq_switch){
    AD9850.wr_serial(0, freq);
  }
  else if(!freq_switch) {
    AD9850.wr_serial(0, freq*10);
  }
  digitalWrite(TRIGGER,0);
  
  delay(100);
  freq_switch = !freq_switch;

}
