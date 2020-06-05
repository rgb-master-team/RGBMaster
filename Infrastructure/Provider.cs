using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public abstract class Provider
    {
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
