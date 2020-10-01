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
        public abstract Task<List<Device>> Discover();

        public async Task<bool> Register()
        {
            try
            {
                if (!IsRegistered)
                {
                    await InternalRegister();

                    IsRegistered = true;
                }

                return IsRegistered;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Unregister()
        {
            try
            {
                if (IsRegistered)
                {
                    await InternalUnregister();

                    IsRegistered = false;
                }

                return true;
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

        protected abstract Task InternalRegister();
        protected abstract Task InternalUnregister();
    }
}
