using Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExecutionManager.State
{
    public class AppState
    {
        private static readonly AppState instance = new AppState();

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
        public EffectMetadata ActiveEffect { get; set; }
        public ObservableCollection<EffectMetadata> Effects { get; set; }
        public StaticColorEffectProps StaticColorEffectProperties { get; set; }
        public string AppVersion { get; set; }
        public ObservableCollection<ProviderMetadata> SupportedProviders { get; set; }
        public bool IsEffectRunning => ActiveEffect != null;
    }
}
