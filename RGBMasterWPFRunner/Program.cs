using System;
using System.Collections.Generic;
using System.Text;

namespace RGBMasterWPFRunner
{
    public class Program
    {
        [System.STAThreadAttribute()]
        public static void Main()
        {
            using (new RGBMasterUWPHost.App())
            {
                RGBMasterWPFRunner.App app = new RGBMasterWPFRunner.App();
                app.InitializeComponent();
                app.Run();
            }
        }
    }
}
