using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logitech.Devices.Speaker
{
    public class LogitechSpeakerDeviceMetadata : DeviceMetadata
    {
        public LogitechSpeakerDeviceMetadata(Guid discoveringProvider, string deviceName) : base(discoveringProvider, Common.DeviceType.Speaker, deviceName, new HashSet<OperationType>() { OperationType.SetColor })
        {
        }

    }
}
