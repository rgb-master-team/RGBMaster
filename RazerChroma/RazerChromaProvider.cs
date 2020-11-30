using Colore;
using Common;
using Provider;
using RazerChroma.Devices.AllDevices;
using RazerChroma.Devices.Keyboard;
using RazerChroma.Devices.Mousepad;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RazerChroma
{
    public class RazerChromaProvider : BaseProvider
    {
        private IChroma internalChromaProvider;
        private static int VENDOR_ID = 0x1532;

        private readonly ReadOnlyCollection<Guid> supportedChromaDeviceGuids = new System.Collections.ObjectModel.ReadOnlyCollection<Guid>(new List<Guid>()
        {
            Colore.Data.Devices.Blackwidow,
            Colore.Data.Devices.BlackwidowTe,
            Colore.Data.Devices.BlackwidowX,
            Colore.Data.Devices.BlackwidowXTe,
            Colore.Data.Devices.Blade14,
            Colore.Data.Devices.BladeStealth,
            Colore.Data.Devices.Core,
            Colore.Data.Devices.Deathadder,
            Colore.Data.Devices.Deathstalker,
            Colore.Data.Devices.Diamondback,
            Colore.Data.Devices.Firefly,
            Colore.Data.Devices.Kraken71,
            Colore.Data.Devices.LenovoY27,
            Colore.Data.Devices.LenovoY900,
            Colore.Data.Devices.Mamba,
            Colore.Data.Devices.MambaTe,
            Colore.Data.Devices.Naga,
            Colore.Data.Devices.NagaEpic,
            Colore.Data.Devices.NagaHex,
            Colore.Data.Devices.Orbweaver,
            Colore.Data.Devices.Ornata,
            Colore.Data.Devices.Orochi,
            Colore.Data.Devices.OverwatchKeyboard,
            Colore.Data.Devices.Tartarus,
        });

        public RazerChromaProvider(): base(new RazerChromaProviderMetadata())
        {

        }

        protected override /*async*/ Task<List<Device>> InternalDiscover(CancellationToken cancellationToken = default)
        {
            /*var discoveredDevices = new List<Device>() { new RazerChromaAllDevicesDevice(internalChromaProvider) };

            foreach (var deviceGuid in supportedChromaDeviceGuids)
            {
                var deviceInfo = await internalChromaProvider.QueryAsync(deviceGuid);

                if (deviceInfo.Connected)
                {
                    Device discoveredDevice = null;

                    switch (deviceInfo.Type)
                    {
                        case Colore.Data.DeviceType.Keyboard:
                            discoveredDevice = new RazerChromaKeyboardDevice(internalChromaProvider, new RazerChromaKeyboardDeviceMetadata(deviceInfo.Name, new HashSet<OperationType>() { OperationType.SetColor }));
                            break;
                        case Colore.Data.DeviceType.Mousepad:
                            discoveredDevice = new RazerChromaMousepadDevice(internalChromaProvider, new RazerChromaMousepadDeviceMetadata(deviceInfo.Name, new HashSet<OperationType>() { OperationType.SetColor }));
                            break;
                        case Colore.Data.DeviceType.Mouse:
                            break;
                        case Colore.Data.DeviceType.Headset:
                            break;
                        case Colore.Data.DeviceType.Keypad:
                            break;
                        case Colore.Data.DeviceType.System:
                            break;
                        case Colore.Data.DeviceType.Speakers:
                            break;
                        case Colore.Data.DeviceType.Invalid:
                            break;
                        default:
                            break;
                    }

                    if (discoveredDevice != null)
                    {
                        discoveredDevices.Add(discoveredDevice);
                    }
                }
            }

            return discoveredDevices;*/

            return Task.FromResult(new List<Device> { new RazerChromaAllDevicesDevice(internalChromaProvider, new RazerChromaAllDevicesDeviceMetadata(ProviderMetadata.ProviderGuid)), new RazerChromaKeyboardDevice(internalChromaProvider, new RazerChromaKeyboardDeviceMetadata(ProviderMetadata.ProviderGuid, "Razer Keyboard", new HashSet<OperationType>() { OperationType.SetColor })), new RazerChromaMousepadDevice(internalChromaProvider, new RazerChromaMousepadDeviceMetadata(ProviderMetadata.ProviderGuid, "Razer Mousepad", new HashSet<OperationType>() { OperationType.SetColor})) });
        }

        protected async override Task InternalRegister(CancellationToken cancellationToken = default)
        {
            internalChromaProvider = await ColoreProvider.CreateNativeAsync();
            await internalChromaProvider.InitializeAsync(new Colore.Data.AppInfo("RGBMaster", "Syncs your brothers and sisters into the light", "RGBMasters", "RGBMaster@github", Colore.Data.Category.Application));
        }

        protected async override Task InternalUnregister(CancellationToken cancellationToken = default)
        {
            internalChromaProvider.Unregister();
            await internalChromaProvider.UninitializeAsync();
        }
    }
}
