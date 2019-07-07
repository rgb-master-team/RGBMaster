namespace Infrastructure.Effects
{
	public abstract class Effect
	{
		protected Device _device;

		protected Effect(Device device)
		{
			_device = device;
		}
	}
}
