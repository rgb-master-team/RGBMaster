using System;
using NAudio.Wave;

namespace chroma_yeelight.Effects.Sound
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
