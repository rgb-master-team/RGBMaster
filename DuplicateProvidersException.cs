using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace chroma_yeelight
{
    public class DuplicateProvidersException : Exception
    {
        public DuplicateProvidersException(Provider provider, Exception ex) : base($"Duplicate instances of provider {provider.ProviderName} were added! Please make sure each provider is registered once", ex)
        {
        }
    }
}
