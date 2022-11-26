// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using AppExecutionManager.EventManagement;
using AppExecutionManager.State;
using Common;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Utils;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RGBMasterWinUI.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DevicesPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public double ProvidersLoadingProgressValue => AppState.Instance.ProvidersLoadingProgress;
        public ProviderMetadata CurrentProcessedProvider => AppState.Instance.CurrentProcessedProvider;

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
            AppState.Instance.PropertyChanged += AppState_PropertyChanged;
        }

        private void AppState_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AppState.IsLoadingProviders))
            {
                if (IsLoaded)
                {
                    HandleProgressbarAnimation();
                }
            }
            else if (e.PropertyName == nameof(AppState.ProvidersLoadingProgress))
            {
                NotifyPropertyChangedUtils.OnPropertyChanged(PropertyChanged, this, nameof(ProvidersLoadingProgressValue));
            }
            else if (e.PropertyName == nameof(AppState.CurrentProcessedProvider))
            {
                NotifyPropertyChangedUtils.OnPropertyChanged(PropertyChanged, this, nameof(CurrentProcessedProvider));
            }
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

            EventManager.Instance.UpdateSelectedDevices(AppState.Instance.RegisteredProviders.SelectMany(prov => prov.Devices).ToImmutableList().ToList());
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
        private async void DevicePropertiesContextItem(object sender, RoutedEventArgs e)
        {
            var button = (MenuFlyoutItem)sender;
            var discoveredDevice = (DiscoveredDevice)button.DataContext;
            var deviceInfoContentDialog = GenerateDeviceInfoContentDialog(discoveredDevice.Device, button.XamlRoot); // lmfaooo
            await deviceInfoContentDialog.ShowAsync();
        }

        private ContentDialog GenerateDeviceInfoContentDialog(DeviceMetadata deviceMetadata, XamlRoot elementRoot)
        {
            var contentDialogInnerContent = new StackPanel() { Orientation = Orientation.Vertical };

            // Device Name
            var deviceNameStackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
            deviceNameStackPanel.Children.Add(new TextBlock() { Text = $"Device name: ", FontWeight = FontWeights.Bold, Margin = new Thickness(0, 0, 4, 0) });
            deviceNameStackPanel.Children.Add(new TextBlock() { Text = $"{deviceMetadata.DeviceName}" });
            contentDialogInnerContent.Children.Add(deviceNameStackPanel);

            // Provider Name
            var providerNameStackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
            providerNameStackPanel.Children.Add(new TextBlock() { Text = $"Provider name: ",FontWeight = FontWeights.Bold, Margin = new Thickness(0, 0, 4, 0) });
            providerNameStackPanel.Children.Add(new TextBlock() { Text = AppState.Instance.RegisteredProviders.FirstOrDefault(x => x.Provider.ProviderGuid == deviceMetadata.RgbMasterDiscoveringProvider)?.Provider?.ProviderName });
            contentDialogInnerContent.Children.Add(providerNameStackPanel);

            // Device Type
            var deviceTypeStackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
            deviceTypeStackPanel.Children.Add(new TextBlock() { Text = $"Device type: ", FontWeight = FontWeights.Bold, Margin = new Thickness(0, 0, 4, 0) });
            deviceTypeStackPanel.Children.Add(new TextBlock() { Text = $"{deviceMetadata.DeviceTypeAsText}" });
            contentDialogInnerContent.Children.Add(deviceTypeStackPanel);

            // Device GUID
            var deviceGUIDStackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
            deviceGUIDStackPanel.Children.Add(new TextBlock() { Text = $"Device ID: ", FontWeight = FontWeights.Bold, Margin = new Thickness(0, 0, 4, 0) });
            deviceGUIDStackPanel.Children.Add(new TextBlock() { Text = $"{deviceMetadata.RgbMasterDeviceGuid}" });
            contentDialogInnerContent.Children.Add(deviceGUIDStackPanel);

            // Device Operations
            var deviceOperationStackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
            var supportedOperationsList = new StackPanel() { Orientation = Orientation.Vertical };
            foreach (var supportedOp in deviceMetadata.SupportedOperations)
            {
                supportedOperationsList.Children.Add(new TextBlock() { Text = OperationTypeToText(supportedOp) });
            }

            deviceOperationStackPanel.Children.Add(new TextBlock() { Text = $"Supported operations: ", FontWeight = FontWeights.Bold, Margin = new Thickness(0, 0, 4, 0) });
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
            string operation = null;

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
                case OperationType.SetColorSmoothly:
                    operation = "Set gradient";
                    break;
                default:
                    throw new NotSupportedException($"The operation type {opType} is not supported.");
            }

            return operation;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            HandleProgressbarAnimation();
        }

        private void HandleProgressbarAnimation()
        {
            if (AppState.Instance.IsLoadingProviders)
            {
                ProvidersProgress.Visibility = Visibility.Visible;
                EnterStoryboard.Begin();
            }
            else
            {
                ExitStoryboard.Begin();
            }
        }

        private void TurnOnDeviceContextItem_Click(object sender, RoutedEventArgs e)
        {
            var device = (DiscoveredDevice)((MenuFlyoutItem)sender).DataContext;
            EventManager.Instance.TurnOnDevices(new System.Collections.Generic.List<DiscoveredDevice>() { device });
        }

        private void SetNameContextItem_Click(object sender, RoutedEventArgs e)
        {
            var flyoutItem = (MenuFlyoutItem)sender;

            var triggeringCheckbox = (CheckBox)flyoutItem.FindName("DeviceSelectionCheckbox");

            var teachingTip = triggeringCheckbox.Resources["TeachingTip_SetName"] as Microsoft.UI.Xaml.Controls.TeachingTip;
            teachingTip.Target = triggeringCheckbox;
            teachingTip.PreferredPlacement = TeachingTipPlacementMode.Right;
            teachingTip.XamlRoot = triggeringCheckbox.XamlRoot;
            teachingTip.IsOpen = true;
        }

        private void TurnOffDeviceContextItem_Click(object sender, RoutedEventArgs e)
        {
            var device = (DiscoveredDevice)((MenuFlyoutItem)sender).DataContext;
            EventManager.Instance.TurnOffDevices(new System.Collections.Generic.List<DiscoveredDevice>() { device });
        }
    }
}
