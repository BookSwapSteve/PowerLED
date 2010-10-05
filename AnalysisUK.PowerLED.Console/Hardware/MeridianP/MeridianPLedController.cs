using Microsoft.SPOT.Hardware;

namespace AnalysisUK.PowerLED.Console.Hardware.MeridianP
{
    class MeridianPLedController : LedHardwareController
    {
        InterruptPort _switchPort;

        public override int GetLightLevel()
        {
            // Analog input for light sensor not supported.
            return 1023; // HACK, always light
        }

        protected override void InitializeProvider()
        {
            Provider = new MeridianPHarddwareProvider();
        }

        protected override void OnInitiaze()
        {
            base.OnInitiaze();

            AreLightsOn = false;

            const Cpu.Pin switchPin = DeviceSolutions.SPOT.Hardware.MeridianP.Pins.SW1;
            _switchPort = new InterruptPort(switchPin, true, Port.ResistorMode.PullUp, Port.InterruptMode.InterruptEdgeLevelLow);
            _switchPort.OnInterrupt += switchPort_OnInterrupt;
        }

        void switchPort_OnInterrupt(uint data1, uint data2, System.DateTime time)
        {
            InvokeSwitchPressed(data1, data2, time);
            _switchPort.ClearInterrupt();
        }
    }
}
