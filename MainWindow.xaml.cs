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

        private async void SyncButton_Click(object sender, RoutedEventArgs e)
        {
            var providers = GetProviders();

            Dictionary<Provider, IEnumerable<Device>> providerToDevices = new Dictionary<Provider, IEnumerable<Infrastructure.Device>>();
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

            // GetSelectedDevices from the user or something.. and then:
            var selectedDevices = providerToDevices.Values.SelectMany(devices => devices).ToList();

            foreach (var device in selectedDevices)
            {
                await device.Connect();
            }

            var captureInstance = SoundHelper.GetCaptureInstance();

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

            /*Dictionary<Device, Socket> deviceToSocket = new Dictionary<Device, Socket>();

            

            using (Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp))
            {
                try
                {
                    listener.Bind(localEndPoint);
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to bind the socket to the specified ip and port.", ex);
                }

                listener.Listen(int.MaxValue);

                foreach (var device in currDevices)
                {
                    try
                    {
                        var setMusicResult = await device.StartMusicMode(ipAddress.ToString(), 11000);
                        if (!setMusicResult)
                        {
                            throw new DeviceCommandFailedException();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An unexpected error has occured. Please check your connection and try to reset the bulbs if the issue persists.", ex);
                    }
                }

                currDevices.ForEach(dev => deviceToSocket[dev] = listener.Accept());

                var chroma = await ColoreProvider.CreateNativeAsync();
                
                var captureInstance = SoundHelper.GetCaptureInstance();

                captureInstance.DataAvailable += (ss, ee) => this.OnNewSoundReceived(ss, ee, currDevices, deviceToSocket, chroma);
                captureInstance.RecordingStopped += (ss, ee) => captureInstance.Dispose();

                try
                {
                    captureInstance.StartRecording();
                }
                catch (Exception ex)
                {
                    throw new AudioCaptureAccessDeniedException("Hello! Yes! Yes, Eliran Sabag. Make sure there are no background softwares running in your pc that are capturing background activity! (including MOBO software, Realtek HD, Asus Sonic etc.)", ex);
                }*/
        }

        private IEnumerable<Provider> GetProviders()
        {
            return new List<Provider>() { new YeelightProvider(), new RazerChromaProvider(), /*new AuraProvider()*/ new LogitechProvider() };
        }

        private async void OnNewSoundReceived(object sender, NAudio.Wave.WaveInEventArgs e, List<Device> currDevices)
        {
            float max = 0;
            float sample = 0;

            var buffer = new WaveBuffer(e.Buffer);
            // interpret as 32 bit floating point audio
            for (int index = 0; index < e.BytesRecorded / 4; index++)
            {
                sample = buffer.FloatBuffer[index];

                // absolute value 
                //if (sample < 0) sample = -sample;
                if (sample < 0) sample = -sample;
                // is this the max value?
                if (sample > max) max = sample;
            }

            Color color = Color.Black;
            // ColorHelper.ComputeRGBColor(ColoreColor.Purple.R, ColoreColor.Purple.G, ColoreColor.Purple.B)

            if (max > 0.1 && max <= 0.3)
            {
                max = 1;
                count1++;
                color = Color.Red;
            }

            else if (max > 0.3 && max <= 0.50)
            {
                max = 30;
                count2++;
                color = Color.Orange;
            }

            else if (max > 0.50 && max <= 0.65)
            {
                max = 60;
                count3++;
                color = Color.Yellow;
            }

            else if (max > 0.65)
            {
                max = 100;
                count4++;
                color = Color.FromArgb(0, 255, 255);
            }

            byte brightnessPercentage = (byte)(max * 100);

            var tasks = new List<Task>();

            foreach (var device in currDevices)
            {
                //tasks.Add(device.SetBrightnessPercentage(brightnessPercentage));
                tasks.Add(device.SetColor(color));
            }

            Task.WaitAll(tasks.ToArray());
        }
    }
}
