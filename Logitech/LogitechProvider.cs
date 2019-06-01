using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logitech
{
    public class LogitechProvider : Provider
    {
        private readonly List<OperationType> logitechSupportedOps = new List<OperationType>() { OperationType.SetColor, OperationType.SetBrightness };
        
        public override string ProviderName => "Logitech G Products Provider";

        public override IEnumerable<OperationType> SupportedOperations => logitechSupportedOps;

        public override Task<IEnumerable<Device>> Discover()
        {
            return Task.FromResult<IEnumerable<Device>>(new List<Device>() { new LogitechAllPeripheralsDevice() });
            

            // Set all devices to Black
            LogitechGSDK.LogiLedSetLighting(0, 0, 0);

            // Set some keys on keyboard
            LogitechGSDK.LogiLedSetLightingForKeyWithKeyName(KeyboardNames.L, 0, 100, 100);
            LogitechGSDK.LogiLedSetLightingForKeyWithKeyName(KeyboardNames.O, 0, 100, 100);
            LogitechGSDK.LogiLedSetLightingForKeyWithKeyName(KeyboardNames.G, 0, 100, 100);
            LogitechGSDK.LogiLedSetLightingForKeyWithKeyName(KeyboardNames.I, 0, 100, 100);


            /*
            // Set RGB mouse logo to Red
            LogitechGSDK.LogiLedSetLightingForTargetZone(DeviceType.Mouse, 1, 100, 0, 0);

            // Set G213 keyboard zones to Red, Yellow, Green, Cyan, Blue
            LogitechGSDK.LogiLedSetLightingForTargetZone(DeviceType.Keyboard, 1, 100, 0, 0);
            LogitechGSDK.LogiLedSetLightingForTargetZone(DeviceType.Keyboard, 2, 100, 100, 0);
            LogitechGSDK.LogiLedSetLightingForTargetZone(DeviceType.Keyboard, 3, 0, 100, 0);
            LogitechGSDK.LogiLedSetLightingForTargetZone(DeviceType.Keyboard, 4, 0, 100, 100);
            LogitechGSDK.LogiLedSetLightingForTargetZone(DeviceType.Keyboard, 5, 0, 0, 100);

            // Set G633/G933 headset logos to White, backsides to Purple
            LogitechGSDK.LogiLedSetLightingForTargetZone(DeviceType.Headset, 0, 100, 100, 100);
            LogitechGSDK.LogiLedSetLightingForTargetZone(DeviceType.Headset, 1, 100, 0, 100);
            */

            LogitechGSDK.LogiLedShutdown();
        }

        public override Task Register()
        {
            // Initialize the LED SDK
            bool LedInitialized = LogitechGSDK.LogiLedInit();

            if (!LedInitialized)
            {
                throw new LogitechInitFailedException();
            }

            LogitechGSDK.LogiLedSetTargetDevice(LogitechGSDK.LOGI_DEVICETYPE_ALL);

            return Task.CompletedTask;
        }

        public override Task Unregister()
        {
            LogitechGSDK.LogiLedShutdown();

            return Task.CompletedTask;
        }
    }
}
