using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logitech
{
    public class LogitechAllPeripheralsDevice : Device
    {
        public LogitechAllPeripheralsDevice()
        {

        }

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

        public override Task<System.Drawing.Color> GetColor()
        {
            throw new NotImplementedException();
        }

        public override Task SetBrightnessPercentage(byte brightness)
        {
            throw new NotImplementedException();
        }

        public override Task SetColor(System.Drawing.Color color)
        {
            throw new NotImplementedException();
        }
    }
}
