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
        private readonly HashSet<OperationType> auraSupportedOps = new HashSet<OperationType>() { OperationType.SetColor };

        public override string DeviceName => "Unknown Aura Device";

        public override HashSet<OperationType> SupportedOperations => auraSupportedOps;

        private readonly AuraSDKDotNet.AuraDevice internalAuraDevice;

        public AuraDevice(AuraSDKDotNet.AuraDevice internalAuraDevice)
        {
            this.internalAuraDevice = internalAuraDevice;
        }

        public AuraDevice()
        {
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
