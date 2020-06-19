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

		public MLDevice(string deviceType, int ledsCount) : base(new MLDeviceMetadata(deviceType))
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

		protected override Color GetColorInternal()
		{
			throw new System.NotImplementedException();
		}

		protected override void SetColorInternal(Color color)
		{
			foreach (var led in Leds)
			{
				led.SetStyle("Steady");
				led.SetColor(color);
			}
		}

		protected override byte GetBrightnessPercentageInternal()
		{
			throw new System.NotImplementedException();
		}

		protected override void SetBrightnessPercentageInternal(byte brightness)
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

		protected override void TurnOnInternal()
		{
			throw new System.NotImplementedException();
		}

		protected override void TurnOffInternal()
		{
			throw new System.NotImplementedException();
		}

		#endregion
	}
}
