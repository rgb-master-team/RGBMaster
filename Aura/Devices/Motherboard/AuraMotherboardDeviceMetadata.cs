using Common;
using System.Collections.Generic;
using System.Drawing;

namespace Aura
{
    public class AuraMotherboardDeviceMetadata : DeviceMetadata
    {
        public AuraMotherboardDeviceMetadata(string deviceName, HashSet<OperationType> supportedOps) : base(DeviceType.Motherboard, deviceName, supportedOps)
        {
        }
    }
}