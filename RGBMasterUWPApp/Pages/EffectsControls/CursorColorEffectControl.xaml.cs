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
    public sealed partial class CursorColorEffectControl : Page, INotifyPropertyChanged
    {
        private const string RelativeSmoothnessUserSettingKey = "CursorColorEffectRelativeSmoothness";
        private const string SyncBrightnessByHSLUserSettingKey = "CursorColorEffectSyncBrightnessByHSL";

        public event PropertyChangedEventHandler PropertyChanged;

        public CursorColorEffectMetadataProperties CursorColorEffectProps => ((CursorColorEffectMetadata)AppState.Instance.Effects.First(effect => effect.Type == EffectType.CursorColor)).EffectProperties;

        public int RelativeSmoothness
        {
            get
            {
                return CursorColorEffectProps.RelativeSmoothness;
            }
            set
            {
                CursorColorEffectProps.RelativeSmoothness = value;
                NotifyPropertyChangedUtils.OnPropertyChanged(PropertyChanged, this);
            }
        }

        public bool SyncBrightnessByHSL
        {
            get
            {
                return CursorColorEffectProps.SyncBrightnessByHSL;
            }
            set
            {
                CursorColorEffectProps.SyncBrightnessByHSL = value;
                NotifyPropertyChangedUtils.OnPropertyChanged(PropertyChanged, this);
            }
        }

        public CursorColorEffectControl()
        {
            EventManager.Instance.SubscribeToAppClosingTriggers(AppClosingTriggered);

            LoadRelativeSmoothness();
            LoadSyncBrightnessByHSL();

            this.InitializeComponent();
        }

        private void LoadSyncBrightnessByHSL()
        {
            EventManager.Instance.LoadUserSetting(SyncBrightnessByHSLUserSettingKey);

            if (AppState.Instance.UserSettingsCache.TryGetValue(SyncBrightnessByHSLUserSettingKey, out var syncBrightnessByHsl))
            {
                SyncBrightnessByHSL = (bool)syncBrightnessByHsl;
            }
            else
            {
                SyncBrightnessByHSL = true;
            }
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

        private void AppClosingTriggered(object sender, EventArgs e)
        {
            SaveUserSettingsForPage();
        }

        private void SaveUserSettingsForPage()
        {
            EventManager.Instance.StoreUserSetting(new Tuple<string, object>(RelativeSmoothnessUserSettingKey, RelativeSmoothness));
            EventManager.Instance.StoreUserSetting(new Tuple<string, object>(SyncBrightnessByHSLUserSettingKey, SyncBrightnessByHSL));
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            SaveUserSettingsForPage();
        }
    }
}
