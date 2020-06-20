using AppExecutionManager.EventManagement;
using AppExecutionManager.State;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class DevicesPage : Page
    {
        public ObservableCollection<RegisteredProvider> RegisteredProviders 
        { 
            get
            {
                return AppState.Instance.RegisteredProviders;
            }
        }

        public DevicesPage()
        {
            this.InitializeComponent();
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            var checkbox = sender as CheckBox;
            var discoveredDevice = (DiscoveredDevice)checkbox.Tag;

            discoveredDevice.IsChecked = !discoveredDevice.IsChecked;

            EventManager.Instance.UpdateSelectedDevices(AppState.Instance.RegisteredProviders.Select(prov => prov.Devices).SelectMany(devices => devices).ToImmutableList().ToList());
        }

        private void ManualConnectionButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Refresh_Button_Clicked(object sender, RoutedEventArgs e)
        {
            EventManager.Instance.InitializeProviders();
        }
    }
}
