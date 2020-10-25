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
    public class AuraMotherboardDevice : Device
    {
        private readonly AuraSDKDotNet.AuraDevice internalAuraDevice;

        public AuraMotherboardDevice(AuraSDKDotNet.AuraDevice internalAuraDevice, AuraMotherboardDeviceMetadata auraMotherboardDeviceMetadata) : base(auraMotherboardDeviceMetadata)
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

        protected override Task<byte> GetBrightnessPercentageInternal()
        {
            throw new NotImplementedException();
        }

        protected override Task<System.Drawing.Color> GetColorInternal()
        {
            throw new NotImplementedException();
        }

        protected override Task SetBrightnessPercentageInternal(byte brightness)
        {
            throw new NotImplementedException();
        }

        protected override Task SetColorInternal(System.Drawing.Color color)
        {
            this.internalAuraDevice.SetColors(new Color[] { new Color(color.R, color.G, color.B) });
            return Task.CompletedTask;
        }

        protected override Task SetGradientInternal(GradientPoint gradientPoint)
        {
            throw new NotImplementedException();
        }

        protected override Task TurnOffInternal()
        {
            throw new NotImplementedException();
        }

        protected override Task TurnOnInternal()
        {
            throw new NotImplementedException();
        }
    }
}
