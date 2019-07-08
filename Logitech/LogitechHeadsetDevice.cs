using Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Effects;

namespace Logitech
{
    public class LogitechHeadsetDevice : Device
    {
        private readonly HashSet<OperationType> logitechSupportedOps = new HashSet<OperationType>() { OperationType.SetColor, OperationType.SetBrightness };

        public override int LedCount => throw new NotImplementedException();

		public override HashSet<OperationType> SupportedOperations => logitechSupportedOps;

		public override Task Connect()
        {
            throw new NotImplementedException();
        }

        public override Task Disconnect()
        {
            throw new NotImplementedException();
        }

        public override int GetLedCountByDirection(EffectDirection direction)
        {
	        throw new NotImplementedException();
        }

        public override Task SetColors(Color[] colors)
        {
	        throw new NotImplementedException();
        }

        public override Task<byte> GetBrightnessPercentage()
        {
            throw new NotImplementedException();
        }

        public override Task<Color> GetColor()
        {
            throw new NotImplementedException();
        }

        public override Task SetBrightnessPercentage(byte brightness)
        {
            throw new NotImplementedException();
        }

        public override Task SetColor(Color color)
        {
            throw new NotImplementedException();
        }
    }
}
