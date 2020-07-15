using Provider;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RgbMasterDeviceType = Common.DeviceType;

namespace Logitech
{
    public class LogitechDevice : Device
    {

        private static RgbMasterDeviceType GetDeviceTypeForLogitech(Logitech.DeviceType logitechDevicesType)
        {
            switch (logitechDevicesType)
            {
                case DeviceType.Keyboard:
                    return RgbMasterDeviceType.Keyboard;
                case DeviceType.Mouse:
                    return RgbMasterDeviceType.Mouse;
                case DeviceType.Mousemat:
                    return RgbMasterDeviceType.Mousepad;
                case DeviceType.Headset:
                    return RgbMasterDeviceType.Headset;
                case DeviceType.Speaker:
                    return RgbMasterDeviceType.Speaker;
                default:
                    return RgbMasterDeviceType.Unknown;
            }
        }
        public LogitechDevice() : base(new LogitechDeviceMetadata())
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

        protected override byte GetBrightnessPercentageInternal()
        {
            throw new NotImplementedException();
        }

        protected override Color GetColorInternal()
        {
            throw new NotImplementedException();
        }

        protected override void SetBrightnessPercentageInternal(byte brightness)
        {
            throw new NotImplementedException();
        }

        protected override void SetColorInternal(Color color)
        {
            LogitechGSDK.LogiLedSetLightingForTargetZone(DeviceType.Mouse, 1, 100 * (color.R / byte.MaxValue), 100 * (color.G / byte.MaxValue), 100 * (color.B / byte.MaxValue));
        }

        protected override void TurnOffInternal()
        {
            throw new NotImplementedException();
        }

        protected override void TurnOnInternal()
        {
            throw new NotImplementedException();
        }
    }
}
