using Microsoft.Toolkit.Wpf.UI.XamlHost;
using RGBMasterWPFRunner.Pages;
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
    public partial class MainWindow : System.Windows.Window
    {
        // List of ValueTuple holding the Navigation Tag and the relative Navigation Page
        private Dictionary<string, Type> pageToType = new Dictionary<string, Type>()
        {
            { nameof(EffectsPage), typeof(EffectsPage) },
            /*{ nameof(DevicesPage), typeof(DevicesPage) },
            { nameof(ControlPanelPage), typeof(ControlPanelPage) },
            { nameof(SettingsPage), typeof(SettingsPage) }*/
        };

        private Windows.UI.Xaml.Controls.NavigationView NavigationView;
        private Windows.UI.Xaml.Controls.Frame MainAppContentFrame;

        public MainWindow()
        {
            InitializeComponent();

            MainNavigationViewWrapper.ChildChanged += MainNavigationViewWrapper_ChildChanged;
        }

        private Windows.UI.Xaml.Controls.CommandBar GenerateMainCommandBar()
        {
            var commandBar = new Windows.UI.Xaml.Controls.CommandBar()
            {
                OverflowButtonVisibility = CommandBarOverflowButtonVisibility.Collapsed,
                DefaultLabelPosition = CommandBarDefaultLabelPosition.Right
            };

            return commandBar;
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


            navigationView.Content = GenerateMainStackPanel();

            NavigationView = navigationView;
        }

        private Windows.UI.Xaml.Controls.StackPanel GenerateMainStackPanel()
        {
            var stackPanel = new Windows.UI.Xaml.Controls.StackPanel();

            stackPanel.Children.Add(GenerateMainCommandBar());
            stackPanel.Children.Add(GenerateMainAppContentFrame());

            return stackPanel;
        }

        private Windows.UI.Xaml.Controls.Frame GenerateMainAppContentFrame()
        {
            var frame = new Windows.UI.Xaml.Controls.Frame()
            {
                Name = "MainAppContentFrame",
                Padding = new Windows.UI.Xaml.Thickness(12, 0, 12, 24),
                IsTabStop = true
            };

            frame.NavigationFailed += Frame_NavigationFailed;

            MainAppContentFrame = frame;

            return frame;
        }

        private void Frame_NavigationFailed(object sender, Windows.UI.Xaml.Navigation.NavigationFailedEventArgs e)
        {
            throw new NotImplementedException();
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

            var pageType = pageToType[selectionTag];

            var pageInstance = Activator.CreateInstance(pageType) as System.Windows.Controls.Page;

            if (pageInstance != null)
            {
                MainAppContentFrame.Content = pageInstance;
            }
        }
    }
}
