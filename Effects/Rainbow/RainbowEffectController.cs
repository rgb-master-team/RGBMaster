using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using Infrastructure;
using Infrastructure.Effects;

namespace chroma_yeelight.Effects.Rainbow
{
	public class RainbowEffectController : DirectionalEffectController<RainbowEffect>
	{
		public override string Name => "Rainbow";

		private readonly Timer rainbowTimer;

		private Color currentColor;

		public RainbowEffectController(IEnumerable<Device> devices) : base(devices)
		{
			rainbowTimer = new Timer();
		}

		public override void Start()
		{
			currentColor = GetStartColor();
			SetRainbowEffectsDirection();

			var devicesEffectToLedColor = _devicesEffect.ToDictionary(x => x, CreateStartColors);

			rainbowTimer.Tick += (sender, args) => UpdateColors(devicesEffectToLedColor);
			rainbowTimer.Interval = 100;

			rainbowTimer.Start();
		}

		private Color GetStartColor() => RGBColorHelper.HSLToRGBColor(0, 142 / 240f, 240 / 240f);

		private void SetRainbowEffectsDirection()
		{
			foreach (var rainbowEffect in _devicesEffect)
			{
				rainbowEffect.SetDirection(Direction);
			}
		}

		private Color[] CreateStartColors(RainbowEffect deviceEffect)
		{
			var colors = new Color[deviceEffect.GetLedCountByDirection(Direction)];
			if (colors.Length == 0)
			{
				return colors;
			}

			colors[0] = currentColor;

			for (var colorIndex = 1; colorIndex < colors.Length; colorIndex++)
			{
				colors[colorIndex] = GetNextColor(colors[colorIndex - 1]);
			}

			currentColor = colors.Last();

			return colors;
		}

		private void UpdateColors(Dictionary<RainbowEffect, Color[]> devicesEffectToLedColor)
		{
			for (var deviceEffectIndex = 0; deviceEffectIndex < devicesEffectToLedColor.Count; deviceEffectIndex++)
			{
				var deviceEffectToLedColor = devicesEffectToLedColor.ElementAt(deviceEffectIndex);

				devicesEffectToLedColor[deviceEffectToLedColor.Key] = deviceEffectToLedColor.Value.Select((x, index) =>
					index != deviceEffectToLedColor.Value.Length - 1 ? deviceEffectToLedColor.Value[index + 1] : GetNextColor(x)).ToArray();

				currentColor = deviceEffectToLedColor.Value.Last();

				deviceEffectToLedColor.Key.SetColors(deviceEffectToLedColor.Value);
			}
		}

		private Color GetNextColor(Color color)
		{
			var hue = color.GetHue();
			var sat = color.GetSaturation();
			var lue = color.GetBrightness();

			return RGBColorHelper.HSLToRGBColor(hue + GetRefreshRate(), lue, sat);
		}

		private int GetRefreshRate()
		{
			switch (Speed)
			{
				case EffectSpeed.Slow:
				{
					return 15;
				}

				case EffectSpeed.Normal:
				{
					return 30;
				}

				default:
				{
					return 45;
				}
			}
		}

		public override void Stop()
		{
			rainbowTimer.Stop();
		}

		public override void Dispose()
		{
			rainbowTimer.Dispose();

			base.Dispose();
		}
	}
}
