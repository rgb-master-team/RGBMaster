using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace NZXT
{
    public class NZXTDeviceMetadata : DeviceMetadata
    {
        public NZXTDeviceMetadata(Guid rgbMasterDiscoveringProvider, string deviceName) : base(rgbMasterDiscoveringProvider, DeviceType.LedStrip, deviceName, new HashSet<OperationType>() { OperationType.SetColor })
        {
        }
    }
}
