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

namespace chroma_yeelight
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int count1 = 0;
        private int count2 = 0;
        private int count3 = 0;
        private int count4 = 0;

        private WasapiLoopbackCapture captureInstance = null;
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

            this.captureInstance = SoundHelper.GetCaptureInstance();

            captureInstance.DataAvailable += (ss, ee) => this.OnNewSoundReceived(ss, ee, selectedDevices);
            captureInstance.RecordingStopped += (ss, ee) => captureInstance.Dispose();

            try
            {
                captureInstance.StartRecording();
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
            return new List<Provider>() { new YeelightProvider(), new RazerChromaProvider(), /*new AuraProvider(),*/ new LogitechProvider() };
        }

        private async Task OnNewSoundReceived(object sender, NAudio.Wave.WaveInEventArgs e, IEnumerable<Device> currDevices)
        {
            float max = 0;
            var buffer = new WaveBuffer(e.Buffer);
            // interpret as 32 bit floating point audio
            for (int index = 0; index < e.BytesRecorded / 4; index++)
            {
                float sample = buffer.FloatBuffer[index];

                // absolute value 
                //if (sample < 0) sample = -sample;
                if (sample < 0) sample = -sample;
                // is this the max value?
                if (sample > max) max = sample;
            }

            Color color = Color.Black;
            // ColorHelper.ComputeRGBColor(ColoreColor.Purple.R, ColoreColor.Purple.G, ColoreColor.Purple.B)

            if (max > 0.01 && max <= 0.1)
            {
                max = 1;
                count1++;
                color = Color.Red;
            }

            if (max > 0.1 && max <= 0.2)
            {
                max = 1;
                count1++;
                color = Color.Orange;
            }

            else if (max > 0.2 && max <= 0.35)
            {
                max = 30;
                count2++;
                color = Color.Yellow;
            }

            else if (max > 0.35 && max <= 0.5)
            {
                max = 30;
                count2++;
                color = Color.Cyan;
            }

            else if (max > 0.5 && max <= 0.65)
            {
                max = 60;
                count3++;
                color = Color.Blue;
            }

            else if (max > 0.65)
            {
                max = 100;
                count4++;
                color = Color.Violet;
            }

            byte brightnessPercentage = (byte)(max * 100);

            var tasks = new List<Task>();

            foreach (var device in currDevices)
            {
                //tasks.Add(device.SetBrightnessPercentage(brightnessPercentage));
                tasks.Add(device.SetColor(color));
            }

            await Task.WhenAll(tasks.ToArray());
        }

        private async void StopSyncingBtn_Click(object sender, RoutedEventArgs e)
        {
            captureInstance.StopRecording();
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
