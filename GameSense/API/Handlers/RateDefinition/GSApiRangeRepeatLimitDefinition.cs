using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSense.API.Handlers.RateDefinition
{
    public class GSApiRangeRepeatLimitDefinition
    {
        [JsonProperty("low")]
        public int Low { get; set; }

        [JsonProperty("high")]
        public int High { get; set; }

        // TODO - Add validation to ensure this is of type range-repeat-limit-definition or just a number.
        [JsonProperty("repeat_limit")]
        public object RepeatLimit { get; set; }
    }
}
