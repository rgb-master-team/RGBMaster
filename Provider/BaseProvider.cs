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

        public async Task InitializeProvider()
        {
            try
            {
                if (IsRegistered)
                {
                    return;
                }

                await Register();

                IsRegistered = true;
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        public BaseProvider(ProviderMetadata providerMetadata)
        {
            this.ProviderMetadata = providerMetadata;
        }

        protected abstract Task Register();
    }
}
