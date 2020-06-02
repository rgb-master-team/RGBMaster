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
using MagicHome;
using System.Collections.ObjectModel;

namespace chroma_yeelight
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public ObservableCollection<Provider> providers;
        public ObservableCollection<Provider> selectedProviders = new ObservableCollection<Provider>();
        public ObservableCollection<Device> discoveredDevices = new ObservableCollection<Device>();
        public ObservableCollection<Device> selectedDevices = new ObservableCollection<Device>();
        private IEffect selectedEffect;

        public MainWindow()
        {
            InitializeComponent();

            if (Environment.GetCommandLineArgs().Any(x => x == "--start-immediate"))
            {
                Task.Run(() => StartSyncing());
            }

            this.providers = GetProviders();

            providersItemsControl.ItemsSource = providers;
            devicesControl.ItemsSource = discoveredDevices;
        }

        private async void DiscoverDevices_Clicked(object sender, RoutedEventArgs e)
        {
            discoveredDevices.Clear();

            foreach (var provider in selectedProviders)
            {
                try
                {
                    await provider.InitializeProvider();

                    var discoveredDevices = await provider.Discover();

                    foreach (var discoveredDevice in discoveredDevices)
                    {
                        this.discoveredDevices.Add(discoveredDevice);
                    }
                }
                catch(Exception)
                {

                }
                finally
                {

                }
            }
        }

        private void CheckProviderForDiscovery_Clicked(object sender, RoutedEventArgs e)
        {
            var checkbox = (CheckBox)sender;
            var provider = checkbox.Tag as Provider;

            selectedProviders.Add(provider);
        }

        private void UncheckProviderForDiscovery_Clicked(object sender, RoutedEventArgs e)
        {
            var checkbox = (CheckBox)sender;
            var provider = checkbox.Tag as Provider;

            selectedProviders.Remove(provider);
        }

        private void CheckDevice_Clicked(object sender, RoutedEventArgs e)
        {
            var checkbox = (CheckBox)sender;
            var device = checkbox.Tag as Device;

            selectedDevices.Add(device);
        }

        private void UncheckDevice_Clicked(object sender, RoutedEventArgs e)
        {
            var checkbox = (CheckBox)sender;
            var device = checkbox.Tag as Device;

            selectedDevices.Remove(device);
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
            await ConnectToSelectedDevices();

            try
            {
                await selectedEffect.Start(discoveredDevices);
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

        private ObservableCollection<Provider> GetProviders()
        {
            return new ObservableCollection<Provider>() { new YeelightProvider(), new LogitechProvider(), new RazerChromaProvider(), new MagicHomeProvider() /*new AuraProvider(), new CorsairProvider()*/ };
		}

        private async void StopSyncingBtn_Click(object sender, RoutedEventArgs e)
        {
            await selectedEffect.Stop();

            foreach (var device in discoveredDevices)
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
