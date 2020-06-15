using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public abstract class Provider<ProviderMd> where ProviderMd : ProviderMetadata
    {
        public ProviderMd ProviderMetadata { get; set; }
        public bool IsRegistered { get; private set; }
        public abstract string ProviderName { get; }
        public abstract Task Unregister();
        public abstract Task<IEnumerable<Device>> Discover();

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
            catch(Exception ex)
            {

            }
            finally
            {

            }    
        }

        protected abstract Task Register();
    }
}
