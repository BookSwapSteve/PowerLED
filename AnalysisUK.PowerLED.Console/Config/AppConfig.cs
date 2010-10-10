using System;

namespace AnalysisUK.PowerLED.Console.Config
{
    [Serializable]
    public class AppConfig
    {
        public int LowLightThreshold { get; set; }

        public int HighLightThreshold { get; set; }
    }
}
