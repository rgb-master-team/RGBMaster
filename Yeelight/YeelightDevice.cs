using Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yeelight
{
    public class YeelightDevice : Device
    {
        private readonly YeelightAPI.Device InternalDevice;

        public YeelightDevice(YeelightAPI.Device internalDevice)
        {
            InternalDevice = internalDevice;
        }

        public async override Task<byte> GetBrightnessPercentage()
        {
            // TODO - Also implement background lighting???
            // TODO2 - Keep the last known brightness at all time in a private member? is it a sensible approach?            
            var percentage = await InternalDevice.GetProp(YeelightAPI.Models.PROPERTIES.bright);
            return (byte)percentage;
        }

        public async override Task<Color> GetColor()
        {
            var hexColor = (int)(await InternalDevice.GetProp(YeelightAPI.Models.PROPERTIES.rgb));
            return Color.FromArgb(1, hexColor, hexColor, hexColor);
        }

        public async override Task SetBrightnessPercentage(byte brightness)
        {
            await InternalDevice.SetBrightness(brightness);
        }

        public async override Task SetColor(Color color)
        {
            await InternalDevice.SetRGBColor(color.R, color.G, color.B);
        }
    }
}
