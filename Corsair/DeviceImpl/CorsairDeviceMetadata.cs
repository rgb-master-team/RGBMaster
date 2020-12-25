using Common;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Corsair.DeviceImpl
{
    public class CorsairDeviceMetadata : DeviceMetadata
    {
        public CorsairDeviceMetadata(Guid discoveringProvider, DeviceType deviceType, string deviceName, HashSet<OperationType> supportedOperations) : base(discoveringProvider, deviceType, deviceName, supportedOperations)
        {
        }
    }
}