using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace chroma_yeelight
{
    public class DiscoveryFailedException : Exception
    {
        public DiscoveryFailedException(Provider provider, Exception ex) : base($"Discovery failed for provider {provider.ProviderName}.", ex)
        {
        }
    }
}
