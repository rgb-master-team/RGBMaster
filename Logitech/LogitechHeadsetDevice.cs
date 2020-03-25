using Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logitech
{
    public class LogitechHeadsetDevice : Device
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
            throw new NotImplementedException();
        }
    }
}
