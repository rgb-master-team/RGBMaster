using Common;
using GameSense.API;
using GameSense.Devices.Headset;
using GameSense.Devices.Mouse;
using GameSense.DeviceScanner;
using Provider;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameSense
{
    public class GameSenseProvider : BaseProvider
    {
        private readonly GSAPI gameSenseAPI;

        public GameSenseProvider() : base(new GameSenseProviderMetadata())
        {
            gameSenseAPI = new GSAPI();
        }

        protected override Task<List<Device>> InternalDiscover(CancellationToken cancellationToken = default)
        {
            var scannedAndMappedDevices = SteelSeriesDeviceScanner.ScanGameSenseDevices();

            var gameSenseDevices = new List<Device>();

            foreach (var scannedDeviceMapping in scannedAndMappedDevices)
            {
                switch (scannedDeviceMapping.DeviceType)
                {
                    case DeviceType.Headset:
                        gameSenseDevices.Add(new GameSenseHeadsetDevice(gameSenseAPI, scannedDeviceMapping, new GameSenseHeadsetDeviceMetadata(ProviderMetadata.ProviderGuid, scannedDeviceMapping.DeviceName)));
                        break;
                    case DeviceType.Unknown:
                    case DeviceType.Lightbulb:
                    case DeviceType.LedStrip:
                    case DeviceType.Keyboard:
                    case DeviceType.Mouse:
                    case DeviceType.Fan:
                    case DeviceType.Mousepad:
                    case DeviceType.Speaker:
                    case DeviceType.Keypad:
                    case DeviceType.Memory:
                    case DeviceType.GPU:
                    case DeviceType.Motherboard:
                    case DeviceType.Chair:
                    case DeviceType.AllDevices:
                    default:
                        break;
                }
            }

            //return Task.FromResult(new List<Device>() { new GameSenseHeadsetDevice(gameSenseAPI, new GameSenseHeadsetDeviceMetadata(ProviderMetadata.ProviderGuid, "GameSense Headset Device")), new GameSenseMouseDevice(gameSenseAPI,new GameSenseMouseDeviceMetadata(ProviderMetadata.ProviderGuid, "GameSense Mouse Device")) });
            return Task.FromResult(gameSenseDevices);
        }

        protected override async Task InternalUnregister(CancellationToken cancellationToken = default)
        {
            await gameSenseAPI.RemoveGame(new GSApiRemoveGamePayload()
            {
                Game = GameSenseConstants.RGB_MASTER_GAME_NAME
            }, cancellationToken).ConfigureAwait(false);
        }

        protected override async Task InternalRegister(CancellationToken cancellationToken = default)
        {
            gameSenseAPI.Initialize();
            await gameSenseAPI.RegisterGameMetadata(new GSApiGameMetadata()
            {
                Game = GameSenseConstants.RGB_MASTER_GAME_NAME,
                Developer = GameSenseConstants.RGB_MASTER_GAME_DEVELOPER,
                GameDisplayName = GameSenseConstants.RGB_MASTER_GAME_NAME
            }, cancellationToken).ConfigureAwait(false);
        }
    }
}
