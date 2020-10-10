using AppExecutionManager.EventManagement;
using AppExecutionManager.State;
using Common;
using EffectsExecution;
using GameSense;
using Hue;
using Logitech;
using MagicHome;
using Microsoft.Toolkit.Wpf.UI.XamlHost;
using NZXT;
using Provider;
using RazerChroma;
using Serilog;
using Serilog.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Yeelight;

namespace RGBMasterWPFRunner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        private SemaphoreSlim changeConnectedDevicesSemaphore = new SemaphoreSlim(1, 1);
        private SemaphoreSlim initializeProvidersSemaphore = new SemaphoreSlim(1, 1);
        private SemaphoreSlim changeStaticColorSemaphore = new SemaphoreSlim(1, 1);

        private readonly Dictionary<Guid, Provider.BaseProvider> supportedProviders = new Dictionary<Guid, Provider.BaseProvider>();
        private readonly Dictionary<Guid, EffectExecutor> supportedEffectsExecutors = new Dictionary<Guid, EffectExecutor>();
        private readonly Dictionary<Guid, Device> concreteDevices = new Dictionary<Guid, Device>();

        private RGBMasterUWPApp.RGBMasterUserControl MainUserControl;

        public MainWindow()
        {
            InitializeComponent();

            Package package = Package.Current;
            PackageId packageId = package.Id;
            PackageVersion version = packageId.Version;
            AppState.Instance.AppVersion = string.Format($"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}");

            var path = Path.Combine(
                    Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)),
                    @$"RGBMaster\Logs\{AppState.Instance.AppVersion}.txt"
                );

            var globalLog = new LoggerConfiguration()
                .MinimumLevel
                .Is(Serilog.Events.LogEventLevel.Debug)
                .Enrich.WithExceptionDetails()
                .WriteTo
                .File(
                    path,
                    fileSizeLimitBytes: 2 * 1024 * 1024,
                    rollOnFileSizeLimit: true,
                    retainedFileCountLimit: 5
                ).CreateLogger();

            // Note - this line makes our "global log" the root log of the app.
            // meaning - when a library writes to log via microsoft's diagnostic it is sinked to this globalLog object.
            // We may want to change this behaviour in the subject as we grow. :)
            Log.Logger = globalLog;

            globalLog.Information("Initializing RGBMaster.....");

            CreateAndSetSupportedProviders(new List<BaseProvider>() { new YeelightProvider(), new MagicHomeProvider(), new RazerChromaProvider(), new LogitechProvider(), new GameSenseProvider(), new HueProvider(), new NZXTProvider() });

            CreateAndSetSupportedEffectsExecutors(new List<EffectExecutor>() { new MusicEffectExecutor(), new DominantDisplayColorEffectExecutor(), new CursorColorEffectExecutor(), new StaticColorEffectExecutor() });
            SetUIStateEffects();

            EventManager.Instance.SubscribeToEffectActivationRequests(Instance_EffectChanged);
            EventManager.Instance.SubscribeToSelectedDevicesChanged(Instance_SelectedDevicesChanged);
            EventManager.Instance.SubscribeToInitializeProvidersRequests(InitializeProviders);
            EventManager.Instance.SubscribeToStaticColorChanges(ChangeStaticColor);
            EventManager.Instance.SubscribeToTurnOnAllLightsRequests(TurnOnAllLights);
        }

        private async void TurnOnAllLights(object sender, EventArgs e)
        {
            var tasks = new List<Task>();

            foreach (var provider in AppState.Instance.RegisteredProviders)
            {
                foreach (var device in provider.Devices)
                {
                    if (device.IsChecked)
                    {
                        var deviceGuid = device.Device.RgbMasterDeviceGuid;
                        tasks.Add(Task.Run(() => concreteDevices[deviceGuid].TurnOn()));
                    }
                }
            }

            await Task.WhenAll(tasks);
        }

        private async void ChangeStaticColor(object sender, StaticColorEffectProps staticColorEffectProps)
        {
            AppState.Instance.StaticColorEffectProperties = staticColorEffectProps;

            if (AppState.Instance.IsEffectRunning)
            {
                var selectedEffectExecutor = supportedEffectsExecutors[AppState.Instance.ActiveEffect.EffectMetadataGuid];

                if (selectedEffectExecutor.executedEffectMetadata.Type == EffectType.StaticColor)
                {
                    await changeStaticColorSemaphore.WaitAsync();

                    ((StaticColorEffectMetadata)selectedEffectExecutor.executedEffectMetadata).UpdateProps(staticColorEffectProps);

                    await selectedEffectExecutor.Start();

                    changeStaticColorSemaphore.Release();
                }
            }
        }

        private void SetUIStateEffects()
        {
            AppState.Instance.Effects.Clear();

            foreach (var supportedEffectExecutor in supportedEffectsExecutors.Values)
            {
                AppState.Instance.Effects.Add(supportedEffectExecutor.executedEffectMetadata);
            }
        }

        private void CreateAndSetSupportedEffectsExecutors(IEnumerable<EffectExecutor> effectExecutors)
        {
            foreach (var effectExecutor in effectExecutors)
            {
                supportedEffectsExecutors.Add(effectExecutor.executedEffectMetadata.EffectMetadataGuid, effectExecutor);
            }
        }

        private void CreateAndSetSupportedProviders(IEnumerable<BaseProvider> providers)
        {
            foreach (var provider in providers)
            {
                supportedProviders[provider.ProviderMetadata.ProviderGuid] = provider;

                AppState.Instance.SupportedProviders.Add(provider.ProviderMetadata);
            }
        }

        private async void InitializeProviders(object sender, EventArgs e)
        {
            await initializeProvidersSemaphore.WaitAsync();
            Log.Logger.Information("Initializing providers.....");

            await CleanupDevicesAndProviders();

            AppState.Instance.ProvidersLoadingProgress = 0.0;
            AppState.Instance.IsLoadingProviders = true;
            AppState.Instance.CurrentProcessedProvider = null;

            var tasks = new List<Task>();

            var registeredProviders = new List<BaseProvider>();

            foreach (var provider in supportedProviders.Values)
            {
                Log.Logger.Warning("Trying to innitialize provider {A} with guid: {B}.", provider.ProviderMetadata.ProviderName, provider.ProviderMetadata.ProviderGuid);

                var didInitialize = await provider.Register();

                if (!didInitialize)
                {
                    Log.Logger.Warning("Provider {A} failed to initialize!", provider.ProviderMetadata.ProviderName);
                    continue;
                }

                Log.Logger.Information("Provider {A} initialized.", provider.ProviderMetadata.ProviderName);

                registeredProviders.Add(provider);
            }

            int loadedProviders = 0;

            foreach (var provider in registeredProviders)
            {
                AppState.Instance.CurrentProcessedProvider = provider.ProviderMetadata;

                var discoveredDevices = await provider.Discover();

                if ((discoveredDevices?.Count).GetValueOrDefault() > 0)
                {
                    foreach (var device in discoveredDevices)
                    {
                        concreteDevices.Add(device.DeviceMetadata.RgbMasterDeviceGuid, device);
                    }

                    Log.Logger.Information("Listing discovered devices for provider {A}:", provider.ProviderMetadata.ProviderName);
                    Log.Logger.Information(string.Join("\n", discoveredDevices.Select(discoveredDevice => $"name: {discoveredDevice.DeviceMetadata.DeviceName}, guid: {discoveredDevice.DeviceMetadata.RgbMasterDeviceGuid}")));

                    AppState.Instance.RegisteredProviders.Add(new RegisteredProvider()
                    {
                        Provider = provider.ProviderMetadata,
                        Devices = new System.Collections.ObjectModel.ObservableCollection<DiscoveredDevice>(discoveredDevices.Select(device => new DiscoveredDevice() { Device = device.DeviceMetadata, IsChecked = false }))
                    });
                }

                loadedProviders++;
                AppState.Instance.ProvidersLoadingProgress = ((double)loadedProviders / (double)registeredProviders.Count)*100.0;
            }

            AppState.Instance.IsLoadingProviders = false;

            if (AppState.Instance.IsEffectRunning)
            {
                EventManager.Instance.RequestEffectActivation(null);
            }

            initializeProvidersSemaphore.Release();
        }

        private async void Instance_EffectChanged(object sender, EffectMetadata e)
        {
            // If we want to stop syncing the effect, and there is an active effect right now, stop it
            if (e == null && AppState.Instance.ActiveEffect != null)
            {
                await supportedEffectsExecutors[AppState.Instance.ActiveEffect.EffectMetadataGuid].Stop();
            }
            else
            {
                var newEffectExecutor = supportedEffectsExecutors[e.EffectMetadataGuid];

                Log.Logger.Information("Effect changed to {A}.", newEffectExecutor.executedEffectMetadata.EffectName);

                newEffectExecutor.ChangeConnectedDevices(AppState.Instance.RegisteredProviders.Select(provider => provider.Devices).SelectMany(devices => devices).Where(device => device.IsChecked).Select(dev => this.concreteDevices[dev.Device.RgbMasterDeviceGuid]));

                if (AppState.Instance.ActiveEffect != null)
                {
                    await supportedEffectsExecutors[AppState.Instance.ActiveEffect.EffectMetadataGuid].Stop();
                }

                await supportedEffectsExecutors[e.EffectMetadataGuid].Start();
            }

            AppState.Instance.ActiveEffect = e;
        }

        private async void Instance_SelectedDevicesChanged(object sender, List<DiscoveredDevice> devices)
        {
            var newSelectedDevices = devices;

            await changeConnectedDevicesSemaphore.WaitAsync();

            foreach (var item in newSelectedDevices)
            {
                var concreteDevice = concreteDevices[item.Device.RgbMasterDeviceGuid];

                if (!item.IsChecked && concreteDevice.IsConnected)
                {
                    Log.Logger.Warning("Turning off device with GUID {A}.", concreteDevice.DeviceMetadata.RgbMasterDeviceGuid);
                    concreteDevice.TurnOff();

                    Log.Logger.Warning("Disconnecting from device with GUID {A}.", concreteDevice.DeviceMetadata.RgbMasterDeviceGuid);
                    var didSucceed = await concreteDevice.Disconnect();

                    if (!didSucceed)
                    {
                        Log.Logger.Error("Failed disconnecting from device with GUID {A}.", concreteDevice.DeviceMetadata.RgbMasterDeviceGuid);
                        InformErrorOnFlyout($"Failed to disconnect from device {item.Device.DeviceName}. \nThis might be a problem in the {AppState.Instance.SupportedProviders.First(x => x.ProviderGuid == item.Device.RgbMasterDiscoveringProvider).ProviderName} provider. \nMake sure network connection is valid and try to refresh devices or restart the app.");
                    }
                }
                else if (item.IsChecked && !concreteDevice.IsConnected)
                {
                    Log.Logger.Warning("Connecting to device with GUID {A}.", concreteDevice.DeviceMetadata.RgbMasterDeviceGuid);
                    var didSucceed = await concreteDevice.Connect();

                    if (!didSucceed)
                    {
                        Log.Logger.Error("Failed connecting to device with GUID {A}.", concreteDevice.DeviceMetadata.RgbMasterDeviceGuid);
                        InformErrorOnFlyout($"Failed to connect to device {item.Device.DeviceName}. \nThis might be a problem in the {AppState.Instance.SupportedProviders.First(x => x.ProviderGuid == item.Device.RgbMasterDiscoveringProvider).ProviderName} provider. \nMake sure network connection is valid and try to refresh devices or restart the app.");

                        item.IsChecked = false;
                    }
                    else
                    {
                        Log.Logger.Warning("Turning on device with GUID {A}.", concreteDevice.DeviceMetadata.RgbMasterDeviceGuid);
                        concreteDevice.TurnOn();
                    }
                }
            }

            if (AppState.Instance.IsEffectRunning)
            {
                supportedEffectsExecutors[AppState.Instance.ActiveEffect.EffectMetadataGuid].ChangeConnectedDevices(newSelectedDevices.Where(device => device.IsChecked).Select(dev => this.concreteDevices[dev.Device.RgbMasterDeviceGuid]));
            }

            changeConnectedDevicesSemaphore.Release();
        }

        private void InformErrorOnFlyout(string errorMessage)
        {
            var flyoutContentStackPanel = new StackPanel() { Orientation = Orientation.Horizontal };

            flyoutContentStackPanel.Children.Add(new FontIcon
            {
                FontFamily = new Windows.UI.Xaml.Media.FontFamily("Segoe MDL2 Assets"),
                Glyph = "\uE783",
                FontSize = 42,
                Margin = new Thickness(0, 0, 8, 0)
            });

            flyoutContentStackPanel.Children.Add(new TextBlock()
            {
                Text = errorMessage
            });

            var flyoutPresenterStyle = new Style(typeof(FlyoutPresenter));

            flyoutPresenterStyle.Setters.Add(new Setter(FrameworkElement.MaxWidthProperty, MainUserControl.Width));

            var flyout = new Flyout()
            {
                Content = flyoutContentStackPanel,
                FlyoutPresenterStyle = flyoutPresenterStyle,
                Placement = Windows.UI.Xaml.Controls.Primitives.FlyoutPlacementMode.Bottom,
                XamlRoot = MainUserControl.XamlRoot
            };

            flyout.ShowAt(MainUserControl);
        }

        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            await CleanupDevicesAndProviders();
        }

        private async Task CleanupDevicesAndProviders()
        {
            if (AppState.Instance.IsEffectRunning)
            {
                await supportedEffectsExecutors[AppState.Instance.ActiveEffect.EffectMetadataGuid].Stop();
            }

            foreach (var concreteDevice in concreteDevices)
            {
                await concreteDevice.Value.Disconnect();
            }

            concreteDevices.Clear();

            foreach (var provider in AppState.Instance.RegisteredProviders)
            {
                await supportedProviders[provider.Provider.ProviderGuid].Unregister();
            }

            AppState.Instance.RegisteredProviders.Clear();

            AppState.Instance.IsLoadingProviders = false;
            AppState.Instance.ProvidersLoadingProgress = 100.0;
            AppState.Instance.CurrentProcessedProvider = null;
        }

        private void MainUserControlWrapper_ChildChanged(object sender, EventArgs e)
        {
            MainUserControl = (RGBMasterUWPApp.RGBMasterUserControl) ((WindowsXamlHost)sender).Child;
        }
    }
}
