using System;
using Infrastructure;

namespace chroma_yeelight.Exceptions
{
    public class DuplicateProvidersException : Exception
    {
        public DuplicateProvidersException(Provider provider, Exception ex) : base($"Duplicate instances of provider {provider.ProviderName} were added! Please make sure each provider is registered once", ex)
        {
        }
    }
}
