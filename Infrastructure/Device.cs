using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Infrastructure.Effects;

namespace Infrastructure
{
    public abstract class Device
    {
		public abstract int LedCount { get; }
        public abstract HashSet<OperationType> SupportedOperations { get; }
        public abstract IEnumerable<Effect> Effects { get; }
        public abstract Task<Color> GetColor();
        public abstract Task SetColor(Color color);
        public abstract Task<byte> GetBrightnessPercentage();
        public abstract Task SetBrightnessPercentage(byte brightness);
        public abstract Task Connect();
        public abstract Task Disconnect();
        public abstract int GetLedCountByDirection(EffectDirection direction);

		// TODO - Exceptions or error messages or both? Hmmmst..
		public abstract Task SetColors(Color[] colors);
    }
}
