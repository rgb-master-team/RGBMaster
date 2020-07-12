using Common;
using System.Collections.Generic;
using System.Drawing;

namespace Aura
{
    public class AuraDeviceMetadata : DeviceMetadata
    {
        public override string DeviceName => "Unknown Aura Device";

        public override HashSet<OperationType> SupportedOperations => new HashSet<OperationType>() { OperationType.SetColor };
    }
}