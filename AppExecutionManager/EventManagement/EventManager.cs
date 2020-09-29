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

        private event EventHandler<StaticColorEffectProps> StaticColorChanged;
        private event EventHandler<EffectMetadata> NewEffectActivationRequested;
        private event EventHandler<List<DiscoveredDevice>> SelectedDevicesChanged;
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
