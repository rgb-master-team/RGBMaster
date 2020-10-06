using Newtonsoft.Json;

namespace GameSense.API
{
    public class GSApiRemoveGamePayload
    {
        [JsonProperty("game")]
        public string Game { get; set; }
    }
}