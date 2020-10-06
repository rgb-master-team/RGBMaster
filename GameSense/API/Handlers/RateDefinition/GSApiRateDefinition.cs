using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSense.API.Handlers.Rate
{
    public class GSApiRateDefinition
    {
        // TODO - Add validation to ensure this is of type range-frequency-definition or just a number.
        [JsonProperty("frequency")]
        public object Frequency { get; set; }

        // TODO - Add validation to ensure this is of type range-repeat-limit-definition or just a number.
        [JsonProperty("repeat_limit")]
        public object RepeatLimit { get; set; }
    }
}
