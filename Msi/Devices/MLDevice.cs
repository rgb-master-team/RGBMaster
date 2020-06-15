using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Infrastructure;
using Msi.Devices.Leds;
using Msi.Provider;
using Msi.SDKs;
using Provider;

namespace Msi.Devices
{
	public class MLDevice : Device<MLDeviceMetadata>
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
				MysticLightSdk.GetLedInfo(DeviceMetadata.deviceType, ledIndex, out var ledName, out var ledStyles);

				Leds[ledIndex] = new MLLed(ledName, ledStyles, ledIndex, DeviceMetadata.deviceType);
				Leds[ledIndex].Load();
			}
		}

		#region Device Methods

		public override Color GetColor()
		{
			throw new System.NotImplementedException();
		}

		public override void SetColor(Color color)
		{
			foreach (var led in Leds)
			{
				led.SetStyle("Steady");
				led.SetColor(color);
			}
		}

		public override byte GetBrightnessPercentage()
		{
			throw new System.NotImplementedException();
		}

		public override void SetBrightnessPercentage(byte brightness)
		{
			throw new System.NotImplementedException();
		}

		public override Task Connect()
		{
			return Task.CompletedTask;
		}

		public override Task Disconnect()
		{
			return Task.CompletedTask;
		} 

		#endregion
	}
}
