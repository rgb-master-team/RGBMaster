namespace Corsair.Channel
{
    /// <summary>
    /// Contains list of the LED-devices which can be connected to the DIY-device or cooler.
    /// </summary>
    public enum CorsairChannelDeviceType
    {
        /// <summary>
        /// Dummy value
        /// </summary>
        Invalid,

        /// <summary>
        /// For a HD fan
        /// </summary>
        HD_Fan,

        /// <summary>
        /// For a SP fan
        /// </summary>
        SP_Fan,

        /// <summary>
        /// For a LL fan
        /// </summary>
        LL_Fan,

        /// <summary>
        /// For a ML fan
        /// </summary>
        ML_Fan,

        /// <summary>
        /// For a light strip
        /// </summary>
        Strip,

        /// <summary>
        /// For a DAP
        /// </summary>
        DAP,

        /// <summary>
        /// For a pump
        /// </summary>
        Pump
    }
}
