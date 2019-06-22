using System;
using Infrastructure;

namespace chroma_yeelight.Exceptions
{
    public class DiscoveryFailedException : Exception
    {
        public DiscoveryFailedException(Provider provider, Exception ex) : base($"Discovery failed for provider {provider.ProviderName}.", ex)
        {
        }
    }
}
