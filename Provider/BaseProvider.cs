using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

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
                    // TODO - Move to a CancellationToken mechanism and enforce receiving CancellationTokens
                    // in all InternalRegister methods.
                    var registerTimeoutSpan = TimeSpan.FromSeconds(5);
                    await InternalRegister().TimeoutAfter(registerTimeoutSpan, $"Failed to register to provider {ProviderMetadata.ProviderName} with guid {ProviderMetadata.ProviderGuid}").ConfigureAwait(false);
                    IsRegistered = true;
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
                    // TODO - Move to a CancellationToken mechanism and enforce receiving CancellationTokens
                    // in all InternalUnregister methods.
                    var unregisterTimeoutSpan = TimeSpan.FromSeconds(5);
                    await InternalUnregister().TimeoutAfter(unregisterTimeoutSpan, $"Failed to unregister from provider {ProviderMetadata.ProviderName} with guid {ProviderMetadata.ProviderGuid}").ConfigureAwait(false);
                    IsRegistered = false;
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
            ProviderMetadata = providerMetadata;
        }

        protected abstract Task InternalRegister();
        protected abstract Task InternalUnregister();
    }
}
