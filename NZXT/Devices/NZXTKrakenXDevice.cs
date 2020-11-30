// WARNING - DON'T USE THIS PROVIDER WITHOUT EXTREME TESTS AND RATE LIMITERS - THIS MAY FRY THE COMPUTER,
// AS NO OFFICIAL SDK HAS SURFACED.

//using Common;
//using NZXTSharp;
//using NZXTSharp.KrakenX;
//using Provider;
//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Text;
//using System.Threading.Tasks;
//using Color = System.Drawing.Color;

//namespace NZXT.Devices
//{
//    public class NZXTKrakenXDevice : Device
//    {
//        private readonly KrakenX internalDevice;
//        public NZXTKrakenXDevice(Guid discoveredProviderGuid, KrakenX internalDevice) : base(new NZXTKrakenXDeviceMetadata(discoveredProviderGuid, "KrakenX"))
//        {
//            this.internalDevice = internalDevice;
//        }

//        protected override Task ConnectInternal()
//        {
//            return Task.CompletedTask;
//        }

//        protected override Task DisconnectInternal()
//        {
//            return Task.CompletedTask;
//        }

//        protected override Task<byte> GetBrightnessPercentageInternal()
//        {
//            throw new NotImplementedException();
//        }

//        protected override Task<Color> GetColorInternal()
//        {
//            throw new NotImplementedException();
//        }

//        protected override Task SetBrightnessPercentageInternal(byte brightness)
//        {
//            throw new NotImplementedException();
//        }

//        protected override Task SetColorInternal(Color color)
//        {
//            Fixed nzxtColor = new Fixed(new NZXTSharp.Color(color.R, color.G, color.B));
//            internalDevice.ApplyEffect(nzxtColor);

//            return Task.CompletedTask;
//        }

//        protected override Task SetGradientInternal(GradientPoint gradientPoint, int relativeSmoothness)
//        {
//            throw new NotImplementedException();
//        }

//        protected override Task TurnOffInternal()
//        {
//            throw new NotImplementedException();
//        }

//        protected override Task TurnOnInternal()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
