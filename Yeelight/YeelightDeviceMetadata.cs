using Common;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Yeelight
{
    public class YeelightDeviceMetadata : DeviceMetadata
    {
        public YeelightDeviceMetadata(Guid discoveringProvider, DeviceType deviceType, string deviceName, HashSet<OperationType> supportedOps) : base(discoveringProvider, deviceType, deviceName, supportedOps)
        {
        }
    }
}