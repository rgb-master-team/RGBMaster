using AppExecutionManager.State;
using Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace AppExecutionManager.EventManagement
{
    public class EventManager
    {
        private static readonly EventManager instance = new EventManager();

        private event EventHandler<Color> StaticColorChanged;
        private event EventHandler<EffectMetadata> EffectChanged;
        private event EventHandler<List<DiscoveredDevice>> SelectedDevicesChanged;
        private event EventHandler StartSyncingRequested;
        private event EventHandler StopSyncingRequested;
        private event EventHandler InitializeProvidersRequested;
        private event EventHandler TurnOnAllLightsRequested;

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

        public void SubscribeToEffectChanged(EventHandler<EffectMetadata> callback)
        {
            EffectChanged += callback;
        }

        public void UnsubscribeFromEffectChanged(EventHandler<EffectMetadata> callback)
        {
            EffectChanged -= callback;
        }

        public void SubscribeToSelectedDevicesChanged(EventHandler<List<DiscoveredDevice>> callback)
        {
            SelectedDevicesChanged += callback;
        }

        public void UnsubscribeFromSelectedDevicesChanged(EventHandler<List<DiscoveredDevice>> callback)
        {
            SelectedDevicesChanged -= callback;
        }

        public void SubscribeToStartSyncingRequested(EventHandler callback)
        {
            StartSyncingRequested += callback;
        }

        public void UnsubscribeFromStartSyncingRequested(EventHandler callback)
        {
            StartSyncingRequested -= callback;
        }

        public void SubscribeToStopSyncingRequested(EventHandler callback)
        {
            StopSyncingRequested += callback;
        }

        public void UnsubscribeFromStopSyncingRequested(EventHandler callback)
        {
            StopSyncingRequested -= callback;
        }

        public void UpdateEffect(EffectMetadata newEffect)
        {
            EffectChanged?.Invoke(this, newEffect);
        }

        public void UpdateSelectedDevices(List<DiscoveredDevice> newSelectedDevices)
        {
            SelectedDevicesChanged?.Invoke(this, newSelectedDevices);
        }

        public void StartSyncing()
        {
            StartSyncingRequested?.Invoke(this, null);
        }

        public void StopSyncing()
        {
            StopSyncingRequested?.Invoke(this, null);
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

        public void SubscribeToStaticColorChanges(EventHandler<Color> callback)
        {
            StaticColorChanged += callback;
        }

        public void UnsubscribeFromStaticColorChanges(EventHandler<Color> callback)
        {
            StaticColorChanged -= callback;
        }

        public void ChangeStaticColor(Color color)
        {
            StaticColorChanged?.Invoke(this, color);
        }

        public void SubscribeToTurnOnAllLightsRequests(EventHandler callback)
        {
            TurnOnAllLightsRequested += callback;
        }

        public void UnsubscribeFromTurnOnAllLightsRequests(EventHandler callback)
        {
            TurnOnAllLightsRequested -= callback;
        }
        public void TurnOnAllLights()
        {
            TurnOnAllLightsRequested?.Invoke(this, null);
        }
    }
}
