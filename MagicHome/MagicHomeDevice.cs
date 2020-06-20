using Common;
using Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicHome
{
    public class MagicHomeDevice : Device
    {
        private Light InternalLight;

        public MagicHomeDevice(Light InternalLight) : base(new MagicHomeDeviceMetadata())
        {
            this.InternalLight = InternalLight;
            this.InternalLight.Logger.Enabled = false;
        }
        protected override Task ConnectInternal()
        {
            return Task.CompletedTask;
        }

        protected override Task DisconnectInternal()
        {
            return Task.CompletedTask;
        }

        protected override byte GetBrightnessPercentageInternal()
        {
            throw new NotImplementedException();
        }

        protected override System.Drawing.Color GetColorInternal()
        {
            throw new NotImplementedException();
        }

        protected override void SetBrightnessPercentageInternal(byte brightness)
        {
            InternalLight.SetBrightness(brightness);
        }

        protected override void SetColorInternal(System.Drawing.Color color)
        {
            InternalLight.SetColor(color.R, color.G, color.B);
        }

        protected override void TurnOffInternal()
        {
            InternalLight.TurnOff();
        }

        protected override void TurnOnInternal()
        {
            InternalLight.TurnOn();
        }
    }
}
