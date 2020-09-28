using ColorThiefDotNet;
using Common;
using EffectsExecution.Win32Api;
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
using Color = System.Drawing.Color;
using Timer = System.Timers.Timer;

namespace EffectsExecution
{
    public class DominantDisplayColorEffectExecutor : EffectExecutor
    {
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(ref Point lpPoint);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr GetDesktopWindow();
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr GetWindowDC(IntPtr window);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        private static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);
        [DllImport("gdi32.dll", SetLastError = true)]
        private static extern uint GetPixel(IntPtr dc, int x, int y);
        [DllImport("user32.dll")]
        private static extern bool EnumDisplaySettings(string deviceName, int modeNum, ref DEVMODE devMode);

        private const int ENUM_CURRENT_SETTINGS = -1;

        //private Timer calculationTimer;
        private Bitmap screenPixel = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
        private ColorThief colorThief = new ColorThief();

        private CancellationTokenSource backgroundWorkCancellationTokenSource;

        public DominantDisplayColorEffectExecutor() : base(new DominantDisplayColorEffectMetadata())
        {
        }

        public override Task StartInternal()
        {
            var enumeratedDevices = Devices.ToList();

            /*calculationTimer = new Timer(100);

            calculationTimer.Elapsed += (sender, e) => OnTimerFired(enumeratedDevices);
            calculationTimer.Start();*/

            backgroundWorkCancellationTokenSource = new CancellationTokenSource();
            Task.Run(() => DoWork(enumeratedDevices), backgroundWorkCancellationTokenSource.Token);

            return Task.CompletedTask;
        }

        private async void DoWork(List<Device> devices)
        {
            while (true)
            {
                var c = GetDominantColorFromThief();

                List<Task> setColorTasks = new List<Task>();

                foreach (var device in devices)
                {
                    setColorTasks.Add(Task.Run(() => device.SetColor(c)));
                }

                await Task.WhenAll(setColorTasks);
            }
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

        private Size GetHeightAndWidth()
        {
            DEVMODE devMode = default;
            devMode.dmSize = (short)Marshal.SizeOf(devMode);
            EnumDisplaySettings(null, ENUM_CURRENT_SETTINGS, ref devMode);

            var height = devMode.dmPelsHeight;
            var width = devMode.dmPelsWidth;

            return new Size(width, height);
        }

        public Color GetDominantColorFromThief()
        {
            var dimensions = GetHeightAndWidth();

            var height = dimensions.Height;
            var width = dimensions.Width;

            var captureBmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            using var captureGraphic = Graphics.FromImage(captureBmp);
            captureGraphic.CopyFromScreen(0, 0, 0, 0, captureBmp.Size);
            var thiefColor = colorThief.GetColor(captureBmp, 1000, true);
            return Color.FromArgb(thiefColor.Color.R, thiefColor.Color.G, thiefColor.Color.B);
        }

        public override Task StopInternal()
        {
            //calculationTimer.Stop();
            //calculationTimer.Dispose();

            backgroundWorkCancellationTokenSource.Cancel();
            return Task.CompletedTask;
        }
    }
}
