using Common;
using System.Collections.Generic;
using System.Drawing;

namespace Logitech
{
    public class LogitechDeviceMetadata : DeviceMetadata
    {
        private readonly HashSet<OperationType> logitechSupportedOps = new HashSet<OperationType>() { OperationType.SetColor };

        public override string DeviceName => "Logitech Mouse";

        public override HashSet<OperationType> SupportedOperations => logitechSupportedOps;

        public override Bitmap DeviceIcon => null;
    }
}