using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RazerChroma.Devices.AllDevices
{
    public class RazerChromaAllDevicesDeviceMetadata : DeviceMetadata
    {
        private readonly HashSet<OperationType> chromaSupportedOps = new HashSet<OperationType>() { OperationType.SetColor };

        public override string DeviceName => "All Razer Chroma connected devices";

        public override HashSet<OperationType> SupportedOperations => chromaSupportedOps;

        public RazerChromaAllDevicesDeviceMetadata() : base(DeviceType.Unknown)
        {
        }
    }
}
