using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSense.API
{
    // {"address":"127.0.0.1:56805","encrypted_address":"127.0.0.1:56806"}
    public class GSApiCoreProps
    {
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("encrypted_address")]
        public string EncryptedAddress { get; set; }
    }
}
