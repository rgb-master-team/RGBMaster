using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Infrastructure;
using Infrastructure.Effects;
using Msi.Devices.Leds;
using Msi.SDKs;

namespace Msi.Devices
{
	public class MLDevice : Device
	{
		public string Type { get; set; }
		public MLLed[] Leds { get; set; }

		#region Device Properties

		public override int LedCount => Leds.Length;
		public override HashSet<OperationType> SupportedOperations { get; }
		public override IEnumerable<Effect> Effects { get; }

		#endregion

		public MLDevice(string deviceType, int ledsCount)
		{
			Type = deviceType;
			Leds = new MLLed[ledsCount];

			SupportedOperations = new HashSet<OperationType>() { OperationType.SetColor };
		}

		internal void Load()
		{
			for (var ledIndex = 0; ledIndex < Leds.Length; ledIndex++)
			{
				MysticLightSdk.GetLedInfo(Type, ledIndex, out var ledName, out var ledStyles);

				Leds[ledIndex] = new MLLed(ledName, ledStyles, ledIndex, Type);
				Leds[ledIndex].Load();
			}
		}

		#region Device Methods

		public override Task<Color> GetColor()
		{
			throw new System.NotImplementedException();
		}

		public override Task SetColor(Color color)
		{
			foreach (var led in Leds)
			{
				led.SetStyle("Steady");
				led.SetColor(color);
			}

			return Task.CompletedTask;
		}

		public override Task<byte> GetBrightnessPercentage()
		{
			throw new System.NotImplementedException();
		}

		public override Task SetBrightnessPercentage(byte brightness)
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

		public override int GetLedCountByDirection(EffectDirection direction)
		{
			throw new System.NotImplementedException();
		}

		public override Task SetColors(Color[] colors)
		{
			throw new System.NotImplementedException();
		}

		#endregion
	}
}
