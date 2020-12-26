using Provider;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace Logitech.Devices.Keyboard
{
    public class LogitechKeyboardDevice : Device
    {
        public LogitechKeyboardDevice(LogitechKeyboardDeviceMetadata logitechDeviceMetadata) : base(logitechDeviceMetadata) { }

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
            LogitechGSDK.LogiLedSetLightingForTargetZone(DeviceType.Keyboard, 0, (int)(100 * (double)color.R / byte.MaxValue), (int)(100 * (double)color.G / byte.MaxValue), (int)(100 * (double)color.B / byte.MaxValue));
            LogitechGSDK.LogiLedSetLightingForTargetZone(DeviceType.Keyboard, 1, (int)(100 * (double)color.R / byte.MaxValue), (int)(100 * (double)color.G / byte.MaxValue), (int)(100 * (double)color.B / byte.MaxValue));

            return Task.CompletedTask;
        }

        protected override Task SetColorSmoothlyInternal(Color color, int relativeSmoothness)
        {
            throw new NotImplementedException();
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
