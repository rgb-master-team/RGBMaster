namespace Msi.Enums
{
	public enum MLStatusCodes
	{
		/// <summary>
		/// Request is completed.
		/// </summary>
		Ok = 0,

		/// <summary>
		/// Generic error.
		/// </summary>
		Error = -1,

		/// <summary>
		/// Request is timeout.
		/// </summary>
		Timeout = -2,

		/// <summary>
		/// MSI application not found or installed version not supported.
		/// </summary>
		NoImplemented = -3,

		/// <summary>
		/// Initialize has not been called successful.
		/// </summary>
		NotInitialized = -4,

		/// <summary>
		/// The parameter value is not valid.
		/// </summary>
		InvalidArgument = -101,

		/// <summary>
		///  The device is not found.
		/// </summary>
		DeviceNotFound = -102,

		/// <summary>
		/// Requested feature is not supported in the selected LED.
		/// </summary>
		NotSupported = -103
	}
}
