using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicHome
{
    public class MagicHomeDevice : Device
    {
        private Light InternalLight;
        public override HashSet<OperationType> SupportedOperations => new HashSet<OperationType>() { OperationType.SetColor, OperationType.SetBrightness };

        public MagicHomeDevice(Light InternalLight)
        {
            this.InternalLight = InternalLight;
            this.InternalLight.Logger.Enabled = false;
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

        public override System.Drawing.Color GetColor()
        {
            throw new NotImplementedException();
        }

        public override void SetBrightnessPercentage(byte brightness)
        {
            InternalLight.SetBrightness(brightness);
        }

        public override void SetColor(System.Drawing.Color color)
        {
            InternalLight.SetColor(color.R, color.G, color.B);
        }
    }
}
