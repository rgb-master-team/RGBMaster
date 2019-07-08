using System;
using System.Collections.Generic;
using Infrastructure.Effects;

namespace chroma_yeelight.Effects
{
	public abstract class EffectController<T> : EffectController where T : Effect
	{
		protected abstract IEnumerable<T> _devicesEffect { get; }
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
