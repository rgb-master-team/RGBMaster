using System;
using System.Drawing;
using System.Linq;
using Corsair.Device;
using Corsair.Led;
using Infrastructure.Effects;

namespace Corsair.Effects
{
	public class CorsairRainbowEffect : RainbowEffect
	{
		public CorsairRainbowEffect(CorsairDevice device) : base(device)
		{
		}

		public override void SetColors(Color[] dimensionColors)
		{
			var corsairDevice = (CorsairDevice)_device;
			var getOppositeDimensionSize = corsairDevice.GetDimensionSizeGetter(OppositeDimension);

			var deviceLength = corsairDevice.LedPositions.LedPosition.Max(getOppositeDimensionSize);

			var dimensionStep = deviceLength / dimensionColors.Length;
			_device.SetColors(corsairDevice.LedPositions.LedPosition.Select(x => GetLedColor(x, getOppositeDimensionSize, dimensionStep, deviceLength, dimensionColors)).ToArray());
		}

		private Color GetLedColor(CorsairLedPosition ledPosition,
			Func<CorsairLedPosition, double> getOppositeDimensionSize,
			double dimensionStep,
			double deviceLength,
			Color[] dimensionColors)
		{
			var currentLedDistance = Direction < 0 ? getOppositeDimensionSize(ledPosition) : deviceLength - getOppositeDimensionSize(ledPosition);

			return dimensionColors[(int)Math.Abs(Math.Round(currentLedDistance / dimensionStep) - 1)];
		}
	}
}
