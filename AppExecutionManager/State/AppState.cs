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

namespace AppExecutionManager.State
{
    public class AppState : INotifyPropertyChanged
    {
        private static readonly AppState instance = new AppState();

        private bool isLoadingProviders = false;
        private double providersLoadingProgress = 0.0;
        private ProviderMetadata currentProcessedProvider;
        private EffectMetadata activeEffect;

        public event PropertyChangedEventHandler PropertyChanged;

        static AppState()
        {
            instance = new AppState()
            {
                RegisteredProviders = new ObservableCollection<RegisteredProvider>(),
                Effects = new ObservableCollection<EffectMetadata>(),
                StaticColorEffectProperties=new StaticColorEffectProps() { SelectedColor = Color.White, SelectedBrightness = 100 },
                SupportedProviders = new ObservableCollection<ProviderMetadata>()
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
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsEffectRunning));
            }
        }
        public ObservableCollection<EffectMetadata> Effects { get; set; }
        public StaticColorEffectProps StaticColorEffectProperties { get; set; }
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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
