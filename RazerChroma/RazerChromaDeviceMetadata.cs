using Common;
using System.Collections.Generic;
using System.Drawing;

namespace RazerChroma
{
    public class RazerChromaDeviceMetadata : DeviceMetadata
    {
        private readonly HashSet<OperationType> chromaSupportedOps = new HashSet<OperationType>() { OperationType.SetColor };

        public override string DeviceName => "All Razer Chroma connected devices";

        public override HashSet<OperationType> SupportedOperations => chromaSupportedOps;

        public override Bitmap DeviceIcon => null;
    }
}