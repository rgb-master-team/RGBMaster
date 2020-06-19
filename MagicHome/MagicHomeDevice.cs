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
        public override Task ConnectInternal()
        {
            return Task.CompletedTask;
        }

        public override Task DisconnectInternal()
        {
            return Task.CompletedTask;
        }

        public override byte GetBrightnessPercentage()
        {
            throw new NotImplementedException();
        }

        public override System.Drawing.Color GetColor()
        {
            throw new NotImplementedException();
        }

        public override void SetBrightnessPercentage(byte brightness)
        {
            InternalLight.SetBrightness(brightness);
        }

        public override void SetColor(System.Drawing.Color color)
        {
            InternalLight.SetColor(color.R, color.G, color.B);
        }

        public override void TurnOffInternal()
        {
            InternalLight.TurnOff();
        }

        public override void TurnOnInternal()
        {
            InternalLight.TurnOn();
        }
    }
}
