using Common;
using GameSense.API;
using GameSense.Devices.Headset;
using GameSense.Devices.Mouse;
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
        private static int VENDOR_ID = 0x1038;
        public GameSenseProvider() : base(new GameSenseProviderMetadata())
        {
            gameSenseAPI = new GSAPI();
        }

        protected override Task<List<Device>> InternalDiscover(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new List<Device>() { new GameSenseHeadsetDevice(gameSenseAPI, new GameSenseHeadsetDeviceMetadata(ProviderMetadata.ProviderGuid, "GameSense Headset Device")), new GameSenseMouseDevice(gameSenseAPI,new GameSenseMouseDeviceMetadata(ProviderMetadata.ProviderGuid, "GameSense Mouse Device")) });
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
