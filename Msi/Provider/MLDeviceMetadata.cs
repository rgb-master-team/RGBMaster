using Common;
using System.Collections.Generic;
using System.Drawing;

namespace Msi.Provider
{
    public class MLDeviceMetadata : DeviceMetadata
    {
        public readonly string deviceType;

        public MLDeviceMetadata(DeviceType deviceType, string deviceName) : base(deviceType, deviceName, new HashSet<OperationType>() { OperationType.SetColor })
        {
        }
    }
}