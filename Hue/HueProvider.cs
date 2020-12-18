using Provider;
using Q42.HueApi;
using Q42.HueApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hue
{
    public class HueProvider : BaseProvider
    {
        private string hueApiAppKey;

        public HueProvider() : base(new HueProviderMetadata())
        {
        }

        protected async override Task<List<Device>> InternalDiscover(CancellationToken cancellationToken = default)
        {
            var lights = new List<Device>();

            IBridgeLocator locator = new LocalNetworkScanBridgeLocator(); //Or: LocalNetworkScanBridgeLocator, MdnsBridgeLocator, MUdpBasedBridgeLocator
            var bridges = await locator.LocateBridgesAsync(cancellationToken);

            // TODO - SUPPORT MULTIPLE BRIDGES
            if (bridges != null)
            {
                foreach (var bridge in bridges)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    ILocalHueClient client = new LocalHueClient(bridges.First().IpAddress);
                    //Make sure the user has pressed the button on the bridge before calling RegisterAsync
                    //It will throw an LinkButtonNotPressedException if the user did not press the button

                    try
                    {
                        hueApiAppKey = await client.RegisterAsync("RGBMaster", "RGBMasterClient");
                        cancellationToken.ThrowIfCancellationRequested();
                    }
                    catch (LinkButtonNotPressedException exception)
                    {
                        // TODO - Log the fact that the link button was not pressed,
                        // figure out how to propagate this as input to the user.
                        // Should be made generically for all providers.....
                    }

                    client.Initialize(hueApiAppKey);
                    var discoveredLights = await client.GetLightsAsync();

                    foreach (var discoveredLight in discoveredLights)
                    {
                        lights.Add(new HueLightDevice(ProviderMetadata.ProviderGuid, client, discoveredLight));
                    }
                }
            }

            return lights;
        }

        protected override Task InternalRegister(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        protected override Task InternalUnregister(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
