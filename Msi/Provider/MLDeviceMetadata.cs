using Common;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Msi.Provider
{
    public class MLDeviceMetadata : DeviceMetadata
    {
        public readonly string deviceType;

        public MLDeviceMetadata(Guid discoveringProvider, DeviceType deviceType, string deviceName) : base(discoveringProvider, deviceType, deviceName, new HashSet<OperationType>() { OperationType.SetColor })
        {
        }
    }
}