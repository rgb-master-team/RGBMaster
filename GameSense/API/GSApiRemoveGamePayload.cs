using Newtonsoft.Json;

namespace GameSense.API
{
    public class GSApiRemoveGamePayload
    {
        // TODO - Move NullValueHandling = NullValueHandling.Ignore) to the attribute from the GSAPI implementation.
        [JsonProperty("game")]
        public string Game { get; set; }
    }
}