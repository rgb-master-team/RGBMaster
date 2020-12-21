using AppExecutionManager.State;
using Common;
using System;
using System.Collections.Generic;

namespace AppExecutionManager.EventManagement
{
    public class EventManager
    {
        private static readonly EventManager instance = new EventManager();

        private event EventHandler<StaticColorEffectProps> StaticColorChanged;
        private event EventHandler<EffectMetadata> NewEffectActivationRequested;
        private event EventHandler<List<DiscoveredDevice>> SelectedDevicesChanged;
        private event EventHandler InitializeProvidersRequested;
        private event EventHandler LoadAudioDevicesRequested;
        private event EventHandler<List<DiscoveredDevice>> TurnOnDevicesRequested;
        private event EventHandler<List<DiscoveredDevice>> TurnOffDevicesRequested;
        private event EventHandler<string> LoadUserSettingRequested;
        private event EventHandler<Tuple<string, object>> StoreUserSettingRequested;
        private event EventHandler<IEnumerable<string>> ResetUserSettingsToDefaultRequested;
        private event EventHandler AppClosingTriggered;

        static EventManager()
        {

        }
        private EventManager()
        {
        }
        public static EventManager Instance
        {
            get
            {
                return instance;
            }
        }

        public void SubscribeToEffectActivationRequests(EventHandler<EffectMetadata> callback)
        {
            NewEffectActivationRequested += callback;
        }

        public void UnsubscribeFromEffectActivationRequests(EventHandler<EffectMetadata> callback)
        {
            NewEffectActivationRequested -= callback;
        }

        public void SubscribeToSelectedDevicesChanged(EventHandler<List<DiscoveredDevice>> callback)
        {
            SelectedDevicesChanged += callback;
        }

        public void UnsubscribeFromSelectedDevicesChanged(EventHandler<List<DiscoveredDevice>> callback)
        {
            SelectedDevicesChanged -= callback;
        }

        public void RequestEffectActivation(EffectMetadata newEffect)
        {
            NewEffectActivationRequested?.Invoke(this, newEffect);
        }

        public void UpdateSelectedDevices(List<DiscoveredDevice> newSelectedDevices)
        {
            SelectedDevicesChanged?.Invoke(this, newSelectedDevices);
        }

        public void SubscribeToInitializeProvidersRequests(EventHandler callback)
        {
            InitializeProvidersRequested += callback;
        }

        public void UnsubscribeFromInitializeProvidersRequests(EventHandler callback)
        {
            InitializeProvidersRequested -= callback;
        }

        public void InitializeProviders()
        {
            InitializeProvidersRequested?.Invoke(this, null);
        }

        public void SubscribeToStaticColorChanges(EventHandler<StaticColorEffectProps> callback)
        {
            StaticColorChanged += callback;
        }

        public void UnsubscribeFromStaticColorChanges(EventHandler<StaticColorEffectProps> callback)
        {
            StaticColorChanged -= callback;
        }

        public void ChangeStaticColor(StaticColorEffectProps color)
        {
            StaticColorChanged?.Invoke(this, color);
        }
        public void SubscribeToLoadAudioDevicesRequests(EventHandler callback)
        {
            LoadAudioDevicesRequested += callback;
        }
        public void UnubscribeFromLoadAudioDevicesRequests(EventHandler callback)
        {
            LoadAudioDevicesRequested -= callback;
        }

        public void LoadAudioDevices()
        {
            LoadAudioDevicesRequested?.Invoke(this, null);
        }
        public void SubscribeToTurnOnDevicesRequests(EventHandler<List<DiscoveredDevice>> callback)
        {
            TurnOnDevicesRequested += callback;
        }
        public void UnsubscribeFromTurnOnDevicesRequests(EventHandler<List<DiscoveredDevice>> callback)
        {
            TurnOnDevicesRequested -= callback;
        }
        public void TurnOnDevices(List<DiscoveredDevice> devices)
        {
            TurnOnDevicesRequested?.Invoke(this, devices);
        }
        public void SubscribeToTurnOffDevicesRequests(EventHandler<List<DiscoveredDevice>> callback)
        {
            TurnOffDevicesRequested += callback;
        }
        public void UnsubscribeFromTurnOffDevicesRequests(EventHandler<List<DiscoveredDevice>> callback)
        {
            TurnOffDevicesRequested -= callback;
        }
        public void TurnOffDevices(List<DiscoveredDevice> devices)
        {
            TurnOffDevicesRequested?.Invoke(this, devices);
        }

        public void SubscribeToLoadUserSettingRequests(EventHandler<string> callback)
        {
            LoadUserSettingRequested += callback;
        }
        public void UnsubscribeFromLoadUserSettingRequests(EventHandler<string> callback)
        {
            LoadUserSettingRequested -= callback;
        }
        public void LoadUserSetting(string keyAndValue)
        {
            LoadUserSettingRequested?.Invoke(this, keyAndValue);
        }

        public void SubscribeToStoreUserSettingRequests(EventHandler<Tuple<string, object>> callback)
        {
            StoreUserSettingRequested += callback;
        }
        public void UnsubscribeFromStoreUserSettingRequests(EventHandler<Tuple<string, object>> callback)
        {
            StoreUserSettingRequested -= callback;
        }
        public void StoreUserSetting(Tuple<string, object> keyAndValue)
        {
            StoreUserSettingRequested?.Invoke(this, keyAndValue);
        }

        public void SubscribeToResetUserSettingsToDefaultRequests(EventHandler<IEnumerable<string>> callback)
        {
            ResetUserSettingsToDefaultRequested += callback;
        }
        public void UnsubscribeFromResetUserSettingsToDefaultRequests(EventHandler<IEnumerable<string>> callback)
        {
            ResetUserSettingsToDefaultRequested -= callback;
        }
        public void ResetUserSettingsToDefault(IEnumerable<string> keys)
        {
            ResetUserSettingsToDefaultRequested?.Invoke(this, keys);
        }

        public void SubscribeToAppClosingTriggers(EventHandler callback)
        {
            AppClosingTriggered += callback;
        }
        public void UnsubscribeFromAppClosingTriggers(EventHandler callback)
        {
            AppClosingTriggered -= callback;
        }
        public void InformAppClosing()
        {
            AppClosingTriggered?.Invoke(this, null);
        }
    }
}
