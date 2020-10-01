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
                gameSenseRestClient = new RestClient($"http://{gameSenseCoreProps.Address}");
                
                isInitialized = true;
            }
        }

        public void RegisterGameMetadata(GSApiGameMetadata gameSenseGameMetadata)
        {
            var restRequest = new RestRequest("game_metadata");
            restRequest.AddHeader("Content-Type", "application/json");

            var bodyJson = JsonConvert.SerializeObject(gameSenseGameMetadata, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            restRequest.AddParameter(null, bodyJson, ParameterType.RequestBody);

            var restResponse = gameSenseRestClient.Post(restRequest);

            if (!restResponse.IsSuccessful)
            {
                throw new Exception("Failed to register game metadata in SteelSeries GameSense REST API", new Exception(restResponse.Content));
            }
        }

        public void BindGameEvent(GSApiBindEventPayload GSApiBindEventPayload)
        {
            var restRequest = new RestRequest("bind_game_event");
            restRequest.AddHeader("Content-Type", "application/json");

            var bodyJson = JsonConvert.SerializeObject(GSApiBindEventPayload, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

            restRequest.AddParameter(null, bodyJson, ParameterType.RequestBody);

            var restResponse = gameSenseRestClient.Post(restRequest);
            if (!restResponse.IsSuccessful)
            
            {
                throw new Exception("Failed to bind event payload in SteelSeries GameSense REST API", new Exception(restResponse.Content));
            }
        }

        public void RegisterGameEvent(GSApiRegisterGameEventPayload gSApiRegisterGameEventPayload)
        {
            var restRequest = new RestRequest("register_game_event");
            restRequest.AddHeader("Content-Type", "application/json");

            var bodyJson = JsonConvert.SerializeObject(gSApiRegisterGameEventPayload, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

            restRequest.AddParameter(null, bodyJson, ParameterType.RequestBody);

            var restResponse = gameSenseRestClient.Post(restRequest);

            if (!restResponse.IsSuccessful)
            {
                throw new Exception("Failed to register game event in SteelSeries GameSense REST API", new Exception(restResponse.Content));
            }
        }

        public void SendGameEvent(GSApiSendGameEventPayload gsApiSendGameEventPayload)
        {
            var restRequest = new RestRequest("game_event");
            restRequest.AddHeader("Content-Type", "application/json");

            var bodyJson = JsonConvert.SerializeObject(gsApiSendGameEventPayload, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

            restRequest.AddParameter(null, bodyJson, ParameterType.RequestBody);

            var restResponse = gameSenseRestClient.Post(restRequest);

            if (!restResponse.IsSuccessful)
            {
                throw new Exception("Failed to send game event in SteelSeries GameSense REST API", new Exception(restResponse.Content));
            }
        }

        public void RemoveGameEvent(GSApiRemoveGameEventPayload gSApiRemoveGameEventPayload)
        {
            var restRequest = new RestRequest("remove_game_event");
            restRequest.AddHeader("Content-Type", "application/json");

            var bodyJson = JsonConvert.SerializeObject(gSApiRemoveGameEventPayload, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

            restRequest.AddParameter(null, bodyJson, ParameterType.RequestBody);

            var restResponse = gameSenseRestClient.Post(restRequest);

            if (!restResponse.IsSuccessful)
            {
                throw new Exception("Failed to remove game event in SteelSeries GameSense REST API", new Exception(restResponse.Content));
            }
        }

        public void RemoveGame(GSApiRemoveGamePayload gSApiRemoveGamePayload)
        {
            var restRequest = new RestRequest("remove_game");
            restRequest.AddHeader("Content-Type", "application/json");

            var bodyJson = JsonConvert.SerializeObject(gSApiRemoveGamePayload, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

            restRequest.AddParameter(null, bodyJson, ParameterType.RequestBody);

            var restResponse = gameSenseRestClient.Post(restRequest);

            if (!restResponse.IsSuccessful)
            {
                throw new Exception("Failed to remove game in SteelSeries GameSense REST API", new Exception(restResponse.Content));
            }
        }
    }
}
