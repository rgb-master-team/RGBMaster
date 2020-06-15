using AuraSDKDotNet;
using Common;
using Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aura
{
    public class AuraDevice : Device<AuraDeviceMetadata>
    {
        private readonly AuraSDKDotNet.AuraDevice internalAuraDevice;

        public AuraDevice(AuraSDKDotNet.AuraDevice internalAuraDevice) : base(new AuraDeviceMetadata())
        {
            this.internalAuraDevice = internalAuraDevice;
        }

        public override Task Connect()
        {
            return Task.CompletedTask;
        }

        public override Task Disconnect()
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
            throw new NotImplementedException();
        }

        public override void SetColor(System.Drawing.Color color)
        {
            this.internalAuraDevice.SetColors(new Color[] { new Color(color.R, color.G, color.B) });
        }
    }
}
