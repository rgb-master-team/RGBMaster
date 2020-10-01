using Common;
using GameSense.API;
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

        public GameSenseProvider() : base(new GameSenseProviderMetadata())
        {
            gameSenseAPI = new GSAPI();
        }

        public override Task<List<Device>> Discover()
        {
            throw new NotImplementedException();
        }

        public override Task Unregister()
        {
            // TODO - Consider explicitly calling stop_game @ GameSense REST API.
            return Task.CompletedTask;
        }

        protected override Task Register()
        {
            gameSenseAPI.Initialize();
            gameSenseAPI.RegisterGameMetadata(new GSApiGameMetadata()
            {
                Game = "RGBMaster",
                Developer = "RGBMaster team",
                GameDisplayName = "RGBMaster"
            });

            return Task.CompletedTask;
        }
    }
}
