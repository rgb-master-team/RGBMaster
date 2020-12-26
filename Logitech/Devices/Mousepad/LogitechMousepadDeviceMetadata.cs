using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logitech.Devices.Mousepad
{
    public class LogitechMousepadDeviceMetadata : DeviceMetadata
    {
        public LogitechMousepadDeviceMetadata(Guid discoveringProvider, string deviceName) : base(discoveringProvider, Common.DeviceType.Mousepad, deviceName, new HashSet<OperationType>() { OperationType.SetColor })
        {
        }

    }
}
