using Provider;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logitech
{
    public class LogitechDevice : Device<LogitechDeviceMetadata>
    {
        public LogitechDevice() : base(new LogitechDeviceMetadata())
        {

        }

        public override Task Connect()
        {
            return Task.CompletedTask;
        }

        public override Task Disconnect()
        {
            return Task.CompletedTask;
        }

        public override byte GetBrightnessPercentage()
        {
            throw new NotImplementedException();
        }

        public override Color GetColor()
        {
            throw new NotImplementedException();
        }

        public override void SetBrightnessPercentage(byte brightness)
        {
            throw new NotImplementedException();
        }

        public override void SetColor(Color color)
        {
            LogitechGSDK.LogiLedSetLightingForTargetZone(DeviceType.Mouse, 1, 100 * (color.R / byte.MaxValue), 100 * (color.G / byte.MaxValue), 100 * (color.B / byte.MaxValue));
        }
    }
}
