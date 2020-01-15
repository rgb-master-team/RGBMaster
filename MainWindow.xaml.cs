using Colore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Colore.Effects.Keyboard;
using YeelightAPI;
using Color = System.Drawing.Color;
using Colore.Data;
using System.Timers;
using System.Net;
using System.Net.Sockets;
using System.Dynamic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using YeelightAPI.Models;
using System.Drawing;
using NAudio.Wave;
using Infrastructure;
using Yeelight;
using RazerChroma;
using Device = Infrastructure.Device;
using Aura;
using Logitech;
using MahApps.Metro.Controls;
using Corsair.Provider;

namespace chroma_yeelight
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private IEnumerable<Provider> providers;
        private IEnumerable<Device> selectedDevices;
        private IEffect selectedEffect;

        public MainWindow()
        {
            InitializeComponent();

            if (Environment.GetCommandLineArgs().Any(x => x == "--start-immediate"))
            {
                Task.Run(() => StartSyncing());
            }
        }

        private async void StartSyncBtn_Click(object sender, RoutedEventArgs e)
        {
            switch (EffectFlipView.SelectedIndex)
            {
                case 0:
                    selectedEffect = new MusicEffect();
                        break;
                case 1:
                    selectedEffect = new DominantDisplayColorEffect();
                    break;
                default:
                    break;
            }

            await StartSyncing();
        }

        private async Task StartSyncing()
        {
            this.providers = GetProviders();
            RegisterProviders();
            Dictionary<Provider, IEnumerable<Device>> providerToDevices = await DiscoverProvidersDevices();

            // GetSelectedDevices from the user or something.. and then:
            selectedDevices = GetSelectedDevices(providerToDevices);
            await ConnectToSelectedDevices();

            //selectedEffect = new MusicEffect();
            //selectedEffect = new MusicEffect();

            try
            {
                await selectedEffect.Start(selectedDevices);
            }
            catch (Exception ex)
            {
                throw new AudioCaptureAccessDeniedException(ex);
            }
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
            return new List<Provider>() { /*new CorsairProvider(), */new YeelightProvider(), /*new RazerChromaProvider(), new AuraProvider(), new LogitechProvider(), new CorsairProvider()*/  };
		}

        private async void StopSyncingBtn_Click(object sender, RoutedEventArgs e)
        {
            await selectedEffect.Stop();

            foreach (var device in selectedDevices)
            {
                await device.Disconnect();
            }

            foreach (var provider in this.providers)
            {
                await provider.Unregister();
            }
        }

        private void EffectFlipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var flipView = (FlipView)sender;

            switch (flipView.SelectedIndex)
            {
                case 0:
                    flipView.BannerText = "Music Mode that should be fucking binded by now";
                    break;
                case 1:
                    flipView.BannerText = "Cursor mode which should be fucking renamed (or let its settings change) and also be fucking binded by now!";
                    break;
                default:
                    break;
            }
        }
    }
}
