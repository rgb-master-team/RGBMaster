using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Corsair.Device;
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
			var getOppositeDimensionSize = corsairDevice.GetDimensionSizeGetter(OppositeDirection);

			var dimensionStep = corsairDevice.LedPositions.LedPosition.Max(getOppositeDimensionSize) / dimensionColors.Length;
			_device.SetColors(corsairDevice.LedPositions.LedPosition.Select(x => dimensionColors[(int)Math.Abs(Math.Round(getOppositeDimensionSize(x) / dimensionStep) - 1)]).ToArray());
		}
	}
}
