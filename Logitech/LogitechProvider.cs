using Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RgbMasterDeviceType = Common.DeviceType;

namespace Logitech
{
    public class LogitechProvider : BaseProvider
    {
        private static int VENDOR_ID = 0x046D;

        private static RgbMasterDeviceType GetDeviceTypeForLogitech(Logitech.DeviceType logitechDevicesType)
        {
            switch (logitechDevicesType)
            {
                case DeviceType.Keyboard:
                    return RgbMasterDeviceType.Keyboard;
                case DeviceType.Mouse:
                    return RgbMasterDeviceType.Mouse;
                case DeviceType.Mousemat:
                    return RgbMasterDeviceType.Mousepad;
                case DeviceType.Headset:
                    return RgbMasterDeviceType.Headset;
                case DeviceType.Speaker:
                    return RgbMasterDeviceType.Speaker;
                default:
                    return RgbMasterDeviceType.Unknown;
            }
        }

        public LogitechProvider(): base(new LogitechProviderMetadata())
        {

        }

        public override Task<List<Device>> Discover()
        {
            return Task.FromResult(new List<Device>() { new LogitechMouseDevice(new LogitechMouseDeviceMetadata(ProviderMetadata.ProviderGuid, "All Logitech G Hub devices")) });
            /*
            // Set all devices to Black
            LogitechGSDK.LogiLedSetLighting(0, 0, 0);

            // Set some keys on keyboard
            LogitechGSDK.LogiLedSetLightingForKeyWithKeyName(KeyboardNames.L, 0, 100, 100);
            LogitechGSDK.LogiLedSetLightingForKeyWithKeyName(KeyboardNames.O, 0, 100, 100);
            LogitechGSDK.LogiLedSetLightingForKeyWithKeyName(KeyboardNames.G, 0, 100, 100);
            LogitechGSDK.LogiLedSetLightingForKeyWithKeyName(KeyboardNames.I, 0, 100, 100);


            
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
        }

        protected override Task InternalRegister()
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

        protected override Task InternalUnregister()
        {
            LogitechGSDK.LogiLedShutdown();
            return Task.CompletedTask;
        }
    }
}
