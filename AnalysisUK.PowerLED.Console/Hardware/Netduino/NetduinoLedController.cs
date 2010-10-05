using SecretLabs.NETMF.Hardware;

namespace AnalysisUK.PowerLED.Console.Hardware.Netduino
{
    class NetduinoLedController : LedHardwareController
    {
        private AnalogInput _lightSensor;

        public override int GetLightLevel()
        {
            return _lightSensor.Read();
        }

        protected override void InitializeProvider()
        {
            Provider = new NetduinoHardwareProvider();
        }

        protected override void OnInitiaze()
        {
            base.OnInitiaze();

            _lightSensor = new AnalogInput(Provider.LightSensorPin);
        }
    }
}
