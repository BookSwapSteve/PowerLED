using System;
using System.Threading;
using AnalysisUK.PowerLED.Console.Config;
using AnalysisUK.PowerLED.Console.Hardware;
using AnalysisUK.PowerLED.Console.Hardware.MeridianP;
using AnalysisUK.PowerLED.Console.Hardware.Netduino;
using Microsoft.SPOT;

namespace AnalysisUK.PowerLED.Console
{
    public class Program
    {
        private static LedHardwareController _hardwareController;
        private static AppConfig _appConfig;

        public static void Main()
        {
            Debug.Print(Microsoft.SPOT.Hardware.SystemInfo.OEMString);
            Debug.Print("Model: " + Microsoft.SPOT.Hardware.SystemInfo.SystemID.Model.ToString());
            Debug.Print("OEM: " + Microsoft.SPOT.Hardware.SystemInfo.SystemID.OEM.ToString());
            Debug.Print("SKU: " + Microsoft.SPOT.Hardware.SystemInfo.SystemID.SKU.ToString());

            // Setup the hardware.
            if (MeridianPHarddwareProvider.IsHardwareMeridianP())
            {
                _hardwareController = new MeridianPLedController();    
            } 
            else
            {
                _hardwareController = new NetduinoLedController();  
            }

            _hardwareController.SwitchPressed += hardwareController_SwitchPressed;
            _hardwareController.Initialize();
            _appConfig = _hardwareController.AppConfig;

            // Create a new timer to check the light level.
            new Timer(timerTick, null, 100, 5000);

            Thread.Sleep(Timeout.Infinite);
        }

        static void hardwareController_SwitchPressed(uint data1, uint data2, DateTime time)
        {
            if (_hardwareController.AreLightsOn)
            {
                _hardwareController.LightsOff();
            }
            else
            {
                _hardwareController.LightsOn();
            }
        }

        private static void timerTick(object state)
        {
            int lightLevel = _hardwareController.GetLightLevel();
            Debug.Print("Light Level: " + lightLevel);

            // Add some hysteris.
            if (lightLevel < _appConfig.LowLightThreshold && !_hardwareController.AreLightsOn) 
            {
                _hardwareController.LightsOn();
            }
            else if (lightLevel > _appConfig.HighLightThreshold)
            {
                _hardwareController.LightsOff();
            }
        }
    }
}
