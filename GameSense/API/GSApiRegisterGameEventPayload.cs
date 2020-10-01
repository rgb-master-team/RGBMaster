using Newtonsoft.Json;

namespace GameSense.API
{
    public class GSApiRegisterGameEventPayload
    {
        [JsonProperty("game")]
        public string Game { get; set; }

        [JsonProperty("event")]
        public string Event { get; set; }

        [JsonProperty("min_value")]
        public int? MinValue { get; set; }

        [JsonProperty("max_value")]
        public int? MaxValue { get; set; }

        [JsonProperty("icon_id")]
        public int? IconId { get; set; }

        [JsonProperty("value_optional")]
        public bool? ValueOptional { get; set; }
    }
}