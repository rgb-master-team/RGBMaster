using GameSense.API.Handlers.ColorDefinitions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSense.API.Handlers.ColorDefinitions.Range
{
    public class GSApiColorHandlerRangeColorDefinition
    {
        [JsonProperty("low")]
        public int Low { get; set; }
        [JsonProperty("high")]
        public int High { get; set; }
        [JsonProperty("color")]
        public GSApiColorHandlerColorDefinition Color { get; set; }
    }
}
