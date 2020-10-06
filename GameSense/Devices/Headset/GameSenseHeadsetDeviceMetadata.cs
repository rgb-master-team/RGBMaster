using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSense.Devices.Headset
{
    public class GameSenseHeadsetDeviceMetadata : DeviceMetadata
    {
        public GameSenseHeadsetDeviceMetadata(Guid discoveringProvider, string deviceName) : base(discoveringProvider, DeviceType.Headset, deviceName, new HashSet<OperationType>() { OperationType.SetColor })
        {
        }
    }
}
