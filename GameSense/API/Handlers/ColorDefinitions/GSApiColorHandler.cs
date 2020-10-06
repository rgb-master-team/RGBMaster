using GameSense.API.Handlers;
using GameSense.API.Handlers.Rate;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSense.API.Handlers.ColorDefinitions
{
    public class GSApiColorHandler : GSApiHandler
    {
        [JsonProperty("device-type")]
        public string DeviceType { get; set; }

        [JsonProperty("zone")]
        public string Zone { get; set; }

        [JsonProperty("custom-zone-keys")]
        public string CustomZoneKeys { get; set; }

        [JsonProperty("mode")]
        public string Mode { get; set; }

        [JsonProperty("color")]
        public GSApiColorHandlerColorDefinition Color { get; set; }

        [JsonProperty("rate")]
        public GSApiRateDefinition Rate { get; set; }

        [JsonProperty("context-frame-key")]
        public string ContextFrameKey { get; set; }
    }
}
