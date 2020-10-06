using Newtonsoft.Json;

namespace GameSense.API.Handlers.RateDefinition
{
    public class GSApiRangeFrequencyDefinition
    {
        [JsonProperty("low")]
        public int Low { get; set; }

        [JsonProperty("high")]
        public int High { get; set; }

        // TODO - Add validation to ensure this is of type range-frequency-definition or just a number.
        [JsonProperty("frequency")]
        public object Frequency { get; set; }
    }
}
