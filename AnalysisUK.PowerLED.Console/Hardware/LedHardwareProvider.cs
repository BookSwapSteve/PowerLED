using Microsoft.SPOT.Hardware;

namespace AnalysisUK.PowerLED.Console.Hardware
{
    abstract class LedHardwareProvider : HardwareProvider
    {
        /// <summary>
        /// Get the pins used for the leds
        /// </summary>
        abstract public Cpu.Pin[] LedPins { get; }

        /// <summary>
        /// Gets the pin used for the light sensor
        /// </summary>
        abstract public Cpu.Pin LightSensorPin { get; }
    }
}
