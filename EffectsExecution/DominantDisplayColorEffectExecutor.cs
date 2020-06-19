using Common;
using Provider;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EffectsExecution
{
    public class DominantDisplayColorEffectExecutor : EffectExecutor
    {
        [DllImport("user32.dll")]
        static extern bool GetCursorPos(ref Point lpPoint);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

        private Timer calculationTimer;
        private Bitmap screenPixel = new Bitmap(1, 1, PixelFormat.Format32bppArgb);

        public DominantDisplayColorEffectExecutor() : base(new DominantDisplayColorEffectMetadata())
        {
        }

        public override Task StartInternal()
        {
            var enumeratedDevices = Devices.ToList();

            calculationTimer = new Timer((state) => OnTimerFired(state, enumeratedDevices), null, 0, 200);

            return Task.CompletedTask;
        }

        private async void OnTimerFired(object state, List<Device> devices)
        {
            Point cursor = new Point();
            GetCursorPos(ref cursor);

            var c = GetColorAt(cursor);

            Task[] setColorTasks = new Task[devices.Count];
            for (int i = 0; i < devices.Count; i++)
            {
                setColorTasks[i] = Task.Run(() => devices[i].SetColor(c));
            }

            await Task.WhenAll(setColorTasks);
        }

        public Color GetColorAt(Point location)
        {
            using (Graphics gdest = Graphics.FromImage(screenPixel))
            {
                using (Graphics gsrc = Graphics.FromHwnd(IntPtr.Zero))
                {
                    IntPtr hSrcDC = gsrc.GetHdc();
                    IntPtr hDC = gdest.GetHdc();
                    int retval = BitBlt(hDC, 0, 0, 1, 1, hSrcDC, location.X, location.Y, (int)CopyPixelOperation.SourceCopy);
                    gdest.ReleaseHdc();
                    gsrc.ReleaseHdc();
                }
            }

            return screenPixel.GetPixel(0, 0);
        }

        public override Task StopInternal()
        {
            calculationTimer.Dispose();

            return Task.CompletedTask;
        }
    }
}
