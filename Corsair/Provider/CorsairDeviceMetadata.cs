using Common;
using System.Collections.Generic;
using System.Drawing;

namespace Corsair.Provider
{
    public class CorsairDeviceMetadata : DeviceMetadata
    {
        private readonly string deviceName;

        public CorsairDeviceMetadata(string deviceName)
        {
            this.deviceName = deviceName;
        }

        public override string DeviceName => deviceName;

        public override HashSet<OperationType> SupportedOperations => new HashSet<OperationType>() { OperationType.SetColor };

    }
}