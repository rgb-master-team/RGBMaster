using Common;
using System.Collections.Generic;
using System.Drawing;

namespace Msi.Provider
{
    public class MLDeviceMetadata : DeviceMetadata
    {
        public readonly string deviceType;

        public MLDeviceMetadata(string deviceType)
        {
            this.deviceType = deviceType;
        }

        public override string DeviceName => deviceType;

        public override HashSet<OperationType> SupportedOperations => new HashSet<OperationType>() { OperationType.SetColor };
    }
}