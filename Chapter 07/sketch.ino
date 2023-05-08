// The baud_rate must match the baud rate in the Figure 7.13
int baud_rate=9600;
void setup() {
  //  Starts a serial protocol with a baud rate of 9600 bps
  Serial.begin(baud_rate);
}
void loop() {
    // reads the serial every tick
    switch (Serial.read())
    {
        case 'A':
             // Received ‘A’ as input in the serial
            digitalWrite(LED_BUILTIN, HIGH); //blinks the led when A is received
            Serial.println("Received the letter: A");  //writes back to serial
            break;
        case 'Z':
             // Received ‘Z’ as input in the serial
            digitalWrite(LED_BUILTIN, HIGH); // blinks the led when Z is received
            Serial.println("Received the letter: Z"); //writes back to serial
            break;
    }
}