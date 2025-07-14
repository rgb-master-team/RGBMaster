using AppExecutionManager.EventManagement;
using AppExecutionManager.State;
using Common;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Utils;

namespace RGBMaster.Pages;

public sealed partial class DevicesPage : Page, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    public double ProvidersLoadingProgressValue => AppState.Instance.ProvidersLoadingProgress;
    public ProviderMetadata CurrentProcessedProvider => AppState.Instance.CurrentProcessedProvider;

    public ObservableCollection<RegisteredProvider> RegisteredProviders 
    { 
        get => AppState.Instance.RegisteredProviders;
    }

    public DevicesPage()
    {
        this.InitializeComponent();
        RefreshDeviceCounter();
        AppState.Instance.RegisteredProviders.CollectionChanged += RefreshDeviceCounter;
        AppState.Instance.PropertyChanged += AppState_PropertyChanged;
    }

    private void AppState_PropertyChanged(object sender, PropertyChangedEventArgs e)
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

                    var flyout = new Flyout()
                    {
                        Content = new TextBlock()
                        {
                            Text = "Specific devices are unselected when an 'All Devices' device is selected."
                        }
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

    private async void DevicePropertiesContextItem(object sender, RoutedEventArgs e)
    {
        var button = (MenuFlyoutItem)sender;
        var discoveredDevice = (DiscoveredDevice)button.DataContext;
        var deviceInfoContentDialog = GenerateDeviceInfoContentDialog(discoveredDevice.Device, button.XamlRoot);
        await deviceInfoContentDialog.ShowAsync();
    }

    private ContentDialog GenerateDeviceInfoContentDialog(DeviceMetadata deviceMetadata, XamlRoot elementRoot)
    {
        var contentDialogInnerContent = new StackPanel() { Orientation = Orientation.Vertical };

        // Device Name
        var deviceNameStackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
        deviceNameStackPanel.Children.Add(new TextBlock() { Text = "Device name: ", FontWeight = Microsoft.UI.Text.FontWeights.Bold, Margin = new Thickness(0, 0, 4, 0) });
        deviceNameStackPanel.Children.Add(new TextBlock() { Text = deviceMetadata.DeviceName });
        contentDialogInnerContent.Children.Add(deviceNameStackPanel);

        // Provider Name
        var providerNameStackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
        providerNameStackPanel.Children.Add(new TextBlock() { Text = "Provider name: ", FontWeight = Microsoft.UI.Text.FontWeights.Bold, Margin = new Thickness(0, 0, 4, 0) });
        providerNameStackPanel.Children.Add(new TextBlock() { Text = AppState.Instance.RegisteredProviders.FirstOrDefault(x => x.Provider.ProviderGuid == deviceMetadata.RgbMasterDiscoveringProvider)?.Provider?.ProviderName });
        contentDialogInnerContent.Children.Add(providerNameStackPanel);

        // Device Type
        var deviceTypeStackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
        deviceTypeStackPanel.Children.Add(new TextBlock() { Text = "Device type: ", FontWeight = Microsoft.UI.Text.FontWeights.Bold, Margin = new Thickness(0, 0, 4, 0) });
        deviceTypeStackPanel.Children.Add(new TextBlock() { Text = DeviceTypeToText(deviceMetadata.DeviceType) });
        contentDialogInnerContent.Children.Add(deviceTypeStackPanel);

        // Device GUID
        var deviceGUIDStackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
        deviceGUIDStackPanel.Children.Add(new TextBlock() { Text = "Device ID: ", FontWeight = Microsoft.UI.Text.FontWeights.Bold, Margin = new Thickness(0, 0, 4, 0) });
        deviceGUIDStackPanel.Children.Add(new TextBlock() { Text = deviceMetadata.RgbMasterDeviceGuid.ToString() });
        contentDialogInnerContent.Children.Add(deviceGUIDStackPanel);

        // Device Operations
        var deviceOperationStackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
        var supportedOperationsList = new StackPanel() { Orientation = Orientation.Vertical };
        foreach (var supportedOp in deviceMetadata.SupportedOperations)
        {
            supportedOperationsList.Children.Add(new TextBlock() { Text = OperationTypeToText(supportedOp) });
        }

        deviceOperationStackPanel.Children.Add(new TextBlock() { Text = "Supported operations: ", FontWeight = Microsoft.UI.Text.FontWeights.Bold, Margin = new Thickness(0, 0, 4, 0) });
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
            XamlRoot = elementRoot
        };
    }

    private string OperationTypeToText(OperationType opType)
    {
        return opType switch
        {
            OperationType.GetColor => "Obtain current color",
            OperationType.SetColor => "Modify color",
            OperationType.GetBrightness => "Obtain current brightness",
            OperationType.SetBrightness => "Modify brightness",
            OperationType.TurnOn => "Turn on",
            OperationType.TurnOff => "Turn off",
            OperationType.SetColorSmoothly => "Set gradient",
            _ => throw new NotSupportedException($"The operation type {opType} is not supported.")
        };
    }

    private string DeviceTypeToText(DeviceType deviceType)
    {
        return deviceType switch
        {
            DeviceType.Unknown => "Unknown",
            DeviceType.Lightbulb => "Lightbulb",
            DeviceType.LedStrip => "Led Strip",
            DeviceType.Keyboard => "Keyboard",
            DeviceType.Mouse => "Mouse",
            DeviceType.Fan => "Fan",
            DeviceType.Mousepad => "Mousepad",
            DeviceType.Speaker => "Speaker",
            DeviceType.Headset => "Headset",
            DeviceType.Keypad => "Keypad",
            DeviceType.Memory => "Memory",
            DeviceType.GPU => "Graphics Card",
            DeviceType.Motherboard => "Motherboard",
            DeviceType.Chair => "Chair",
            DeviceType.AllDevices => "All Devices",
            _ => throw new NotSupportedException($"There is no matching text for device type {deviceType}. Make sure to include the translation in {MethodBase.GetCurrentMethod()}.")
        };
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
        }
        else
        {
            ProvidersProgress.Visibility = Visibility.Collapsed;
        }
    }

    private void TurnOnDeviceContextItem_Click(object sender, RoutedEventArgs e)
    {
        var device = (DiscoveredDevice)((MenuFlyoutItem)sender).DataContext;
        EventManager.Instance.TurnOnDevices(new System.Collections.Generic.List<DiscoveredDevice>() { device });
    }

    private void TurnOffDeviceContextItem_Click(object sender, RoutedEventArgs e)
    {
        var device = (DiscoveredDevice)((MenuFlyoutItem)sender).DataContext;
        EventManager.Instance.TurnOffDevices(new System.Collections.Generic.List<DiscoveredDevice>() { device });
    }
}