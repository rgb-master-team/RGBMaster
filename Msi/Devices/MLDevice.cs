using Msi.Devices.Leds;
using Msi.Provider;
using Msi.SDKs;
using Provider;
using System.Drawing;
using System.Threading.Tasks;

namespace Msi.Devices
{
    public class MLDevice : Device
	{
		public MLLed[] Leds { get; set; }

		public MLDevice(int ledsCount, MLDeviceMetadata mLDeviceMetadata) : base(mLDeviceMetadata)
		{
			Leds = new MLLed[ledsCount];
		}

		internal void Load()
		{
			for (var ledIndex = 0; ledIndex < Leds.Length; ledIndex++)
			{
				var mlDeviceMd = (MLDeviceMetadata)DeviceMetadata;
				MysticLightSdk.GetLedInfo(mlDeviceMd.deviceType, ledIndex, out var ledName, out var ledStyles);

				Leds[ledIndex] = new MLLed(ledName, ledStyles, ledIndex, mlDeviceMd.deviceType);
				Leds[ledIndex].Load();
			}
		}

		#region Device Methods

		protected override Task<Color> GetColorInternal()
		{
			throw new System.NotImplementedException();
		}

		protected override Task SetColorInternal(Color color)
		{
			foreach (var led in Leds)
			{
				led.SetStyle("Steady");
				led.SetColor(color);
			}

			return Task.CompletedTask;
		}

		protected override Task<byte> GetBrightnessPercentageInternal()
		{
			throw new System.NotImplementedException();
		}

		protected override Task SetBrightnessPercentageInternal(byte brightness)
		{
			throw new System.NotImplementedException();
		}

		protected override Task ConnectInternal()
		{
			return Task.CompletedTask;
		}

		protected override Task DisconnectInternal()
		{
			return Task.CompletedTask;
		}

		protected override Task TurnOnInternal()
		{
			throw new System.NotImplementedException();
		}

		protected override Task TurnOffInternal()
		{
			throw new System.NotImplementedException();
		}

		#endregion
	}
}
