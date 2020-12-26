using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSense.Devices.Mouse
{
    public class GameSenseMouseDeviceMetadata : DeviceMetadata
    {
        public GameSenseMouseDeviceMetadata(Guid discoveringProvider, string deviceName) : base(discoveringProvider, DeviceType.Mouse, deviceName, new HashSet<OperationType>() { OperationType.SetColor })
        {
        }
    }
}
