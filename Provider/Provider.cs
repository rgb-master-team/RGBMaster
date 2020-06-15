using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider
{
    public abstract class Provider<ProviderMd, DeviceMd> where ProviderMd : ProviderMetadata, new() where DeviceMd : DeviceMetadata
    {
        public readonly ProviderMd ProviderMetadata;
        public bool IsRegistered { get; private set; }
        public abstract Task Unregister();
        public abstract Task<IEnumerable<Device<DeviceMd>>> Discover();

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

        public Provider(ProviderMd providerMetadata)
        {
            this.ProviderMetadata = providerMetadata;
        }

        protected abstract Task Register();
    }
}
