using Common;
using Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MagicHome
{
    public class MagicHomeDevice : Device
    {
        private const int defaultMagicHomePort = 5577;

        private readonly string LightIp;
        private readonly Socket InternalLightSocket;

        public MagicHomeDevice(string lightIp, MagicHomeDeviceMetadata magicHomeDeviceMetadata) : base(magicHomeDeviceMetadata)
        {
            LightIp = lightIp;
            InternalLightSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        protected override async Task ConnectInternal()
        {
            await InternalLightSocket.ConnectAsync(IPAddress.Parse(LightIp), defaultMagicHomePort);
        }

        protected override Task DisconnectInternal()
        {
            InternalLightSocket.Close();
            InternalLightSocket.Dispose();

            return Task.CompletedTask;
        }

        protected override byte GetBrightnessPercentageInternal()
        {
            throw new NotImplementedException();
        }

        protected override System.Drawing.Color GetColorInternal()
        {
            throw new NotImplementedException();
        }

        protected override void SetBrightnessPercentageInternal(byte brightness)
        {
            throw new NotImplementedException();
        }

        protected override void SetColorInternal(System.Drawing.Color color)
        {
            // TODEAN
        }

        protected override void TurnOffInternal()
        {
            // TODEAN
        }

        protected override void TurnOnInternal()
        {
            // TODEAN
        }
    }
}
