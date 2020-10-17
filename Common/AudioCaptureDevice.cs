using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public enum AudioCaptureDeviceFlowType
    {
        Unknown,
        Input,
        Output,
    }

    public class AudioCaptureDevice
    {
        public string Id { get; }
        public string DeviceFriendlyName { get; }
        public AudioCaptureDeviceFlowType FlowType { get; }
        public string GlyphIcon
        {
            get
            {
                switch (FlowType)
                {
                    case AudioCaptureDeviceFlowType.Input:
                        return "\uE720";
                    case AudioCaptureDeviceFlowType.Output:
                        return "\uE7F5";
                    default:
                        return "\uE9CE";
                }
            }
        }

        public AudioCaptureDevice(string id, string deviceFriendlyName, AudioCaptureDeviceFlowType flowType)
        {
            Id = id;
            DeviceFriendlyName = deviceFriendlyName;
            FlowType = flowType;
        }
    }
}
