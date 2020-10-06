using AppExecutionManager.EventManagement;
using AppExecutionManager.State;
using Common;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
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
            RefreshDeviceCounter();
            AppState.Instance.RegisteredProviders.CollectionChanged += RefreshDeviceCounter;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            AppState.Instance.RegisteredProviders.CollectionChanged -= RefreshDeviceCounter;
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            var checkbox = sender as CheckBox;
            var discoveredDevice = (DiscoveredDevice)checkbox.Tag;

            discoveredDevice.IsChecked = checkbox.IsChecked.GetValueOrDefault();

            var discoveringProvider = AppState.Instance.RegisteredProviders.FirstOrDefault(x => x.Provider.ProviderGuid == discoveredDevice.Device.RgbMasterDiscoveringProvider);

            if (discoveringProvider != null)
            {
                var allDevicesDevice = discoveringProvider.Devices.FirstOrDefault(x => x.Device.DeviceType == DeviceType.AllDevices);

                if (allDevicesDevice != null && allDevicesDevice.IsChecked)
                {
                    bool shouldDisplayFlyout = false;

                    foreach (var device in discoveringProvider.Devices)
                    {
                        if (device.Device.RgbMasterDeviceGuid != allDevicesDevice.Device.RgbMasterDeviceGuid && device.IsChecked)
                        {
                            device.IsChecked = false;
                            shouldDisplayFlyout = true;
                        }
                    }

                    if (shouldDisplayFlyout)
                    {
                        var clickedCheckbox = (CheckBox)sender;

                        var flyoutPresenterStyle = new Style(typeof(FlyoutPresenter));

                        flyoutPresenterStyle.Setters.Add(new Setter(FrameworkElement.MaxWidthProperty, Width));

                        Flyout flyout = new Flyout()
                        {
                            Content = new TextBlock()
                            {
                                Text = "Specific devices are unselected when an 'All Devices' device is selected."
                            },
                            FlyoutPresenterStyle = flyoutPresenterStyle,
                            XamlRoot = clickedCheckbox.XamlRoot
                        };

                        flyout.ShowAt(clickedCheckbox);
                    }
                }
            }

            EventManager.Instance.UpdateSelectedDevices(AppState.Instance.RegisteredProviders.Select(prov => prov.Devices).SelectMany(devices => devices).ToImmutableList().ToList());
        }

        private void ManualConnectionButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Refresh_Button_Clicked(object sender, RoutedEventArgs e)
        {
            // EventManager.Instance.InitializeProviders();

            var button = (Button)sender;

            var teachingTip = button.Resources["TeachingTip_RefreshDevices"] as Microsoft.UI.Xaml.Controls.TeachingTip;
            teachingTip.Target = button;
            teachingTip.PreferredPlacement = TeachingTipPlacementMode.Left;
            teachingTip.IsOpen = true;

        }

        private void TeachingTip_RefreshDevices_ActionButtonClick(TeachingTip sender, object args)
        {
            sender.IsOpen = false;
            EventManager.Instance.InitializeProviders();
        }

        private void RefreshDeviceCounter(object sender, NotifyCollectionChangedEventArgs args)
        {
            RefreshDeviceCounter();
        }

        private void RefreshDeviceCounter()
        {
            int foundedDevices = 0;
            foreach (var provider in AppState.Instance.RegisteredProviders)
            {
                foreach (var device in provider.Devices)
                {
                    foundedDevices += 1;
                }
            }
        }

        private void ListView_Loaded(object sender, RoutedEventArgs e)
        {
         //   this.ChosenDevicesListView.ItemsSource = AppState.Instance.RegisteredProviders.Select(provider=> new RegisteredProvider() { Provider = provider.Provider, Devices = provider.Devices.Where(device => device.IsChecked)})
        }

        private void Change_Device_Name_Button_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;

            var teachingTip = button.Resources["TeachingTip_SetName"] as Microsoft.UI.Xaml.Controls.TeachingTip;
            teachingTip.Target = button;
            teachingTip.PreferredPlacement = TeachingTipPlacementMode.Right;
            teachingTip.IsOpen = true;
        }

        private async void Device_Info_Button_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var discoveredDevice = (DiscoveredDevice)button.DataContext;
            var deviceInfoContentDialog = GenerateDeviceInfoContentDialog(discoveredDevice.Device, button.XamlRoot); // lmfaooo
            await deviceInfoContentDialog.ShowAsync();
        }

        private ContentDialog GenerateDeviceInfoContentDialog(DeviceMetadata deviceMetadata, XamlRoot elementRoot)
        {
            var contentDialogInnerContent = new StackPanel() { Orientation = Orientation.Vertical };

            // Device Name
            var deviceNameStackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
            deviceNameStackPanel.Children.Add(new TextBlock() { Text = $"Device name: " , FontWeight = FontWeight = Windows.UI.Text.FontWeights.Bold, Margin = new Thickness(0, 0, 4, 0) });
            deviceNameStackPanel.Children.Add(new TextBlock() { Text = $"{deviceMetadata.DeviceName}" });
            contentDialogInnerContent.Children.Add(deviceNameStackPanel);

            // Provider Name
            var providerNameStackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
            providerNameStackPanel.Children.Add(new TextBlock() { Text = $"Provider name: ", FontWeight = FontWeight = Windows.UI.Text.FontWeights.Bold, Margin = new Thickness(0, 0, 4, 0) });
            providerNameStackPanel.Children.Add(new TextBlock() { Text = AppState.Instance.RegisteredProviders.FirstOrDefault(x => x.Provider.ProviderGuid == deviceMetadata.RgbMasterDiscoveringProvider)?.Provider?.ProviderName });
            contentDialogInnerContent.Children.Add(providerNameStackPanel);

            // Device Type
            var deviceTypeStackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
            deviceTypeStackPanel.Children.Add(new TextBlock() { Text = $"Device type: ", FontWeight = FontWeight = Windows.UI.Text.FontWeights.Bold, Margin = new Thickness(0, 0, 4, 0) });
            deviceTypeStackPanel.Children.Add(new TextBlock() { Text = $"{DeviceTypeToText(deviceMetadata.DeviceType)}" });
            contentDialogInnerContent.Children.Add(deviceTypeStackPanel);

            // Device GUID
            var deviceGUIDStackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
            deviceGUIDStackPanel.Children.Add(new TextBlock() { Text = $"Device ID: ", FontWeight = FontWeight = Windows.UI.Text.FontWeights.Bold, Margin = new Thickness(0, 0, 4, 0) });
            deviceGUIDStackPanel.Children.Add(new TextBlock() { Text = $"{deviceMetadata.RgbMasterDeviceGuid}" });
            contentDialogInnerContent.Children.Add(deviceGUIDStackPanel);

            // Device Operations
            var deviceOperationStackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
            var supportedOperationsList = new StackPanel() { Orientation = Orientation.Vertical };
            foreach (var supportedOp in deviceMetadata.SupportedOperations)
            {
                supportedOperationsList.Children.Add(new TextBlock() { Text = OperationTypeToText(supportedOp) });
            }

            deviceOperationStackPanel.Children.Add(new TextBlock() { Text = $"Supported operations: ", FontWeight = Windows.UI.Text.FontWeights.Bold, Margin = new Thickness(0, 0, 4, 0) });
            deviceOperationStackPanel.Children.Add(supportedOperationsList);
            contentDialogInnerContent.Children.Add(deviceOperationStackPanel);

            Image deviceIcon = new Image
            {
                Source = new BitmapImage { UriSource = new Uri(this.BaseUri, deviceMetadata.DeviceIconAssetPath) },
                Height = 24,
                Width = 24
            };

            StackPanel contentDialogTitleStackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
            Image contentDialogDeviceIconImage = deviceIcon;
            TextBlock contentDialogDeviceName = new TextBlock() { Text = deviceMetadata.DeviceName, Margin = new Thickness(4, 0, 0, 0) };
            contentDialogTitleStackPanel.Children.Add(contentDialogDeviceIconImage);
            contentDialogTitleStackPanel.Children.Add(contentDialogDeviceName);
            return new ContentDialog()
            {
                Title = contentDialogTitleStackPanel,
                CloseButtonText = "Close",
                Content = contentDialogInnerContent,
                XamlRoot = elementRoot // lmao
            };
        }

        private string OperationTypeToText(OperationType opType)
        {
            string operation;

            switch (opType)
            {
                case OperationType.GetColor:
                    operation = "Obtain current color";
                    break;
                case OperationType.SetColor:
                    operation = "Modify color";
                    break;
                case OperationType.GetBrightness:
                    operation = "Obtain current brightness";
                    break;
                case OperationType.SetBrightness:
                    operation = "Modify brightness";
                    break;
                case OperationType.TurnOn:
                    operation = "Turn on";
                    break;
                case OperationType.TurnOff:
                    operation = "Turn off";
                    break;
                default:
                    throw new NotSupportedException($"The operation type {opType} is not supported.");
            }

            return operation;
        }

        private string DeviceTypeToText(DeviceType deviceType)
        {
            string deviceTypeText;

            switch (deviceType)
            {
                case DeviceType.Unknown:
                    deviceTypeText = "Unknown";
                    break;
                case DeviceType.Lightbulb:
                    deviceTypeText = "Lightbulb";
                    break;
                case DeviceType.LedStrip:
                    deviceTypeText = "Led Strip";
                    break;
                case DeviceType.Keyboard:
                    deviceTypeText = "Keyboard";
                    break;
                case DeviceType.Mouse:
                    deviceTypeText = "Mouse";
                    break;
                case DeviceType.Fan:
                    deviceTypeText = "Fan";
                    break;
                case DeviceType.Mousepad:
                    deviceTypeText = "Mousepad";
                    break;
                case DeviceType.Speaker:
                    deviceTypeText = "Speaker";
                    break;
                case DeviceType.Headset:
                    deviceTypeText = "Headset";
                    break;
                case DeviceType.Keypad:
                    deviceTypeText = "Keypad";
                    break;
                case DeviceType.Memory:
                    deviceTypeText = "Memory";
                    break;
                case DeviceType.GPU:
                    deviceTypeText = "Graphics Card";
                    break;
                case DeviceType.Motherboard:
                    deviceTypeText = "Motherboard";
                    break;
                case DeviceType.Chair:
                    deviceTypeText = "Chair";
                    break;
                case DeviceType.AllDevices:
                    deviceTypeText = "All Devices";
                    break; 
                default:
                    throw new NotSupportedException($"There is no matching text for device type {deviceType}. Make sure to include the translation in {MethodBase.GetCurrentMethod()}.");
            }

            return deviceTypeText;
        }

        //private void Change_Device_Name_Button_Click(object sender, RoutedEventArgs e)
        //{
        //    TeachingTip_SetName.IsOpen = true;
        //}

        //private void TeachingTip_SetName_ActionButtonClick(TeachingTip sender, object args)
        //{
        //    TeachingTip_SetName.IsOpen = false;
        //}
    }
}
