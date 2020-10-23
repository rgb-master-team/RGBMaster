using Provider;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logitech
{
    public class LogitechMouseDevice : Device
    {
        public LogitechMouseDevice(LogitechMouseDeviceMetadata logitechDeviceMetadata) : base(logitechDeviceMetadata)
        {

        }

        protected override Task ConnectInternal()
        {
            return Task.CompletedTask;
        }

        protected override Task DisconnectInternal()
        {
            return Task.CompletedTask;
        }

        protected override Task<byte> GetBrightnessPercentageInternal()
        {
            throw new NotImplementedException();
        }

        protected override Task<Color> GetColorInternal()
        {
            throw new NotImplementedException();
        }

        protected override Task SetBrightnessPercentageInternal(byte brightness)
        {
            throw new NotImplementedException();
        }

        protected override Task SetColorInternal(Color color)
        {
            LogitechGSDK.LogiLedSetLightingForTargetZone(DeviceType.Mouse, 0, (int)(100 * (double)color.R / byte.MaxValue), (int)(100 * (double)color.G / byte.MaxValue), (int)(100 * (double)color.B / byte.MaxValue));
            LogitechGSDK.LogiLedSetLightingForTargetZone(DeviceType.Mouse, 1, (int)(100 * (double)color.R / byte.MaxValue), (int)(100 * (double)color.G / byte.MaxValue), (int)(100 * (double)color.B / byte.MaxValue));

            return Task.CompletedTask;
        }

        protected override Task TurnOffInternal()
        {
            throw new NotImplementedException();
        }

        protected override Task TurnOnInternal()
        {
            throw new NotImplementedException();
        }
    }
}
