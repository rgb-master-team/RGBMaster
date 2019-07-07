using System.Collections.Generic;
using System.Drawing;

namespace Infrastructure.Effects
{
	public abstract class DirectionalEffect : Effect
	{
		public EffectDirection Direction { get; private set; }

		public EffectDirection OppositeDirection => Direction == EffectDirection.Horizontal ? EffectDirection.Vertical : EffectDirection.Horizontal;

		protected DirectionalEffect(Device device) : base(device)
		{
		}

		public virtual void SetDirection(EffectDirection direction)
		{
			Direction = direction;
		}

		public abstract void SetColors(Color[] color);
	}
}
