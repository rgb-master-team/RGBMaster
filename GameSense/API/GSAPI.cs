using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static System.Environment;

namespace GameSense.API
{
    public class GSAPI
    {
        private bool isInitialized = false;
        private RestClient gameSenseRestClient;

        public GSAPI()
        {
            

        }

        public void Initialize()
        {
            // Windows path - %PROGRAMDATA%/SteelSeries/SteelSeries Engine 3/coreProps.json

            if (!isInitialized && OSVersion.Platform == PlatformID.Win32NT)
            {
                var corePropsPath = Path.Combine(GetFolderPath(SpecialFolder.CommonApplicationData), @"SteelSeries/SteelSeries Engine 3/coreProps.json");
                var gameSenseCoreProps = JsonConvert.DeserializeObject<GSApiCoreProps>(File.ReadAllText(corePropsPath));
                gameSenseRestClient = new RestClient(gameSenseCoreProps.Address);
                
                isInitialized = true;
            }
        }

        public void RegisterGameMetadata(GSApiGameMetadata gameSenseGameMetadata)
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
