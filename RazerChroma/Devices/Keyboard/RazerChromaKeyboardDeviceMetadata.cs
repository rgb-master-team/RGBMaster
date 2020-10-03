using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RazerChroma.Devices.Keyboard
{
    public class RazerChromaKeyboardDeviceMetadata : DeviceMetadata
    {
        public RazerChromaKeyboardDeviceMetadata(Guid discoveringProvider, string deviceName, HashSet<OperationType> operationTypes) : base(discoveringProvider, DeviceType.Keyboard, deviceName, operationTypes)
        {
        }
    }
}
