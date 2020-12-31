using HidLibrary;

namespace Provider.HID
{
    public class RGBMasterHidDevice
    {
        private readonly IHidDevice hidDevice;

        public RGBMasterHidDevice(IHidDevice hidDevice)
        {
            this.hidDevice = hidDevice;
        }

        public int VendorId => hidDevice.Attributes.VendorId;
        public int ProductId => hidDevice.Attributes.ProductId;
    }
}