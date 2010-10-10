using AnalysisUK.PowerLED.Console.Config;
using Microsoft.SPOT.Hardware;

namespace AnalysisUK.PowerLED.Console.Hardware
{
    abstract class LedHardwareController
    {
        public event NativeEventHandler SwitchPressed;

        #region Public Properties

        public bool AreLightsOn { get; protected set; }

        public AppConfig AppConfig { get; protected set; }

        #endregion

        #region Public Methods

        public abstract int GetLightLevel();

        public virtual void LightsOn()
        {
            SetLedStates(true);
        }

        public virtual void LightsOff()
        {
            SetLedStates(false);
        }

        public void Initialize()
        {
            InitializeProvider();
            HardwareProvider.Register(Provider);
            OnInitiaze();
        }

        #endregion

        #region Protected Properties

        protected LedHardwareProvider Provider { get; set; }

        protected OutputPort[] LedPorts { get; set; }

        #endregion

        #region Protected Methods

        protected void InvokeSwitchPressed(uint data1, uint data2, System.DateTime time)
        {
            NativeEventHandler handler = SwitchPressed;
            if (handler != null) handler(data1, data2, time);
        }

        protected abstract void InitializeProvider();

        protected virtual void OnInitiaze()
        {
            AppConfig = LoadConfiguration();
            LedPorts = new OutputPort[Provider.LedPins.Length];

            for (int i = 0; i < Provider.LedPins.Length; i++)
            {
                LedPorts[i] = new OutputPort(Provider.LedPins[i], false);
            }

            AreLightsOn = false;
        }

        protected virtual void SetLedStates(bool state)
        {
            foreach (var ledPort in LedPorts)
            {
                ledPort.Write(state);
            }
            AreLightsOn = state;
        }

        protected virtual AppConfig LoadConfiguration()
        {
            return new AppConfig { HighLightThreshold = 50, LowLightThreshold = 20 };
        }

        #endregion
    }
}
