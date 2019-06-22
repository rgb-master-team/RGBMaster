using System.Collections.Generic;
using Infrastructure;

namespace chroma_yeelight.Effects.Common
{
	public abstract class Effect
	{
		protected readonly IEnumerable<Device> _devices;
		public abstract string Name { get; }

		protected Effect(IEnumerable<Device> devices)
		{
			_devices = devices;
		}

		public abstract void Start();
		public abstract void Stop();
	}
}
