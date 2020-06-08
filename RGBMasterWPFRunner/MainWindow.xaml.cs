using Microsoft.Toolkit.Wpf.UI.XamlHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using BitmapImage = Windows.UI.Xaml.Media.Imaging.BitmapImage;
using Image = Windows.UI.Xaml.Controls.Image;

namespace RGBMasterWPFRunner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NavigationView NavigationView;

        public MainWindow()
        {
            InitializeComponent();

            MainNavigationViewWrapper.ChildChanged += MainNavigationViewWrapper_ChildChanged;
        }

        private void MainNavigationViewWrapper_ChildChanged(object sender, EventArgs e)
        {
            var windowsXamlHost = (WindowsXamlHost)sender;

            var navigationView = (NavigationView)windowsXamlHost.Child;

            navigationView.PaneTitle = "RGB Master";
            navigationView.PaneDisplayMode = NavigationViewPaneDisplayMode.Auto;
            navigationView.IsBackButtonVisible = NavigationViewBackButtonVisible.Collapsed;

            navigationView.SelectionChanged += NavigationView_SelectionChanged;
            navigationView.Loaded += NavigationView_Loaded;

            navigationView.PaneHeader = new Image()
            {
                Source = new BitmapImage(new Uri("ms-appx:///Assets/128.png")),
                Width = 25,
                Height = 25
            };

            navigationView.MenuItems.Add(new NavigationViewItem()
            {
                Content = "Control Panel",
                Name = "StatusNavigationItem",
                Tag = "ControlPanelPage",
                Icon = new FontIcon()
                {
                    Glyph = "\uE946",
                    FontFamily = new Windows.UI.Xaml.Media.FontFamily("Segoe MDL2 Assets")
                }
            });

            navigationView.MenuItems.Add(new NavigationViewItem()
            {
                Content = "Devices",
                Name = "DevicesNavigationItem",
                Tag = "DevicesPage",
                Icon = new FontIcon()
                {
                    Glyph = "\uEA80",
                    FontFamily = new Windows.UI.Xaml.Media.FontFamily("Segoe MDL2 Assets")
                }
            });

            navigationView.MenuItems.Add(new NavigationViewItem()
            {
                Content = "Effects",
                Name = "Effects",
                Tag = "EffectsPage",
                Icon = new FontIcon()
                {
                    Glyph = "\uE790",
                    FontFamily = new Windows.UI.Xaml.Media.FontFamily("Segoe MDL2 Assets")
                }
            });

            NavigationView = navigationView;
        }

        private void NavigationView_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NavigationView.SelectedItem = NavigationView.MenuItems[0];
        }

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
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
    }
}
