using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure;
using Infrastructure.Effects;

namespace chroma_yeelight.Effects
{
	public abstract class EffectController<T> : EffectController where T : Effect
	{
		protected readonly IEnumerable<T> _devicesEffect;

		protected EffectController(IEnumerable<Device> devices)
		{
			_devicesEffect = devices.SelectMany(x => x.Effects).OfType<T>();
		}
	}


	public abstract class EffectController : IDisposable
	{
		public abstract string Name { get; }

		public abstract void Start();
		public abstract void Stop();

		public virtual void Dispose()
		{
		}
	}
}
