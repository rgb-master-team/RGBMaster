using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace Infrastructure
{
    public static class SoundHelper
    {
        public static WasapiLoopbackCapture GetCaptureInstance()
        {
            return new WasapiLoopbackCapture();
        }

        public static Tuple<int, int, int> TranslateSoundToColor()
        {
            return null;
        }
    }
}
