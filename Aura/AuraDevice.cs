using AuraSDKDotNet;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aura
{
    public class AuraDevice : Device
    {
        private readonly AuraSDKDotNet.AuraDevice internalAuraDevice;

        public AuraDevice(AuraSDKDotNet.AuraDevice internalAuraDevice)
        {
            this.internalAuraDevice = internalAuraDevice;
        }

        public override Task<byte> GetBrightnessPercentage()
        {
            throw new NotImplementedException();
        }

        public override Task<System.Drawing.Color> GetColor()
        {
            throw new NotImplementedException();
        }

        public override Task SetBrightnessPercentage(byte brightness)
        {
            throw new NotImplementedException();
        }

        public override Task SetColor(System.Drawing.Color color)
        {
            this.internalAuraDevice.SetColors(new Color[] { new Color(color.R, color.G, color.B) });

            return Task.CompletedTask;
        }
    }
}
