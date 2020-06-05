using Infrastructure;
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
        private readonly HashSet<OperationType> logitechSupportedOps = new HashSet<OperationType>() { OperationType.SetColor };

        public override string DeviceName => "Logitech Mouse";

        public override HashSet<OperationType> SupportedOperations => logitechSupportedOps;

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
