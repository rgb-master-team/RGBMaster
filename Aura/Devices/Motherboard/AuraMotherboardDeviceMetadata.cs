using Common;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Aura
{
    public class AuraMotherboardDeviceMetadata : DeviceMetadata
    {
        public AuraMotherboardDeviceMetadata(Guid discoveringProvider, string deviceName, HashSet<OperationType> supportedOps) : base(discoveringProvider, DeviceType.Motherboard, deviceName, supportedOps)
        {
        }
    }
}