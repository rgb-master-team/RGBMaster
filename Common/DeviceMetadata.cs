using System;
using System.Collections.Generic;

namespace Common
{
    public class DeviceMetadata
    {
        public Guid RgbMasterDiscoveringProvider { get; }
        public Guid RgbMasterDeviceGuid { get; }
        public DeviceInterface DeviceInterface { get; }
        public string DeviceName { get; }
        public HashSet<OperationType> SupportedOperations { get; }
        public DeviceType DeviceType { get; }
        public virtual string DeviceIconAssetPath
        {
            get
            {
                switch (DeviceType)
                {
                    case DeviceType.Unknown:
                        return @"/Assets/Icons/White/Unknown.png";
                    case DeviceType.Lightbulb:
                        return @"/Assets/Icons/White/Lightbulb.png";
                    case DeviceType.LedStrip:
                        return @"/Assets/Icons/White/Ledstrip.png";
                    case DeviceType.Keyboard:
                        return @"/Assets/Icons/White/Keyboard.png";
                    case DeviceType.Mouse:
                        return @"/Assets/Icons/White/Mouse.png";
                    case DeviceType.Fan:
                        return @"/Assets/Icons/White/Fan.png";
                    case DeviceType.Mousepad:
                        return @"/Assets/Icons/White/Mousepad.png";
                    case DeviceType.Speaker:
                        return @"/Assets/Icons/White/Speaker.png";
                    case DeviceType.Headset:
                        return @"/Assets/Icons/White/Headset.png";
                    case DeviceType.Keypad:
                        return @"/Assets/Icons/White/Keypad.png";
                    case DeviceType.Memory:
                        return @"/Assets/Icons/White/Memory.png";
                    case DeviceType.GPU:
                        return @"/Assets/Icons/White/GPU.png";
                    case DeviceType.Motherboard:
                        return @"/Assets/Icons/White/Motherboard.png";
                    case DeviceType.Chair:
                        return @"/Assets/Icons/White/Chair.png";
                    case DeviceType.HeadphoneStand:
                        return @"/Assets/Icons/White/HeadphoneStand.png";
                    case DeviceType.AllDevices:
                        return @"/Assets/Icons/White/AllDevices.png";
                    default:
                        throw new NotSupportedException($"An icon is not mapped to device type {DeviceType}.");
                }
            }
        }

        public DeviceMetadata(Guid rgbMasterDiscoveringProvider, DeviceType deviceType, string deviceName, HashSet<OperationType> supportedOperations, DeviceInterface deviceInterface = null)
        {
            RgbMasterDiscoveringProvider = rgbMasterDiscoveringProvider;
            RgbMasterDeviceGuid = Guid.NewGuid();
            DeviceType = deviceType;
            DeviceName = deviceName;
            SupportedOperations = supportedOperations;
            DeviceInterface = deviceInterface;
        }

        public bool IsOperationSupported(OperationType op)
        {
            return SupportedOperations.Contains(op);
        }
    }
}
