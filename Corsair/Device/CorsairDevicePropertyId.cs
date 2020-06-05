namespace Corsair.Device
{
    /// <summary>
    /// Contains list of properties identifiers which can be read from device that supports CDC_PropertyLookup capability. Each identifier characterized by two values - id and data type. Data type is represented by high nibble and equals 1 for boolean or 2 for integer property values. E.g. Headset_MicEnabled and 0xF000 == CDPT_Boolean, Headset_EqualizerPreset and 0xF000 == CDPT_Int32.
    /// </summary>
    public enum CorsairDevicePropertyId
    {
        /// <summary>
        /// Indicates Mic state (On or Off)
        /// </summary>
        Headset_MicEnabled = 0x1000,

        /// <summary>
        /// Indicates Surround Sound state (On or Off)
        /// </summary>
        Headset_SurroundSoundEnabled = 0x1001,

        /// <summary>
        /// Indicates Sidetone state (On or Off)
        /// </summary>
        Headset_SidetoneEnabled = 0x1002,

        /// <summary>
        /// The number of active equalizer preset (integer, 1 - 5)
        /// </summary>
        Headset_EqualizerPresent = 0x2000
    }
}
