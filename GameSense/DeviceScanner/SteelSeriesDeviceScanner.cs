using Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace GameSense.DeviceScanner
{
    public class ScannedSteelSeriesDeviceMapping
    {
        public string GameSenseDeviceType { get; }
        public IEnumerable<string> GameSenseZones { get; }
        public DeviceType DeviceType { get; }
        public string DeviceName { get; }

        public ScannedSteelSeriesDeviceMapping(string gameSenseDeviceType, IEnumerable<string> gameSenseZones, DeviceType deviceType, string deviceName)
        {
            GameSenseDeviceType = gameSenseDeviceType;
            GameSenseZones = new List<string>(gameSenseZones);
            DeviceType = deviceType;
            DeviceName = deviceName;
        }
    }

    public static class SteelSeriesDeviceScanner
    {
        private const int VENDOR_ID = 0x1038;

        private static readonly ReadOnlyDictionary<int, ScannedSteelSeriesDeviceMapping> hidProductIdToGsDeviceProps = new ReadOnlyDictionary<int, ScannedSteelSeriesDeviceMapping>(new Dictionary<int, ScannedSteelSeriesDeviceMapping>()
        {
            // Mice
            {
                0x1836, new ScannedSteelSeriesDeviceMapping(
                gameSenseDeviceType: GameSenseConstants.RGB_3_ZONE,
                gameSenseZones: GameSenseConstants.MOUSE_POSSIBLE_ZONES,
                deviceType: DeviceType.Mouse,
                deviceName: "Aerox 3")
            },

            // Headsets
            { 
                0x1283, new ScannedSteelSeriesDeviceMapping(
                gameSenseDeviceType: GameSenseConstants.RGB_2_ZONE, 
                gameSenseZones: GameSenseConstants.HEADSET_POSSIBLE_ZONES,
                deviceType: DeviceType.Headset, 
                deviceName: "SteelSeries Arctis Pro + Game DAC") 
            }
        });

        public static IEnumerable<ScannedSteelSeriesDeviceMapping> ScanGameSenseDevices()
        {
            var hidDevices = Provider.HID.HIDDeviceScannerUtil.ScanDevicesForVendor(VENDOR_ID);

            var gameSenseDevicesProps = new List<ScannedSteelSeriesDeviceMapping>();

            foreach (var hidDevice in hidDevices)
            {
                if (hidProductIdToGsDeviceProps.TryGetValue(hidDevice.ProductId, out var gameSenseProps))
                {
                    gameSenseDevicesProps.Add(gameSenseProps);
                }
            }

            return gameSenseDevicesProps;
        }
    }
}
