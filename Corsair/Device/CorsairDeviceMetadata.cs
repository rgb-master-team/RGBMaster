using Common;
using System.Collections.Generic;
using System.Drawing;

namespace Corsair.Provider
{
    public class CorsairDeviceMetadata : DeviceMetadata
    {
        public CorsairDeviceMetadata(DeviceType deviceType, string deviceName, HashSet<OperationType> supportedOperations) : base(deviceType, deviceName, supportedOperations)
        {
        }
    }
}