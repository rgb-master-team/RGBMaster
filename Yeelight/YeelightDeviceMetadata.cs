using Common;
using System.Collections.Generic;
using System.Drawing;

namespace Yeelight
{
    public class YeelightDeviceMetadata : DeviceMetadata
    {
        private readonly string deviceName;

        public override string DeviceName
        {
            get
            {
                return deviceName;
            }
        }

        private readonly HashSet<OperationType> yeelightSupportedOps = new HashSet<OperationType>() { OperationType.GetBrightness, OperationType.SetBrightness, OperationType.GetColor, OperationType.SetColor };

        public YeelightDeviceMetadata(string deviceName)
        {
            this.deviceName = deviceName;
        }

        public override HashSet<OperationType> SupportedOperations => yeelightSupportedOps;

        public override Bitmap DeviceIcon => null;
    }
}