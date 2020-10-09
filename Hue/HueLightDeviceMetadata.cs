using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hue
{
    public class HueLightDeviceMetadata : DeviceMetadata
    {
        public HueLightDeviceMetadata(Guid rgbMasterDiscoveringProvider, string deviceName) : base(rgbMasterDiscoveringProvider, DeviceType.Lightbulb, deviceName, new HashSet<OperationType>() { OperationType.TurnOff, OperationType.TurnOn, OperationType.SetColor, OperationType.SetBrightness })
        {
        }
    }
}
