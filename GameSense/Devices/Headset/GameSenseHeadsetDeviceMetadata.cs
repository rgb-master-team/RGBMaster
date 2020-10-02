using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSense.Devices.Headset
{
    public class GameSenseHeadsetDeviceMetadata : DeviceMetadata
    {
        public GameSenseHeadsetDeviceMetadata(string deviceName) : base(DeviceType.Headset, deviceName, new HashSet<OperationType>() { OperationType.SetColor })
        {
        }
    }
}
