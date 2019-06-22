using System;
using Infrastructure;

namespace chroma_yeelight.Exceptions
{
    [Serializable]
    internal class ProviderRegistrationFailedException : Exception
    {
        public ProviderRegistrationFailedException(Provider provider) : this(provider, null)
        {
        }

        public ProviderRegistrationFailedException(Provider provider, Exception ex) : base($"Failed to register provider {provider.ProviderName}.", ex)
        {
        }
    }
}