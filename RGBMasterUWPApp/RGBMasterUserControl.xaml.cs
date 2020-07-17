using AppExecutionManager.EventManagement;
using AppExecutionManager.State;
using RGBMasterUWPApp.Pages;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace RGBMasterUWPApp
{
    public sealed partial class RGBMasterUserControl : UserControl
    {
        public string CurrentEffectName
        {
            get
            {
                return AppState.Instance.SelectedEffect?.EffectName;
            }
        }

        // List of ValueTuple holding the Navigation Tag and the relative Navigation Page
        private readonly Dictionary<string, Type> pageToType = new Dictionary<string, Type>()
                {
                    { nameof(DevicesPage), typeof(DevicesPage) },
                    { nameof(EffectsPage), typeof(EffectsPage) },
                    { nameof(SettingsPage), typeof(SettingsPage) }
                };

        private SemaphoreSlim startAndStopSemaphore = new SemaphoreSlim(1, 1);

        private void MainAppContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {

        }

        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            await startAndStopSemaphore.WaitAsync();

            var oldInnerButton = FindName("InnerButton") as FontIcon;
            var oldOuterButton = FindName("OuterButton") as FontIcon;

            AppBarStartStopGrid.Children.Remove(oldInnerButton);
            AppBarStartStopGrid.Children.Remove(oldOuterButton);

            UIElement innerButton;
            UIElement outerButton;

            var syncStatusToolTip = new ToolTip();

            if (AppState.Instance.IsEffectRunning)
            {
                EventManager.Instance.StopSyncing();

                outerButton = new FontIcon() { Glyph = "\uE739", FontFamily = new Windows.UI.Xaml.Media.FontFamily("Segoe MDL2 Assets"), Name = "OuterButton" };
                innerButton = new FontIcon() { Glyph = "\uE73B", FontFamily = new Windows.UI.Xaml.Media.FontFamily("Segoe MDL2 Assets"), Foreground = new SolidColorBrush(Colors.Red), Name = "InnerButton" };

                AppBarButton button = (AppBarButton)sender;

                outerButton = new FontIcon() { Glyph = "\uE768", FontFamily = new Windows.UI.Xaml.Media.FontFamily("Segoe MDL2 Assets"), Name = "OuterButton" };
                innerButton = new FontIcon() { Glyph = "\uF5B0", FontFamily = new Windows.UI.Xaml.Media.FontFamily("Segoe MDL2 Assets"), Foreground = new SolidColorBrush(Colors.Green), Name = "InnerButton" };

                button.Label = "Start";
                DisplayCurrentSyncMode.Visibility = Visibility.Collapsed;

                ToolTipService.SetToolTip(Sync_Button, null);
            }
            else
            {
                EventManager.Instance.StartSyncing();

                AppBarButton button = (AppBarButton)sender;

                outerButton = new FontIcon() { Glyph = "\uE739", FontFamily = new Windows.UI.Xaml.Media.FontFamily("Segoe MDL2 Assets"), Name = "OuterButton" };
                innerButton = new FontIcon() { Glyph = "\uE73B", FontFamily = new Windows.UI.Xaml.Media.FontFamily("Segoe MDL2 Assets"), Foreground = new SolidColorBrush(Colors.Red), Name = "InnerButton" };

                button.Label = "Stop";
                DisplayCurrentSyncMode.Visibility = Visibility.Visible;

                syncStatusToolTip.Content = AppState.Instance.SelectedEffect.EffectName;
                ToolTipService.SetToolTip(Sync_Button, syncStatusToolTip);
            }

            AppBarStartStopGrid.Children.Add(innerButton);
            AppBarStartStopGrid.Children.Add(outerButton);

            startAndStopSemaphore.Release();
        }

        private void NavigationView_Loaded(object sender, RoutedEventArgs e)
        {
            MainNavigationView.SelectedItem = MainNavigationView.MenuItems[0];
        }

        public RGBMasterUserControl()
        {
            this.InitializeComponent();
        }

        private void MainNavigationView_SelectionChanged(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            string selectionTag;

            if (args.IsSettingsSelected == true)
            {
                selectionTag = "SettingsPage";
            }
            else
            {
                selectionTag = (string)args.SelectedItemContainer.Tag;
            }

            var navigationResult = MainAppContentFrame.Navigate(pageToType[selectionTag], null, args.RecommendedNavigationTransitionInfo);
        }


        private void RGBMasterUserControl_Loaded(object sender, RoutedEventArgs e)
        {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            EventManager.Instance.InitializeProviders();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }
    }
}
