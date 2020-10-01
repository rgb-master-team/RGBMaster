using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSense.API
{
    public class GSApiRemoveGameEventPayload
    {
        [JsonProperty("game")]
        public string Game { get; set; }

        [JsonProperty("event")]
        public string Event { get; set; }
    }
}
