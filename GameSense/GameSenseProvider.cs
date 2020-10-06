using Common;
using GameSense.API;
using GameSense.Devices.Headset;
using Provider;
using System;
using System.Collections.Generic;
using System.Text;
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

        public override Task<List<Device>> Discover()
        {
            return Task.FromResult(new List<Device>() { new GameSenseHeadsetDevice(gameSenseAPI, new GameSenseHeadsetDeviceMetadata(ProviderMetadata.ProviderGuid, "GameSense Headset Device"))});
        }

        protected override Task InternalUnregister()
        {
            gameSenseAPI.RemoveGame(new GSApiRemoveGamePayload()
            {
                Game = GameSenseConstants.RGB_MASTER_GAME_NAME
            });

            return Task.CompletedTask;
        }

        protected override Task InternalRegister()
        {
            gameSenseAPI.Initialize();
            gameSenseAPI.RegisterGameMetadata(new GSApiGameMetadata()
            {
                Game = GameSenseConstants.RGB_MASTER_GAME_NAME,
                Developer = GameSenseConstants.RGB_MASTER_GAME_DEVELOPER,
                GameDisplayName = GameSenseConstants.RGB_MASTER_GAME_NAME
            });

            return Task.CompletedTask;
        }
    }
}
