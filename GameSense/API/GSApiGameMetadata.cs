using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSense.API
{
    public class GSApiGameMetadata
    {
        [JsonProperty("game")]
        public string Game { get; set; }

        [JsonProperty("game_display_name")]
        public string GameDisplayName { get; set; }

        [JsonProperty("developer")]
        public string Developer { get; set; }
    }
}
