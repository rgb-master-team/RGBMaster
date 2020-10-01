using Newtonsoft.Json;

namespace GameSense.API
{
    public class GSApiSendGameEventDataPayload
    {
        [JsonProperty("value")]
        public int? Value { get; set; }

        [JsonProperty("frame")]
        public object Frame { get; set; }
    }
}