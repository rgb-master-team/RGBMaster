using Common;
using System.Collections.Generic;
using System.Drawing;

namespace Yeelight
{
    public class YeelightDeviceMetadata : DeviceMetadata
    {
        public YeelightDeviceMetadata(DeviceType deviceType, string deviceName, HashSet<OperationType> supportedOps) : base(deviceType, deviceName, supportedOps)
        {
        }
    }
}