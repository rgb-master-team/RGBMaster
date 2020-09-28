using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RazerChroma.Devices.Keyboard
{
    public class RazerChromaKeyboardDeviceMetadata : DeviceMetadata
    {
        public override string DeviceName => "Razer Chroma Keyboard";
        public override HashSet<OperationType> SupportedOperations => new HashSet<OperationType>() { OperationType.SetColor };

        public RazerChromaKeyboardDeviceMetadata() : base(DeviceType.Keyboard)
        {
        }
    }
}
