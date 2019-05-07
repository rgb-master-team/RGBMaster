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
using ColoreColor = Colore.Data.Color;
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

namespace chroma_yeelight
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
            Dictionary<Device, Socket> deviceToSocket = new Dictionary<Device, Socket>();

            var currDevices = await DeviceLocator.Discover();

            if (!currDevices.Any())
            {
                throw new NoDevicesFoundException();
            }

            IPAddress ipAddress = IPAddress.Parse("10.0.0.9");

            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            currDevices.ForEach(dev =>
            {
                dev.Connect();

                var supportedOps = dev.SupportedOperations;

                if (!supportedOps.Contains(YeelightAPI.Models.METHODS.SetMusicMode))
                {
                    throw new YeelightDeviceNotSupportedException();
                }
            });

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

                currDevices.ForEach(async dev =>
                {
                    try
                    {
                        var setMusicResult = await dev.StartMusicMode("10.0.0.9", 11000);
                        if (!setMusicResult)
                        {
                            throw new DeviceCommandFailedException();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An unexpected error has occured. Please check your connection and try to reset the bulbs if the issue persists.", ex);
                    }
                });

                currDevices.ForEach(dev => deviceToSocket[dev] = listener.Accept());

                var chroma = await ColoreProvider.CreateNativeAsync();

                var random = new Random();

                var timer = new Timer() { Interval = 200 };
                timer.Elapsed += async (bla1, bla2) =>
                {
                    var currColor = new ColoreColor(random.Next(256), random.Next(256), random.Next(256));

                    await chroma.SetAllAsync(currColor);

                    currDevices.ForEach(dev =>
                    {
                        //await dev.SetRGBColor(currColor.R, currColor.G, currColor.B).ConfigureAwait(true);

                        //currColor.R * 65536 + currColor.G * 256

                        int value = ColorHelper.ComputeRGBColor(currColor.R, currColor.G, currColor.B);

                        // var serverParams = new List<object>() { value, "smooth", 500 };
                        // var serverParams = new List<object>() { value, "sudden", null };
                        var serverParams = new List<object>() { value, "smooth", 100 };

                        Command command = new Command()
                        {
                            Id = 1,
                            Method = "set_rgb",
                            Params = serverParams
                        };

                        string data = JsonConvert.SerializeObject(command, DeviceSerializerSettings);
                        byte[] sentData = Encoding.ASCII.GetBytes(data + "\r\n"); // \r\n is the end of the message, it needs to be sent for the message to be read by the device

                        var sentBytes = deviceToSocket[dev].Send(sentData);
                    });
                };

                timer.Start();
            }

            /*while (true)
            {
                foreach (var device in currDevices)
                {
                    

                    // var currColor = chroma.Keyboard[Colore.Effects.Keyboard.Key.F];
                    var currColor = new ColoreColor(random.Next(256), random.Next(256), random.Next(256));

                    //await chroma.SetAllAsync(currColor);

                    await device.SetRGBColor(currColor.R, currColor.G, currColor.B).ConfigureAwait(true);
                }
            }*/

            //var appInfo = new AppInfo("My app", "I liek 69 dicks", "John Doe", "me@example.com", Category.Application);
            //var chroma = await ColoreProvider.CreateRestAsync(appInfo, new Uri("http://localhost:54235"));
            //var chroma = await ColoreProvider.CreateNativeAsync();
        }
    }
}
