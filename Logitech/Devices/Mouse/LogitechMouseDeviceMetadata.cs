using Common;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Logitech
{
    public class LogitechMouseDeviceMetadata : DeviceMetadata
    {
        public LogitechMouseDeviceMetadata(Guid discoveringProvider, string deviceName) : base(discoveringProvider, Common.DeviceType.Mouse, deviceName, new HashSet<OperationType>() { OperationType.SetColor })
        {
        }
    }
}