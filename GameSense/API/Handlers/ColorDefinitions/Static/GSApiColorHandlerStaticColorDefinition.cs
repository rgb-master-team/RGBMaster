using GameSense.API.Handlers.ColorDefinitions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSense.API.Handlers.ColorDefinitions.Static
{
    public class GSApiColorHandlerStaticColorDefinition : GSApiColorHandlerColorDefinition
    {
        [JsonProperty("red")]
        public byte Red { get; set; }
        [JsonProperty("green")]
        public byte Green { get; set; }
        [JsonProperty("blue")]
        public byte Blue { get; set; }
    }
}
