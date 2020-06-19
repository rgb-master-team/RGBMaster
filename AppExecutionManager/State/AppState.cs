using Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBMasterUWPApp.State
{
    public class AppState
    {
        private static readonly AppState instance = new AppState();

        static AppState()
        {
            instance = new AppState()
            {
                RegisteredProviders = new ObservableCollection<RegisteredProvider>(),
                IsEffectRunning = false,
                //SelectedDevices = new ObservableCollection<DiscoveredDevice>(),
                AreAllLightsOn = false,
                Effects = new ObservableCollection<EffectMetadata>()
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
        //public ObservableCollection<DiscoveredDevice> SelectedDevices { get; set; }
        public EffectMetadata SelectedEffect { get; set; }
        public ObservableCollection<EffectMetadata> Effects { get; set; }
        public bool IsEffectRunning { get; set; }
        public System.Drawing.Color StaticColor { get; set; }
        public bool AreAllLightsOn { get; set; }
        public string AppVersion { get; set; }
    }
}
