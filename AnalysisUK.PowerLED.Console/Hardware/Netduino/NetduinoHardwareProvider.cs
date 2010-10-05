using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace AnalysisUK.PowerLED.Console.Hardware.Netduino
{
    class NetduinoHardwareProvider : LedHardwareProvider
    {
        /// <summary>
        /// Pins used for LED control.
        /// </summary>
        private readonly Cpu.Pin[] _ledPins = new []
                                                {
                                                    Pins.ONBOARD_LED , 
                                                    Pins.GPIO_PIN_D5,
                                                    Pins.GPIO_PIN_D6
                                                };

        public override Cpu.Pin[] LedPins
        {
            get { return _ledPins; }
        }

        public override Cpu.Pin LightSensorPin
        {
            get { return Pins.GPIO_PIN_A0; }
        }
    }
}
