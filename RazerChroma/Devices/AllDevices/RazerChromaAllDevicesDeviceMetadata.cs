using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RazerChroma.Devices.AllDevices
{
    public class RazerChromaAllDevicesDeviceMetadata : DeviceMetadata
    {
        public RazerChromaAllDevicesDeviceMetadata() : base(DeviceType.AllDevices, "All Razer Chroma connected devices", new HashSet<OperationType>() { OperationType.SetColor, OperationType.TurnOff, OperationType.TurnOn })
        {
        }
    }
}
