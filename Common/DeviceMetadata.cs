using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Common
{
    public class DeviceMetadata
    {
        public virtual string DeviceName { get; }
        public virtual HashSet<OperationType> SupportedOperations { get; }
        public virtual Bitmap DeviceIcon { get; }
    }
}
