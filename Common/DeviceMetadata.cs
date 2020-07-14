using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;

namespace Common
{
    public class DeviceMetadata
    {
        public Guid DeviceGuid { get; }
        public virtual string DeviceName { get; }
        public virtual HashSet<OperationType> SupportedOperations { get; }
        public DeviceType DeviceType { get; private set; }
        public virtual string DeviceIconAssetPath
        {
            get
            {
                switch (DeviceType)
                {
                    case DeviceType.Unknown:
                        return @"/Assets/Icons/Unknown.png";
                    case DeviceType.Lightbulb:
                        return @"/Assets/Icons/Lightbulb.png";
                    case DeviceType.LedStrip:
                        return @"/Assets/Icons/Ledstrip.png";
                    case DeviceType.Keyboard:
                        return @"/Assets/Icons/Keyboard.png";
                    case DeviceType.Mouse:
                        return @"/Assets/Icons/Mouse.png";
                    case DeviceType.Fan:
                        return @"/Assets/Icons/Fan.png";
                    case DeviceType.Mousepad:
                        return @"/Assets/Icons/Mousepad.png";
                    case DeviceType.Speaker:
                        return @"/Assets/Icons/Speaker.png";
                    case DeviceType.Headset:
                        return @"/Assets/Icons/Headset.png";
                    case DeviceType.Keypad:
                        return @"/Assets/Icons/Keypad.png";
                    case DeviceType.Memory:
                        return @"/Assets/Icons/Memory.png";
                    case DeviceType.GPU:
                        return @"/Assets/Icons/GPU.png";
                    case DeviceType.Motherboard:
                        return @"/Assets/Icons/Motherboard.png";
                    case DeviceType.Chair:
                        return @"/Assets/Icons/Chair.png";
                    default:
                        return @"/Assets/Icons/Lightbulb.png";
                }
            }
        }

        public DeviceMetadata(DeviceType deviceType = DeviceType.Unknown)
        {
            DeviceGuid = Guid.NewGuid();
            DeviceType = deviceType;
        }
    }
}
