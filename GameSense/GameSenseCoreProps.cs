using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSense
{
    // {"address":"127.0.0.1:56805","encrypted_address":"127.0.0.1:56806"}
    public class GameSenseCoreProps
    {
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("encrypted_address")]
        public string EncryptedAddress { get; set; }
    }
}
