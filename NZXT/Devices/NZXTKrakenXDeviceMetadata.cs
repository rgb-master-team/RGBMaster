using Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace NZXT.Devices
{
    public class NZXTKrakenXDeviceMetadata : DeviceMetadata
    {
        public NZXTKrakenXDeviceMetadata(Guid rgbMasterDiscoveringProvider, string deviceName) : base(rgbMasterDiscoveringProvider, DeviceType.Fan, deviceName, new HashSet<OperationType>() { OperationType.SetColor})
        {
        }
    }
}
