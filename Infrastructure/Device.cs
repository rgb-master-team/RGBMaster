using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Effects;

namespace Infrastructure
{
    public abstract class Device
    {
		public abstract int LedCount { get; }
        public abstract HashSet<OperationType> SupportedOperations { get; }
        public List<Effect> Effects { get; }
        public abstract Task<Color> GetColor();
        public abstract Task SetColor(Color color);
        public abstract Task<byte> GetBrightnessPercentage();
        public abstract Task SetBrightnessPercentage(byte brightness);
        public abstract Task Connect();
        public abstract Task Disconnect();
        public abstract int GetLedCountByDirection(EffectDirection direction);

		// TODO - Exceptions or error messages or both? Hmmmst..
		public abstract Task SetColors(Color[] colors);

		protected Device()
		{
			Effects = GetType().Assembly.GetTypes().Where(x => x.IsSubclassOf(typeof(Effect))).Select(x => (Effect)Activator.CreateInstance(x, this)).ToList();
		}
    }
}
