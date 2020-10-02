using Common;
using System.Collections.Generic;
using System.Drawing;

namespace MagicHome
{
    public class MagicHomeDeviceMetadata : DeviceMetadata
    {
        public MagicHomeDeviceMetadata(string deviceName) : base(DeviceType.Lightbulb, deviceName, new HashSet<OperationType>() { OperationType.SetColor, OperationType.TurnOn, OperationType.TurnOff /*, OperationType.SetBrightness*/ })
        {

        }
    }
}