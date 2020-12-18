using AppExecutionManager.EventManagement;
using AppExecutionManager.State;
using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Utils;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace RGBMasterUWPApp.Pages.EffectsControls
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DominantDisplayColorEffectControl : Page, INotifyPropertyChanged
    {
        private const string RelativeSmoothnessUserSettingKey = "DominantDisplayColorEffectRelativeSmoothness";
        private const string SyncBrightnessByHSLUserSettingKey = "DominantDisplayColorEffectSyncBrightnessByHSL";

        public event PropertyChangedEventHandler PropertyChanged;

        public DominantDisplayColorEffectMetadataProperties DominantDisplayColorEffectProps => ((DominantDisplayColorEffectMetadata)AppState.Instance.Effects.First(effect => effect.Type == EffectType.DominantColor)).EffectProperties;

        public int RelativeSmoothness
        {
            get
            {
                return DominantDisplayColorEffectProps.RelativeSmoothness;
            }
            set
            {
                DominantDisplayColorEffectProps.RelativeSmoothness = value;
                NotifyPropertyChangedUtils.OnPropertyChanged(PropertyChanged, this);
            }
        }

        public bool SyncBrightnessByHSL
        {
            get
            {
                return DominantDisplayColorEffectProps.SyncBrightnessByHSL;
            }
            set
            {
                DominantDisplayColorEffectProps.SyncBrightnessByHSL = value;
                NotifyPropertyChangedUtils.OnPropertyChanged(PropertyChanged, this);
            }
        }

        public DominantDisplayColorEffectControl()
        {
            EventManager.Instance.SubscribeToAppClosingTriggers(AppClosingTriggered);
            LoadSyncBrightnessByHSL();
            LoadRelativeSmoothness();

            this.InitializeComponent();
        }

        private void LoadRelativeSmoothness()
        {
            EventManager.Instance.LoadUserSetting(RelativeSmoothnessUserSettingKey);
            
            if (AppState.Instance.UserSettingsCache.TryGetValue(RelativeSmoothnessUserSettingKey, out var relativeSmoothness))
            {
                RelativeSmoothness = (int)relativeSmoothness;
            }
            else
            {
                RelativeSmoothness = 0;
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            SaveUserSettingsForPage();
        }

        private void LoadSyncBrightnessByHSL()
        {
            EventManager.Instance.LoadUserSetting(SyncBrightnessByHSLUserSettingKey);

            if (AppState.Instance.UserSettingsCache.TryGetValue(SyncBrightnessByHSLUserSettingKey, out var syncBrightnessByHSL))
            {
                SyncBrightnessByHSL = (bool)syncBrightnessByHSL;
            }
            else
            {
                SyncBrightnessByHSL = true;
            }
        }

        private void AppClosingTriggered(object sender, EventArgs e)
        {
            SaveUserSettingsForPage();
        }

        private void SaveUserSettingsForPage()
        {
            EventManager.Instance.StoreUserSetting(new Tuple<string, object>(SyncBrightnessByHSLUserSettingKey, SyncBrightnessByHSL));
            EventManager.Instance.StoreUserSetting(new Tuple<string, object>(RelativeSmoothnessUserSettingKey, RelativeSmoothness));
        }
    }
}
