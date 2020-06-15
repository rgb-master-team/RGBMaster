using Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Common
{
    public abstract class DeviceMetadata
    {
        public abstract string DeviceName { get; }
        public abstract HashSet<OperationType> SupportedOperations { get; }
        public abstract Bitmap DeviceIcon { get; }
    }
}
