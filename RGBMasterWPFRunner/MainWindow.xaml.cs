using AppExecutionManager.EventManagement;
using AppExecutionManager.State;
using Colore.Logging;
using Common;
using Corsair.ProviderImpl;
using EffectsExecution;
using GameSense;
using Hue;
using Logitech;
using MagicHome;
using Microsoft.Toolkit.Wpf.UI.XamlHost;
using NAudio.CoreAudioApi;
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
        private const string ToggleDeviceOnCheckUserSettingKey = "ToggleDeviceOnCheck";
        private const string LogPathKey = "LogPath";

        private SemaphoreSlim changeConnectedDevicesSemaphore = new SemaphoreSlim(1, 1);
        private SemaphoreSlim initializeProvidersSemaphore = new SemaphoreSlim(1, 1);
        private SemaphoreSlim changeStaticColorSemaphore = new SemaphoreSlim(1, 1);

        private readonly Dictionary<Guid, Provider.BaseProvider> supportedProviders = new Dictionary<Guid, Provider.BaseProvider>();
        private readonly Dictionary<Guid, EffectExecutor> supportedEffectsExecutors = new Dictionary<Guid, EffectExecutor>();
        private readonly Dictionary<Guid, Device> concreteDevices = new Dictionary<Guid, Device>();

        private RGBMasterUWPApp.RGBMasterUserControl MainUserControl;

        public MainWindow()
        {
            Dispatcher.UnhandledException += Dispatcher_UnhandledException;

            EventManager.Instance.SubscribeToEffectActivationRequests(Instance_EffectChanged);
            EventManager.Instance.SubscribeToSelectedDevicesChanged(Instance_SelectedDevicesChanged);
            EventManager.Instance.SubscribeToInitializeProvidersRequests(InitializeProviders);
            EventManager.Instance.SubscribeToStaticColorChanges(ChangeStaticColor);
            EventManager.Instance.SubscribeToLoadAudioDevicesRequests(LoadAudioDevices);
            EventManager.Instance.SubscribeToTurnOnDevicesRequests(TurnOnDevices);
            EventManager.Instance.SubscribeToTurnOffDevicesRequests(TurnOffDevices);
            EventManager.Instance.SubscribeToStoreUserSettingRequests(StoreUserSetting);
            EventManager.Instance.SubscribeToLoadUserSettingRequests(LoadUserSetting);
            EventManager.Instance.SubscribeToResetUserSettingsToDefaultRequests(ResetUserSettingsToDefault);

            SetAppVersion();
            Serilog.Core.Logger globalLog = GenerateAppLogger();

            globalLog.Information("Initializing RGBMaster.....");

            CreateAndSetSupportedProviders(new List<BaseProvider>() { new YeelightProvider(), new MagicHomeProvider(), new RazerChromaProvider(), new LogitechProvider(), new GameSenseProvider(), new HueProvider(), new CorsairProvider() /*new NZXTProvider()*/ });
            CreateAndSetSupportedEffectsExecutors(new List<EffectExecutor>() { new MusicEffectExecutor(), new GradientEffectExecutor(), new DominantDisplayColorEffectExecutor(), new StaticColorEffectExecutor(), new CursorColorEffectExecutor() });
            SetUIStateEffects();

            InitializeComponent();
        }

        private void ResetUserSettingsToDefault(object sender, IEnumerable<string> keys)
        {
            foreach (var key in keys)
            {
                var propValueObj = Settings1.Default.PropertyValues[key];
                var defaultValue = Settings1.Default.Properties[key].DefaultValue;

                propValueObj.SerializedValue = defaultValue;
                propValueObj.Deserialized = false;
            }

            Settings1.Default.Save();

            foreach (var key in keys)
            {
                AppState.Instance.UserSettingsCache[key] = Settings1.Default[key];
            }
        }

        private void LoadUserSetting(object sender, string e)
        {
            // TODO - Consider propagating a notification of change. With NotifyPropertyChanged...
            AppState.Instance.UserSettingsCache[e] = Settings1.Default[e];
        }

        private void StoreUserSetting(object sender, Tuple<string, object> e)
        {
            // TODO - Turn this into a service, facade, agent, whatever.
            // Are we ever gonna use DI in here, anyway?
            Settings1.Default[e.Item1] = e.Item2;
            Settings1.Default.Save();

            AppState.Instance.UserSettingsCache[e.Item1] = e.Item2;
        }

        private void Dispatcher_UnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Log.Logger.Fatal(e.Exception, "An unhandled exception was thrown.");
        }

        private static Serilog.Core.Logger GenerateAppLogger()
        {
            string path;

            EventManager.Instance.LoadUserSetting(LogPathKey);

            if (!AppState.Instance.UserSettingsCache.TryGetValue(LogPathKey, out var logPathObj) ||
                string.IsNullOrWhiteSpace(logPathObj as string))
            {
                path = Path.Combine(
                                Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)),
                                @$"RGBMaster\Logs\RGBMaster.txt"
                            );
            }
            else
            {
                path = (string)logPathObj;
            }

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
            return globalLog;
        }

        private static void SetAppVersion()
        {
            Package package = Package.Current;
            PackageId packageId = package.Id;
            PackageVersion version = packageId.Version;
            AppState.Instance.AppVersion = string.Format($"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}");
        }

        private async Task<ConnectionAlterResult> AttemptDeviceConnection(Device concreteDevice, DiscoveredDevice discoveredDevice)
        {
            discoveredDevice.ConnectionStatus = ConnectionStatus.Connecting;

            Log.Logger.Warning("Device with GUID {A} is not connected. Attempting connection.", concreteDevice.DeviceMetadata.RgbMasterDeviceGuid);

            var connectionResult = await concreteDevice.Connect();

            if (connectionResult == ConnectionAlterResult.Failed)
            {
                Log.Logger.Error("Failed connecting to device with GUID {A}.", concreteDevice.DeviceMetadata.RgbMasterDeviceGuid);
                InformErrorOnFlyout($"Failed to connect to device {concreteDevice.DeviceMetadata.DeviceName}. \nThis might be a problem in the {AppState.Instance.SupportedProviders.First(x => x.ProviderGuid == concreteDevice.DeviceMetadata.RgbMasterDiscoveringProvider).ProviderName} provider. \nMake sure network connection is valid and try to refresh devices or restart the app.");
                discoveredDevice.ConnectionStatus = ConnectionStatus.Disconnected;
            }
            else if (connectionResult == ConnectionAlterResult.Succeeded)
            {
                discoveredDevice.ConnectionStatus = ConnectionStatus.Connected;
            }

            return connectionResult;
        }

        private async Task<ConnectionAlterResult> AttemptDeviceDisconnection(Device concreteDevice, DiscoveredDevice discoveredDevice)
        {
            discoveredDevice.ConnectionStatus = ConnectionStatus.Disconnecting;

            Log.Logger.Warning("Disconnecting from device with GUID {A}.", concreteDevice.DeviceMetadata.RgbMasterDeviceGuid);
            var connectionAlterResult = await concreteDevice.Disconnect();

            if (connectionAlterResult == ConnectionAlterResult.Failed)
            {
                Log.Logger.Error("Failed disconnecting from device with GUID {A}.", concreteDevice.DeviceMetadata.RgbMasterDeviceGuid);
                InformErrorOnFlyout($"Failed to disconnect from device {concreteDevice.DeviceMetadata.DeviceName}. \nThis might be a problem in the {AppState.Instance.SupportedProviders.First(x => x.ProviderGuid == concreteDevice.DeviceMetadata.RgbMasterDiscoveringProvider).ProviderName} provider. \nMake sure network connection is valid and try to refresh devices or restart the app.");
                discoveredDevice.ConnectionStatus = ConnectionStatus.Disconnected;
            }
            else if (connectionAlterResult == ConnectionAlterResult.Succeeded)
            {
                discoveredDevice.ConnectionStatus = ConnectionStatus.Disconnected;
            }

            return connectionAlterResult;
        }

        private async void TurnOffDevices(object sender, List<DiscoveredDevice> e)
        {
            foreach (var device in e)
            {
                await TurnOffDevice(device);
            }
        }

        private async void TurnOnDevices(object sender, List<DiscoveredDevice> e)
        {
            foreach (var device in e)
            {
                await TurnOnDevice(device);
            }
        }

        private async Task TurnOffDevice(DiscoveredDevice item)
        {
            var concreteDevice = concreteDevices[item.Device.RgbMasterDeviceGuid];

            var isDeviceConnected = await AttemptDeviceConnection(concreteDevice, item);

            if (isDeviceConnected == ConnectionAlterResult.Succeeded && concreteDevice.DeviceMetadata.IsOperationSupported(OperationType.TurnOff))
            {
                Log.Logger.Warning("Turning off device with GUID {A}.", concreteDevice.DeviceMetadata.RgbMasterDeviceGuid);
                await concreteDevice.TurnOff();
            }
        }

        private async Task TurnOnDevice(DiscoveredDevice item)
        {
            var concreteDevice = concreteDevices[item.Device.RgbMasterDeviceGuid];

            var isDeviceConnected = await AttemptDeviceConnection(concreteDevice, item);

            if (isDeviceConnected == ConnectionAlterResult.Succeeded && concreteDevice.DeviceMetadata.IsOperationSupported(OperationType.TurnOn))
            {
                Log.Logger.Warning("Turning on device with GUID {A}.", concreteDevice.DeviceMetadata.RgbMasterDeviceGuid);
                await concreteDevice.TurnOn();
            }
        }

        private void LoadAudioDevices(object sender, EventArgs e)
        {
            var audioCaptureDevices = new List<AudioCaptureDevice>();

            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();

            var defaultOutputDevice = enumerator.HasDefaultAudioEndpoint(DataFlow.Render, Role.Console) ? enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console) : null;
            var defaultInputDevice = enumerator.HasDefaultAudioEndpoint(DataFlow.Capture, Role.Console) ? enumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Console) : null;

            foreach (MMDevice device in enumerator.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active))
            {
                bool isDeviceDefault = false;
                var flowType = DataFlowToFlowType(device.DataFlow);

                if (
                    (flowType == AudioCaptureDeviceFlowType.Input && device.ID == defaultInputDevice?.ID) ||
                    (flowType == AudioCaptureDeviceFlowType.Output && device.ID == defaultOutputDevice?.ID)
                    )
                {
                    isDeviceDefault = true;
                }

                audioCaptureDevices.Add(new AudioCaptureDevice(device.ID, device.FriendlyName, flowType, isDeviceDefault));
            }

            AppState.Instance.AudioCaptureDevices = audioCaptureDevices;
        }

        private AudioCaptureDeviceFlowType DataFlowToFlowType(DataFlow dataFlow)
        {
            return dataFlow switch
            {
                DataFlow.Render => AudioCaptureDeviceFlowType.Output,
                DataFlow.Capture => AudioCaptureDeviceFlowType.Input,
                _ => AudioCaptureDeviceFlowType.Unknown,
            };
        }

        private async void ChangeStaticColor(object sender, StaticColorEffectProps staticColorEffectProps)
        {
            // TODO - Fix this into having a single source of truth...
            ((StaticColorEffectMetadata)AppState.Instance.Effects.First(effect => effect.Type == EffectType.StaticColor)).UpdateProps(staticColorEffectProps);

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
                Log.Logger.Warning("Trying to initialize provider {A} with guid: {B}.", provider.ProviderMetadata.ProviderName, provider.ProviderMetadata.ProviderGuid);

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

                var (didDiscoverSucceed, discoveredDevices) = await provider.Discover();

                if (!didDiscoverSucceed)
                {
                    Log.Logger.Warning("Provider {A} with GUID {B} failed to discover devices!", provider.ProviderMetadata.ProviderName, provider.ProviderMetadata.ProviderGuid);
                    continue;
                }

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
                        Devices = new System.Collections.ObjectModel.ObservableCollection<DiscoveredDevice>(discoveredDevices.Select(device => new DiscoveredDevice() { Device = device.DeviceMetadata, IsChecked = false, ConnectionStatus = ConnectionStatus.Disconnected }))
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
            EventManager.Instance.LoadUserSetting(ToggleDeviceOnCheckUserSettingKey);

            var shouldToggleDevice = AppState.Instance.UserSettingsCache.TryGetValue(ToggleDeviceOnCheckUserSettingKey, out var shouldTurnOnDeviceObj) ? (bool)shouldTurnOnDeviceObj : true;

            var newSelectedDevices = devices;

            await changeConnectedDevicesSemaphore.WaitAsync();

            foreach (var item in newSelectedDevices)
            {
                var concreteDevice = concreteDevices[item.Device.RgbMasterDeviceGuid];

                // If an item is not selected, but is connected - we need to attempt disconnecting it,
                // and turn if off by the user's configuration.
                if (!item.IsChecked && concreteDevice.IsConnected)
                {
                    if (shouldToggleDevice)
                    {
                        await TurnOffDevice(item);
                    }

                    var didDisconnect = await AttemptDeviceDisconnection(concreteDevice, item);
                }
                // If the item is checked, and is not connected - we need to attempt connecting it,
                // and turn it on by the user's configuration.
                else if (item.IsChecked && !concreteDevice.IsConnected)
                {
                    var didConnect = await AttemptDeviceConnection(concreteDevice, item);

                    if (didConnect == ConnectionAlterResult.Succeeded && shouldToggleDevice)
                    {
                        await TurnOnDevice(item);
                    }
                    else if (didConnect == ConnectionAlterResult.Failed)
                    {
                        item.IsChecked = false;
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
            EventManager.Instance.InformAppClosing();
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
