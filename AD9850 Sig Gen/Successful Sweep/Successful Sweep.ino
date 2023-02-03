/*
Simple controller for AD9850 modules
Uses serial protocol (refer to AD9850 datasheet what pins need to be pulled up/down
Uses library by Elecfreaks http://elecfreaks.com/store/download/datasheet/breakout/DDS/EF_AD9850_library.zip
(for use with chipkit delete that unnecessary line "#include <avr/pgmspace.h>" from .h file)
Fix the pin numbers used to communicate with module
Serial comms - 9600, NewLine (0x0A) denotes end of input,
f 10000 - set frequency
s 20 20000 10 1000 - sweep from frequency to frequency using this step size in that many milliseconds
o 10 10000 5 3000 - oscillating sweep from frequency to frequency and back using this step size in that many ms
NB! no error checking in input, code relies on intelligence of user
*/

#include <EF_AD9850.h>
#define MAXBUF 40

// FIXME set up pins to match your case
// parameters in following order - CLK, FQUP, RESET, DATA
EF_AD9850 AD9850(11, 10, 8, 9);
double freqValue = 0;
double freqStart = 0;
double freqEnd = 0;
double freqStep = 0;
int sweepDelay = 0;
char freqStr[MAXBUF];
int bufPos = 0;
char byteIn = 0;
int mode = 0;
int debugDelay = 0;

void setup() {
  AD9850.init();
  AD9850.reset();
  Serial.begin(9600);
  memset(freqStr, 0, sizeof(freqStr));
}

void loop() {
  if (mode == 1 || mode == 2) {

    // Read the analog voltage at A1 pin.
    // Convert the data of 0-1023 to a voltage value and output it to the serial monitor.
    int sensorValue = analogRead(A0);
    float voltage = sensorValue * (3.3 / 1023.0);
    Serial.println(voltage);



    if (((freqStep > 0.0) && (freqValue + freqStep <= max(freqStart, freqEnd))) || ((freqStep < 0.0) && (freqValue + freqStep >= min(freqStart, freqEnd))))
      freqValue += freqStep;
    else if (mode == 1)
      freqValue = freqStart;
    else {
      freqStep *= -1;
      freqValue += freqStep;
    }
    AD9850.wr_serial(0, freqValue);
  }
  while (Serial.available()) {
    byteIn = Serial.read();
    if (bufPos < sizeof(freqStr))
      freqStr[bufPos++] = byteIn;
    else {
      bufPos = 0;
      byteIn = 0;
      memset(freqStr, 0, sizeof(freqStr));
      Serial.println("Command too long. Ignored.");
    }
  }
  if (byteIn == 0x0a) {
    switch (freqStr[0]) {
      case 'f':
        mode = 0;
        freqValue = strtod(freqStr + 2, NULL);
        freqEnd = 0;
        freqStep = 0;
        sweepDelay = 0;
        Serial.print("Frequency ");
        Serial.println(freqValue);
        break;
      case 's':
      case 'o':
        char *fEnd1, *fEnd2;
        freqStart = abs(strtod(freqStr + 2, &fEnd1));
        freqEnd = abs(strtod(fEnd1, &fEnd2));
        freqStep = abs(strtod(fEnd2, &fEnd1));
        if (freqStep == 0) {
          Serial.println("You gotta be kidding me, step can not be 0");
          break;
        }
        sweepDelay = abs(atoi(fEnd1));
        if (freqStr[0] == 's') {
          mode = 1;
          Serial.print("Sweep");
        } else {
          mode = 2;
          Serial.print("Oscillate sweep");
        }
        Serial.print(" start freq. ");
        Serial.print(freqStart);
        Serial.print(" end freq. ");
        Serial.print(freqEnd);
        Serial.print(" step ");
        Serial.print(freqStep);
        Serial.print(" time ");
        Serial.println(sweepDelay);
        sweepDelay /= abs(freqEnd - freqStart) / freqStep;
        if (mode == 2)
          sweepDelay /= 2;
        if (freqStart > freqEnd)
          freqStep *= -1;
        freqValue = freqStart;
        break;
      default:
        Serial.println("AI blown up - unknown command");
    }
    memset(freqStr, 0, sizeof(freqStr));
    byteIn = 0;
    bufPos = 0;
    AD9850.wr_serial(0, freqValue);
  }
}