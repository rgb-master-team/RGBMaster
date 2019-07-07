using AuraSDKDotNet;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Effects;

namespace Aura
{
    public class AuraDevice : Device
    {
        private readonly HashSet<OperationType> auraSupportedOps = new HashSet<OperationType>() { OperationType.SetColor };

        public override int LedCount => internalAuraDevice.LedCount;


		public override HashSet<OperationType> SupportedOperations => auraSupportedOps;
		public override IEnumerable<Effect> Effects { get; }

		private readonly AuraSDKDotNet.AuraDevice internalAuraDevice;

        public AuraDevice(AuraSDKDotNet.AuraDevice internalAuraDevice)
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

        public override int GetLedCountByDirection(EffectDirection direction)
        {
	        throw new NotImplementedException();
        }

        public override Task SetColors(System.Drawing.Color[] colors)
        {
	        throw new NotImplementedException();
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
