using Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider
{
    public abstract class Device : Device<DeviceMetadata>
    {
        public Device() : base(new DeviceMetadata())
        {
        }
    }

    public abstract class Device<DeviceMd> where DeviceMd : DeviceMetadata
    {
        public readonly DeviceMd DeviceMetadata;

        public Device(DeviceMd deviceMetadata)
        {
            this.DeviceMetadata = deviceMetadata;
        }

        public bool IsConnected { get; }
        public abstract Color GetColor();
        public abstract void SetColor(Color color);
        public abstract byte GetBrightnessPercentage();
        public abstract void SetBrightnessPercentage(byte brightness);
        public abstract Task Connect();
        public abstract Task Disconnect();
    }
}
