using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RazerChroma.Devices.Mousepad
{
    public class RazerChromaMousepadDeviceMetadata : DeviceMetadata
    {
        public RazerChromaMousepadDeviceMetadata(string deviceName, HashSet<OperationType> supportedOps) : base(DeviceType.Mousepad, deviceName, supportedOps)
        {
        }
    }
}
