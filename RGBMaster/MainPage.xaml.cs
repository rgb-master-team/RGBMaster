using Infrastructure;
using MagicHome;
using RazerChroma;
using RGBMaster.Pages;
using RGBMaster.State;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
using Yeelight;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RGBMaster
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // List of ValueTuple holding the Navigation Tag and the relative Navigation Page
        private readonly Dictionary<string, Type> pageToType = new Dictionary<string, Type>()
        {
            { "DevicesPage", typeof(DevicesPage) },
            { "EffectsPage", typeof(EffectsPage) }
        };

        private readonly IEnumerable<Provider> SupportedProviders = new List<Provider>()
        {
            new YeelightProvider(), new MagicHomeProvider()
        };

        public MainPage()
        {
            this.InitializeComponent();

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            SeekAndRediscoverDevices();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }


        private async Task SeekAndRediscoverDevices()
        {
            var availableRegisteredProviders = new List<RegisteredProvider>();

            foreach (var provider in SupportedProviders)
            {
                await provider.InitializeProvider();

                if (provider.IsRegistered)
                {
                    var discoveredDevices = await provider.Discover();

                    availableRegisteredProviders.Add(new RegisteredProvider()
                    {
                        Provider = provider,
                        Devices = new ObservableCollection<Device>(discoveredDevices)
                    });
                }
            }

            AppState.Instance.RegisteredProviders.Clear();

            availableRegisteredProviders.ForEach(registeredProvider => AppState.Instance.RegisteredProviders.Add(registeredProvider));
        }

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var selectionTag = (string)args.SelectedItemContainer.Tag;

            var navigationResult = MainAppContentFrame.Navigate(pageToType[selectionTag], null, args.RecommendedNavigationTransitionInfo);
        }

        private void MainAppContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {

        }
    }
}
