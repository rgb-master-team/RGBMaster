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
    public class AuraDevice : Device
    {
        private readonly AuraSDKDotNet.AuraDevice internalAuraDevice;

        public AuraDevice(AuraSDKDotNet.AuraDevice internalAuraDevice) : base(new AuraDeviceMetadata())
        {
            this.internalAuraDevice = internalAuraDevice;
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
            throw new NotImplementedException();
        }

        protected override void SetColorInternal(System.Drawing.Color color)
        {
            this.internalAuraDevice.SetColors(new Color[] { new Color(color.R, color.G, color.B) });
        }

        protected override void TurnOffInternal()
        {
            throw new NotImplementedException();
        }

        protected override void TurnOnInternal()
        {
            throw new NotImplementedException();
        }
    }
}
