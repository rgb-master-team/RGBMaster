using AppExecutionManager.State;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace RGBMasterUWPApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ControlPanelPage : Page
    {
        public ControlPanelPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            TeachingTip tipToToggle;

            if (AppState.Instance.AreAllLightsOn)
            {
                // Turn off all lights logic goes here...
                button.Content = "Turn on all lights";
                tipToToggle = ToggleTip_LightsOff;
            }
            else
            {
                // Turn on all lights logic goes here...
                button.Content = "Turn off all lights";
                tipToToggle = ToggleTip_LightsOn;
            }

            AppState.Instance.AreAllLightsOn = !AppState.Instance.AreAllLightsOn;

            tipToToggle.IsOpen = true;
            await Task.Delay(3000);
            tipToToggle.IsOpen = false;
        }
    }
}
