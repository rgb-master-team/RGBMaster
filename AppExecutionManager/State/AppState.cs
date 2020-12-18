using Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace AppExecutionManager.State
{
    public class AppState : INotifyPropertyChanged
    {
        private static readonly AppState instance = new AppState();

        private bool isLoadingProviders;
        private double providersLoadingProgress;
        private ProviderMetadata currentProcessedProvider;
        private EffectMetadata activeEffect;
        private List<AudioCaptureDevice> audioCaptureDevices;
        private Dictionary<string, object> userSettingsCache;

        public event PropertyChangedEventHandler PropertyChanged;

        static AppState()
        {
            instance = new AppState()
            {
                RegisteredProviders = new ObservableCollection<RegisteredProvider>(),
                Effects = new ObservableCollection<EffectMetadata>(),
                SupportedProviders = new ObservableCollection<ProviderMetadata>(),
                IsLoadingProviders = false,
                ProvidersLoadingProgress = 0.0,
                AudioCaptureDevices = null,
                UserSettingsCache = new Dictionary<string, object>()
            };

        }
        private AppState()
        {
        }
        public static AppState Instance
        {
            get
            {
                return instance;
            }
        }
        public ObservableCollection<RegisteredProvider> RegisteredProviders { get; set; }
        public EffectMetadata ActiveEffect 
        {
            get
            {
                return activeEffect;
            }
            set
            {
                activeEffect = value;
                NotifyPropertyChangedUtils.OnPropertyChanged(PropertyChanged, this);
                NotifyPropertyChangedUtils.OnPropertyChanged(PropertyChanged, this, nameof(IsEffectRunning));
            }
        }
        public ObservableCollection<EffectMetadata> Effects { get; set; }
        public string AppVersion { get; set; }
        public ObservableCollection<ProviderMetadata> SupportedProviders { get; set; }
        public bool IsEffectRunning => ActiveEffect != null;
        public bool IsLoadingProviders
        {
            get
            {
                return isLoadingProviders;
            }
            set
            {
                isLoadingProviders = value;
                NotifyPropertyChangedUtils.OnPropertyChanged(PropertyChanged, this);
            }
        }

        public double ProvidersLoadingProgress
        {
            get
            {
                return providersLoadingProgress;
            }
            set
            {
                providersLoadingProgress = value;
                NotifyPropertyChangedUtils.OnPropertyChanged(PropertyChanged, this);
            }
        }

        public ProviderMetadata CurrentProcessedProvider 
        {
            get
            {
                return currentProcessedProvider;
            }
            set
            {
                currentProcessedProvider = value;
                NotifyPropertyChangedUtils.OnPropertyChanged(PropertyChanged, this);
            }
        }

        public List<AudioCaptureDevice> AudioCaptureDevices 
        { 
            get
            {
                return audioCaptureDevices;
            }
            set
            {
                audioCaptureDevices = value;
                NotifyPropertyChangedUtils.OnPropertyChanged(PropertyChanged, this);
            }
        }

        // TODO - MAKE THIS A FUCKING ICONFIGURATION OBJECT
        public Dictionary<string, object> UserSettingsCache
        {
            get
            {
                return userSettingsCache;
            }
            set
            {
                userSettingsCache = value;
                NotifyPropertyChangedUtils.OnPropertyChanged(PropertyChanged, this);
            }
        }
    }
}
