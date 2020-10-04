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
                    int registerProviderTimeout = 5000;
                    var task = InternalRegister();
                    if (await Task.WhenAny(task, Task.Delay(registerProviderTimeout)) == task)
                    {
                        IsRegistered = true;
                    }
                    else
                    {
                        throw new Exception($"Failed to register to provider {ProviderMetadata.ProviderName} with guid {ProviderMetadata.ProviderGuid}");
                    }

                }

                return IsRegistered;
            }
            catch (Exception ex)
            {
                // TODO - Log
                return false;
            }
        }

        public async Task<bool> Unregister()
        {
            try
            {
                if (IsRegistered)
                {
                    int unregisterTimeout = 1000;
                    var task = InternalUnregister();
                    if (await Task.WhenAny(task, Task.Delay(unregisterTimeout)) == task)
                    {
                        IsRegistered = false;
                    }
                    else
                    {
                        throw new Exception($"Failed to unregister from provider {ProviderMetadata.ProviderName} with guid {ProviderMetadata.ProviderGuid}");
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                // TODO - Log
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
