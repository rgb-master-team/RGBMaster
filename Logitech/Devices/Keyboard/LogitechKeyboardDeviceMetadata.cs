using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logitech.Devices.Keyboard
{
    public class LogitechKeyboardDeviceMetadata : DeviceMetadata
    {
        public LogitechKeyboardDeviceMetadata(Guid discoveringProvider, string deviceName) : base(discoveringProvider, Common.DeviceType.Keyboard, deviceName, new HashSet<OperationType>() { OperationType.SetColor}) { }
    }
}
