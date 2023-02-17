#include <EF_AD9850.h>

// Define constants
const int MAX_BUFFER_SIZE = 1024;
const int SERIAL_BAUD_RATE = 115200;

// Define pin numbers used to communicate with module ESP
const int CLK_PIN = 0; // D3
const int FQUP_PIN = 2; // D4
const int RESET_PIN = 5; // D1
const int DATA_PIN = 4; // D2

// const int CLK_PIN = 11;
// const int FQUP_PIN = 10;
// const int RESET_PIN = 8;
// const int DATA_PIN = 9;

// Define variables
EF_AD9850 ad9850(CLK_PIN, FQUP_PIN, RESET_PIN, DATA_PIN);
double freqValue = 0;
double freqStart = 0;
double freqEnd = 0;
double freqStep = 0;
int sweepDelay = 0;
char frequencyString[MAX_BUFFER_SIZE];
int bufferPosition = 0;
char inputByte = 0;
int mode = 0;

// Compute the number of points per decade based on the starting and stopping frequencies
int getPointsPerDecade(double startFreq, double stopFreq, int numPoints) {
  double decades = log10(stopFreq / startFreq);
  int pointsPerDecade = (int)(numPoints / decades);
  return pointsPerDecade;
}


// Initialize the module and serial communication
void setup() {
  ad9850.init();
  ad9850.reset();
  Serial.begin(SERIAL_BAUD_RATE);
  memset(frequencyString, 0, sizeof(frequencyString));
}

// Main loop
void loop() {
  if (mode == 1) {
    int sensorValue = analogRead(A0);
    float voltage = sensorValue * (3.3 / 1023.0);
    Serial.print(freqValue);
    Serial.print(":");
    Serial.println(voltage);

    if (((freqStep > 0.0) && (freqValue + freqStep <= max(freqStart, freqEnd))) || ((freqStep < 0.0) && (freqValue + freqStep >= min(freqStart, freqEnd)))) {
      freqValue += freqStep;
    } else {
      ad9850.reset();
      mode = 0;
      Serial.println("Sweep finished");
    }
    ad9850.wr_serial(0, freqValue);
  } 

  while (Serial.available()) {
      inputByte = Serial.read();
      if (bufferPosition < sizeof(frequencyString)) {
        frequencyString[bufferPosition++] = inputByte;
      } else {
        bufferPosition = 0;
        inputByte = 0;
        memset(frequencyString, 0, sizeof(frequencyString));
        Serial.println("Command too long. Ignored.");
      }
      if (inputByte == 'r') {
        ad9850.reset();
        mode = 0;
        Serial.println("Sweep reset");
        memset(frequencyString, 0, sizeof(frequencyString));
        inputByte = 0;
        bufferPosition = 0;
      }
    }


  if (inputByte == '\n') {
    switch (frequencyString[0]) {
      case 's':
        char *freqEnd1, *freqEnd2;
        freqStart = abs(strtod(frequencyString + 2, &freqEnd1));
        freqEnd = abs(strtod(freqEnd1, &freqEnd2));
        freqStep = abs(strtod(freqEnd2, &freqEnd1));
        if (freqStep == 0) {
          Serial.println("Error: step size cannot be 0.");
          break;
        }
        sweepDelay = abs(atoi(freqEnd1));
        mode = 1;
        Serial.print("Sweep start frequency: ");
        Serial.print(freqStart);
        Serial.print(" end frequency: ");
        Serial.print(freqEnd);
        Serial.print(" step size: ");
        Serial.print(freqStep);
        Serial.print(" delay: ");
        Serial.println(sweepDelay);
        sweepDelay /= abs(freqEnd - freqStart) / freqStep;
        if (freqStart > freqEnd) {
          freqStep *= -1;
        }
        freqValue = freqStart;
        break;

      default:
        Serial.println("Error: unknown command.");
        break;
    }

    memset(frequencyString, 0, sizeof(frequencyString));
    inputByte = 0;
    bufferPosition = 0;
  }
}
