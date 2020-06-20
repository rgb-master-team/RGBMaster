using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider
{
    public abstract class BaseProvider
    {
        public readonly ProviderMetadata ProviderMetadata;
        public bool IsRegistered { get; private set; }
        public abstract Task Unregister();
        public abstract Task<List<Device>> Discover();

        public async Task<bool> InitializeProvider()
        {
            try
            {
                if (!IsRegistered)
                {
                    await Register();

                    IsRegistered = true;
                }

                return IsRegistered;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public BaseProvider(ProviderMetadata providerMetadata)
        {
            this.ProviderMetadata = providerMetadata;
        }

        protected abstract Task Register();
    }
}
