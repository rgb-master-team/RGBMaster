using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RazerChroma.Devices.Keyboard
{
    public class RazerChromaKeyboardDeviceMetadata : DeviceMetadata
    {
        public RazerChromaKeyboardDeviceMetadata(string deviceName, HashSet<OperationType> operationTypes) : base(DeviceType.Keyboard, deviceName, operationTypes)
        {
        }
    }
}
