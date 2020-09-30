using Common;
using Provider;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameSense
{
    public class GameSenseProvider : BaseProvider
    {
        private readonly GameSenseAPI gameSenseAPI;

        public GameSenseProvider() : base(new GameSenseProviderMetadata())
        {
            gameSenseAPI = new GameSenseAPI();
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
            gameSenseAPI.RegisterGameMetadata(new GameSenseGameMetadata()
            {
                Game = "RGBMaster",
                Developer = "RGBMaster team",
                GameDisplayName = "RGBMaster"
            });

            return Task.CompletedTask;
        }
    }
}
