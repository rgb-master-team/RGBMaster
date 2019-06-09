using Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logitech
{
    public class LogitechKeyboardDevice : Device
    {
        private readonly HashSet<OperationType> logitechSupportedOps = new HashSet<OperationType>() { OperationType.SetColor, OperationType.SetBrightness };

        public override HashSet<OperationType> SupportedOperations => logitechSupportedOps;

        public override Task Connect()
        {
            throw new NotImplementedException();
        }

        public override Task Disconnect()
        {
            throw new NotImplementedException();
        }

        public override Task<byte> GetBrightnessPercentage()
        {
            throw new NotImplementedException();
        }

        public override Task<Color> GetColor()
        {
            throw new NotImplementedException();
        }

        public override Task SetBrightnessPercentage(byte brightness)
        {
            throw new NotImplementedException();
        }

        public override Task SetColor(Color color)
        {
            throw new NotImplementedException();
        }
    }
}
