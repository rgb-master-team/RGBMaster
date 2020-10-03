using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RazerChroma.Devices.Mousepad
{
    public class RazerChromaMousepadDeviceMetadata : DeviceMetadata
    {
        public RazerChromaMousepadDeviceMetadata(Guid discoveringProvider, string deviceName, HashSet<OperationType> supportedOps) : base(discoveringProvider, DeviceType.Mousepad, deviceName, supportedOps)
        {
        }
    }
}
