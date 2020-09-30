using GameSense.API.Handlers.ColorDefinitions;
using GameSense.API.Handlers.ColorDefinitions.Static;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSense.API.Handlers.ColorDefinitions.Gradient
{
    public class GSApiColorHandlerGradientColorDefinition : GSApiColorHandlerColorDefinition
    {
        [JsonProperty("zero")]
        public GSApiColorHandlerStaticColorDefinition Zero { get; set; }
        [JsonProperty("hundred")]
        public GSApiColorHandlerStaticColorDefinition Hundred { get; set; }
    }
}
