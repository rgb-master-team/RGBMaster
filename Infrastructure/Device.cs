using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public abstract class Device
    {
        public abstract Task<Color> GetColor();
        public abstract Task SetColor(Color color);

        public abstract Task<byte> GetBrightnessPercentage();
        public abstract Task SetBrightnessPercentage(byte brightness);
        
        // TODO - Exceptions or error messages or both? Hmmmst..
    }
}
