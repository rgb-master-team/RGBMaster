using System.Collections.Generic;
using System.Linq;
using Infrastructure;
using Infrastructure.Effects;

namespace chroma_yeelight.Effects
{
	public abstract class DirectionalEffectController<T> : EffectController<T> where T : Effect
	{
		private readonly IEnumerable<T> _forwardDevicesEffect;
		private readonly IEnumerable<T> _backwardsDevicesEffect;

		public EffectDirection Direction { get; set; }
		public EffectSpeed Speed { get; set; }

		protected override IEnumerable<T> _devicesEffect => Direction < 0 ? _backwardsDevicesEffect : _forwardDevicesEffect;

		protected DirectionalEffectController(IEnumerable<Device> devices)
		{
			_forwardDevicesEffect = devices.SelectMany(x => x.Effects).OfType<T>();
			_backwardsDevicesEffect = _forwardDevicesEffect.Reverse();
		}
	}
}
