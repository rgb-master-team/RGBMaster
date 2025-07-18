using Microsoft.UI.Xaml;
using AppExecutionManager.EventManagement;
using AppExecutionManager.State;
using Colore.Logging;
using Common;
using EffectsExecution;
using GameSense;
using Hue;
using Logitech;
using MagicHome;
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
using Yeelight;

namespace RGBMaster;

public partial class App : Application
{
    private Window m_window;
    private readonly Dictionary<Guid, Provider.BaseProvider> supportedProviders = new();
    private readonly Dictionary<Guid, EffectExecutor> supportedEffectsExecutors = new();
    private readonly Dictionary<Guid, Device> concreteDevices = new();

    public App()
    {
        this.InitializeComponent();
    }

    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        InitializeApplication();
        
        m_window = new MainWindow();
        m_window.Activate();
    }

    private void InitializeApplication()
    {
        EventManager.Instance.SubscribeToEffectActivationRequests(Instance_EffectChanged);
        EventManager.Instance.SubscribeToSelectedDevicesChanged(Instance_SelectedDevicesChanged);
        EventManager.Instance.SubscribeToInitializeProvidersRequests(InitializeProviders);
        EventManager.Instance.SubscribeToStaticColorChanges(ChangeStaticColor);
        EventManager.Instance.SubscribeToLoadAudioDevicesRequests(LoadAudioDevices);
        EventManager.Instance.SubscribeToTurnOnDevicesRequests(TurnOnDevices);
        EventManager.Instance.SubscribeToTurnOffDevicesRequests(TurnOffDevices);

        var globalLog = GenerateAppLogger();
        globalLog.Information("Initializing RGBMaster.....");

        CreateAndSetSupportedProviders(new List<BaseProvider>() { 
            new YeelightProvider(), 
            new MagicHomeProvider(), 
            new RazerChromaProvider(), 
            new LogitechProvider(), 
            new GameSenseProvider(), 
            new HueProvider() 
        });
        
        CreateAndSetSupportedEffectsExecutors(new List<EffectExecutor>() { 
            new MusicEffectExecutor(), 
            new DominantDisplayColorEffectExecutor(), 
            new CursorColorEffectExecutor(), 
            new StaticColorEffectExecutor(), 
            new GradientEffectExecutor() 
        });
        
        SetUIStateEffects();
    }

    private static Serilog.Core.Logger GenerateAppLogger()
    {
        var path = Path.Combine(
            Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)),
            @"RGBMaster\Logs\RGBMaster.txt"
        );

        var globalLog = new LoggerConfiguration()
            .MinimumLevel.Is(Serilog.Events.LogEventLevel.Debug)
            .Enrich.WithExceptionDetails()
            .WriteTo.File(
                path,
                fileSizeLimitBytes: 2 * 1024 * 1024,
                rollOnFileSizeLimit: true,
                retainedFileCountLimit: 5
            ).CreateLogger();

        Log.Logger = globalLog;
        return globalLog;
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
        Log.Logger.Information("Initializing providers.....");
        await CleanupDevicesAndProviders();

        AppState.Instance.ProvidersLoadingProgress = 0.0;
        AppState.Instance.IsLoadingProviders = true;
        AppState.Instance.CurrentProcessedProvider = null;

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
            AppState.Instance.ProvidersLoadingProgress = ((double)loadedProviders / (double)registeredProviders.Count) * 100.0;
        }

        AppState.Instance.IsLoadingProviders = false;

        if (AppState.Instance.IsEffectRunning)
        {
            EventManager.Instance.RequestEffectActivation(null);
        }
    }

    private async void Instance_EffectChanged(object sender, EffectMetadata e)
    {
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
        // Device management handled in MainWindow
    }

    private async void ChangeStaticColor(object sender, object staticColorEffectProps)
    {
        // Static color change logic - simplified for now
        if (AppState.Instance.IsEffectRunning && AppState.Instance.ActiveEffect?.Type == EffectType.StaticColor)
        {
            if (supportedEffectsExecutors.ContainsKey(AppState.Instance.ActiveEffect.EffectMetadataGuid))
            {
                var selectedEffectExecutor = supportedEffectsExecutors[AppState.Instance.ActiveEffect.EffectMetadataGuid];
                await selectedEffectExecutor.Start();
            }
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

            if ((flowType == AudioCaptureDeviceFlowType.Input && device.ID == defaultInputDevice?.ID) ||
                (flowType == AudioCaptureDeviceFlowType.Output && device.ID == defaultOutputDevice?.ID))
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

    private async void TurnOffDevices(object sender, List<DiscoveredDevice> e)
    {
        foreach (var device in e)
        {
            if (concreteDevices.ContainsKey(device.Device.RgbMasterDeviceGuid))
            {
                var concreteDevice = concreteDevices[device.Device.RgbMasterDeviceGuid];
                if (concreteDevice.DeviceMetadata.IsOperationSupported(OperationType.TurnOff))
                {
                    await concreteDevice.TurnOff();
                }
            }
        }
    }

    private async void TurnOnDevices(object sender, List<DiscoveredDevice> e)
    {
        foreach (var device in e)
        {
            if (concreteDevices.ContainsKey(device.Device.RgbMasterDeviceGuid))
            {
                var concreteDevice = concreteDevices[device.Device.RgbMasterDeviceGuid];
                if (concreteDevice.DeviceMetadata.IsOperationSupported(OperationType.TurnOn))
                {
                    await concreteDevice.TurnOn();
                }
            }
        }
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
}