using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSense.Devices.Headset
{
    public class GameSenseHeadsetDeviceMetadata : DeviceMetadata
    {
        public GameSenseHeadsetDeviceMetadata() : base(DeviceType.Headset)
        {
        }

        public override string DeviceName => "SteelSeries Headset";
        public override HashSet<OperationType> SupportedOperations => new HashSet<OperationType>() { OperationType.SetColor };
    }
}
