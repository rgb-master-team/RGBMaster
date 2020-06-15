using Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public abstract class Device<DeviceMd> where DeviceMd : DeviceMetadata
    {
        public DeviceMd DeviceMetadata { get; set; }

        public bool IsConnected { get; }
        public abstract string DeviceName { get; }
        public abstract HashSet<OperationType> SupportedOperations { get; }
        public abstract Color GetColor();
        public abstract void SetColor(Color color);
        public abstract byte GetBrightnessPercentage();
        public abstract void SetBrightnessPercentage(byte brightness);
        public abstract Task Connect();
        public abstract Task Disconnect();

        // TODO - Exceptions or error messages or both? Hmmmst..
    }
}
