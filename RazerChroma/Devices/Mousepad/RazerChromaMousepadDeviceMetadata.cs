using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RazerChroma.Devices.Mousepad
{
    public class RazerChromaMousepadDeviceMetadata : DeviceMetadata
    {
        public override string DeviceName => "Razer Chroma Mousepad";
        public override HashSet<OperationType> SupportedOperations => new HashSet<OperationType>() { OperationType.SetColor };

        public RazerChromaMousepadDeviceMetadata() : base(DeviceType.Mousepad)
        {
        }
    }
}
