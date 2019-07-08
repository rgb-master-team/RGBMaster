using System.Drawing;

namespace Infrastructure.Effects
{
	public abstract class DirectionalEffect : Effect
	{
		public EffectDirection Direction { get; private set; }

		public EffectDirection OppositeDimension => Direction == EffectDirection.Up || Direction == EffectDirection.Down ? EffectDirection.Left : EffectDirection.Up;

		protected DirectionalEffect(Device device) : base(device)
		{
		}

		public virtual void SetDirection(EffectDirection direction)
		{
			Direction = direction;
		}

		public abstract void SetColors(Color[] colors);
	}
}
