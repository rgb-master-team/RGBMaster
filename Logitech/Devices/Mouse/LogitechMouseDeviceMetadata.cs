using Common;
using System.Collections.Generic;
using System.Drawing;

namespace Logitech
{
    public class LogitechMouseDeviceMetadata : DeviceMetadata
    {
        public LogitechMouseDeviceMetadata(string deviceName) : base(Common.DeviceType.Mouse, deviceName, new HashSet<OperationType>() { OperationType.SetColor })
        {
        }
    }
}