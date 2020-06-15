using Common;
using System.Collections.Generic;
using System.Drawing;

namespace MagicHome
{
    public class MagicHomeDeviceMetadata : DeviceMetadata
    {
        public override string DeviceName => "Magic Home Device";

        public override HashSet<OperationType> SupportedOperations => new HashSet<OperationType>() { OperationType.SetColor/*, OperationType.SetBrightness*/ };

        public override Bitmap DeviceIcon => throw new System.NotImplementedException();
    }
}