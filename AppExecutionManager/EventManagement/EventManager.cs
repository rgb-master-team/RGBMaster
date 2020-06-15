using Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppExecutionManager.EventManagement
{
    public class EventManager
    {
        private static readonly EventManager instance;

        private event EventHandler<EffectMetadata> EffectChanged;
        private event EventHandler<List<Device>> SelectedDevicesChanged;
        private event EventHandler StartSyncingRequested;
        private event EventHandler StopSyncingRequested;

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

        public void SubscribeToSelectedDevicesChanged(EventHandler<List<Device>> callback)
        {
            SelectedDevicesChanged += callback;
        }

        public void UnsubscribeFromSelectedDevicesChanged(EventHandler<List<Device>> callback)
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

        public void UpdateEffect(EffectMetadata newEffect)
        {
            EffectChanged?.Invoke(this, newEffect);
        }

        public void UpdateSelectedDevices(List<Device> newSelectedDevices)
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
    }
}
