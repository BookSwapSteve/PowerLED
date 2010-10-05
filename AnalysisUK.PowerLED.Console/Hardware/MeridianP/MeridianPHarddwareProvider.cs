using Microsoft.SPOT.Hardware;

namespace AnalysisUK.PowerLED.Console.Hardware.MeridianP
{
    class MeridianPHarddwareProvider : LedHardwareProvider
    {
        /// <summary>
        /// Pins used for LED control.
        /// </summary>
        /// <remarks>Pins exposed on 10w header. (GPIO15-J2 P7, GPIO14- J2 P5)</remarks>
        private readonly Cpu.Pin[] _ledPins = new []
                                                {
                                                    DeviceSolutions.SPOT.Hardware.MeridianP.Pins.GPIO14,
                                                    DeviceSolutions.SPOT.Hardware.MeridianP.Pins.GPIO15,
                                                    DeviceSolutions.SPOT.Hardware.MeridianP.Pins.LED
                                                };

        public override Cpu.Pin[] LedPins
        {
            get { return _ledPins; }
        }

        public override Cpu.Pin LightSensorPin
        {
            get { return Cpu.Pin.GPIO_NONE; }
        }

        public static bool IsHardwareMeridianP()
        {
            return SystemInfo.OEMString == "MERIDIANP_NET Device Solutions Ltd";
        }
    }
}
