using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Infrastructure;
using Device = Infrastructure.Device;
using chroma_yeelight.Effects;
using chroma_yeelight.Effects.Rainbow;
using chroma_yeelight.Exceptions;
using MahApps.Metro.Controls;
using Corsair.Provider;
using Infrastructure.Effects;

namespace chroma_yeelight
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private EffectController currentEffectController;
        private IEnumerable<Provider> providers;
        private IEnumerable<Device> selectedDevices;

        /// <summary>
        /// Serializer settings
        /// </summary>
        public static readonly JsonSerializerSettings DeviceSerializerSettings = new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };


        public MainWindow()
        {
            InitializeComponent();
        }

        private async void StartSyncBtn_Click(object sender, RoutedEventArgs e)
        {
            this.providers = GetProviders();
            RegisterProviders();
            Dictionary<Provider, IEnumerable<Device>> providerToDevices = await DiscoverProvidersDevices();

            // GetSelectedDevices from the user or something.. and then:
            selectedDevices = GetSelectedDevices(providerToDevices);
            await ConnectToSelectedDevices();

            currentEffectController = new RainbowEffectController(selectedDevices)
            {
				Direction = EffectDirection.Vertical
            };
			currentEffectController.Start();
        }

        private async Task ConnectToSelectedDevices()
        {
            foreach (var device in selectedDevices)
            {
                await device.Connect();
            }
        }

        private IEnumerable<Device> GetSelectedDevices(Dictionary<Provider, IEnumerable<Device>> providerToDevices)
        {
            return providerToDevices.Values.SelectMany(devices => devices).ToList();
        }

        private async Task<Dictionary<Provider, IEnumerable<Device>>> DiscoverProvidersDevices()
        {
            Dictionary<Provider, IEnumerable<Device>> providerToDevices = new Dictionary<Provider, IEnumerable<Device>>();
            foreach (var provider in providers)
            {
                IEnumerable<Device> devices;
                try
                {
                    devices = await provider.Discover();
                }
                catch (Exception ex)
                {
                    throw new DiscoveryFailedException(provider, ex);
                }

                if (providerToDevices.ContainsKey(provider))
                {
                    throw new DuplicateProvidersException(provider, null);
                }

                providerToDevices.Add(provider, devices);
            }

            return providerToDevices;
        }

        private async void RegisterProviders()
        {
            foreach (var provider in providers)
            {
                try
                {
                    await provider.Register();
                }
                catch (Exception ex)
                {
                    throw new ProviderRegistrationFailedException(provider, ex);
                }
            }
        }

        private IEnumerable<Provider> GetProviders()
        {
            return new List<Provider>() { new CorsairProvider()/*new YeelightProvider(), new RazerChromaProvider(), /*new AuraProvider(), new LogitechProvider(), new CorsairProvider()*/  };
		}

        private async void StopSyncingBtn_Click(object sender, RoutedEventArgs e)
        {
	        currentEffectController.Stop();
            foreach (var device in selectedDevices)
            {
                await device.Disconnect();
            }

            foreach (var provider in this.providers)
            {
                await provider.Unregister();
            }
        }
    }
}
