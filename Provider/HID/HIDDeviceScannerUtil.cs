using HidLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Provider.HID
{
    // TODO - Consider moving this to a utils class? 
    // (That only providers are going to know anyway, 
    // so not sure if it's worth the hassle, or if it's even better)
    public static class HIDDeviceScannerUtil
    {
        public static IEnumerable<RGBMasterHidDevice> ScanDevicesForVendor(int vendorId)
        {
            var enumerator = new HidLibrary.HidEnumerator();
            return enumerator.Enumerate(vendorId).Select(hidDevice => new RGBMasterHidDevice(hidDevice));
        }
    }
}
