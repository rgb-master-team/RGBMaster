using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSense
{
    public class GameSenseColorHandler : GameSenseHandler
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
        public GameSenseColorHandlerColorDefinition Color { get; set; }

        [JsonProperty("rate")]
        public string Rate { get; set; }
    }
}
