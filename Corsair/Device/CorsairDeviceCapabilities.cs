namespace Corsair.Device
{
    /// <summary>
    /// Contains list of device capabilities. Current version of SDK only supports lighting and property lookup, but future versions may also support other capabilities.
    /// </summary>
    public enum CorsairDeviceCapabilities
    {
        /// <summary>
        /// For devices that do not support any SDK functions
        /// </summary>
        None,

        /// <summary>
        /// For devices that has controlled lighting
        /// </summary>
        Lighting,

        /// <summary>
        /// For devices that provide current state through set of properties. These properties could be read with CorsairGetPropertyValue function.
        /// </summary>
        PropertyLookup
    }
}
