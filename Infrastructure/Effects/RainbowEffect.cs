namespace Infrastructure.Effects
{
	public abstract class RainbowEffect : DirectionalEffect
	{
		protected RainbowEffect(Device device) : base(device)
		{
		}

		public int GetLedCountByDirection(EffectDirection direction) => _device.GetLedCountByDirection(direction);
	}
}
