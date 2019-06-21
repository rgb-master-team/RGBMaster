using System.Drawing;
using Msi.SDKs;

namespace Msi.Devices.Leds
{
	public class MLLed
	{
		protected readonly string OwnerDeviceType;

		public string Name { get; }
		public string[] AvailableStyles { get; }
		public int Index { get; }
		public int MaxBright { get; set; }
		public int MaxSpeed { get; set; }

		public MLLed(string name, string[] availableStyles, int index, string deviceType)
		{
			Name = name;
			AvailableStyles = availableStyles;
			Index = index;
			OwnerDeviceType = deviceType;
		}

		public void Load()
		{
			MaxBright = MysticLightSdk.GetLedMaxBright(OwnerDeviceType, Index);
			MaxSpeed = MysticLightSdk.GetLedMaxSpeed(OwnerDeviceType, Index);
		}

		public Color GetColor()
		{
			return MysticLightSdk.GetLedColor(OwnerDeviceType, Index);
		}

		public string GetStyle()
		{
			return MysticLightSdk.GetLedStyle(OwnerDeviceType, Index);
		}

		public int GetBright()
		{
			return MysticLightSdk.GetLedBright(OwnerDeviceType, Index);
		}

		public int GetSpeed()
		{
			return MysticLightSdk.GetLedSpeed(OwnerDeviceType, Index);
		}

		public void SetColor(Color color)
		{
			MysticLightSdk.SetLedColor(OwnerDeviceType, Index, color);
		}

		public void SetStyle(string style)
		{
			MysticLightSdk.SetLedStyle(OwnerDeviceType, Index, style);
		}

		public void SetBright(int brightLevel)
		{
			MysticLightSdk.SetLedBright(OwnerDeviceType, Index, brightLevel);
		}

		public void SetSpeed(int speedLevel)
		{
			MysticLightSdk.SetLedSpeed(OwnerDeviceType, Index, speedLevel);
		}
	}
}
