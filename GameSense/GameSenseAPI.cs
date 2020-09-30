using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static System.Environment;

namespace GameSense
{
    public class GameSenseAPI
    {
        private bool isInitialized = false;
        private RestClient gameSenseRestClient;

        public GameSenseAPI()
        {
            

        }

        public void Initialize()
        {
            // Windows path - %PROGRAMDATA%/SteelSeries/SteelSeries Engine 3/coreProps.json

            if (!isInitialized && OSVersion.Platform == PlatformID.Win32NT)
            {
                var corePropsPath = Path.Combine(GetFolderPath(SpecialFolder.CommonApplicationData), @"SteelSeries/SteelSeries Engine 3/coreProps.json");
                var gameSenseCoreProps = JsonConvert.DeserializeObject<GameSenseCoreProps>(File.ReadAllText(corePropsPath));
                gameSenseRestClient = new RestClient(gameSenseCoreProps.Address);
                
                isInitialized = true;
            }
        }

        public void RegisterGameMetadata(GameSenseGameMetadata gameSenseGameMetadata)
        {
            var restRequest = new RestRequest("game_metadata");
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddJsonBody(gameSenseGameMetadata);

            var restResponse = gameSenseRestClient.Post(restRequest);

            if (!restResponse.IsSuccessful)
            {
                throw new Exception("Failed to register game metadata in SteelSeries GameSense REST API", restResponse.ErrorException);
            }
        }

        public void BindGameEvent()
        {

        }
    }
}
