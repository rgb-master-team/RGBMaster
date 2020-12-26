using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logitech.Devices.Headset
{
    public class LogitechHeadsetDeviceMetadata : DeviceMetadata
    {
        public LogitechHeadsetDeviceMetadata(Guid discoveringProvider, string deviceName) : base(discoveringProvider, Common.DeviceType.Headset, deviceName, new HashSet<OperationType>() { OperationType.SetColor }) { }
    }
}
