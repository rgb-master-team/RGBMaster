using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Common;
using Corsair.Channel;
using Corsair.Layout;
using Corsair.Led;
using Corsair.Provider;
using RGBDevice = Provider.Device;

namespace Corsair.Device
{
    /// <summary>
    /// Contains information about device.
    /// </summary>
    public class CorsairDevice : RGBDevice
	{
		#region Corsair Native

		internal CorsairDeviceNative Native;

		/// <summary>
		/// The Id of the device which represents him in the cue driver
		/// </summary>
		public int Id { get; }

		/// <summary>
		/// Enum describing device type
		/// </summary>
		public CorsairDeviceType Type => Native.type;

		/// <summary>
		/// Null-terminated device model (like “K95RGB”)
		/// </summary>
		public string Model { get; }

		/// <summary>
		/// Enum describing physical layout of the keyboard or mouse. If device is neither keyboard nor mouse then value is CPL_Invalid
		/// </summary>
		public CorsairPhysicalLayout PhysicalLayout => Native.physicalLayout;

		/// <summary>
		/// Enum describing logical layout of the keyboard as set in CUE settings. If device is not keyboard then value is CLL_Invalid
		/// </summary>
		public CorsairLogicalLayout LogicalLayout => Native.logicalLayout;

		/// <summary>
		/// Mask that describes device capabilities, formed as logical “or” of CorsairDeviceCaps enum values
		/// </summary>
		public int CapsMask => Native.capsMask;

		/// <summary>
		/// Structure that describes channels of the DIY-devices and coolers.
		/// </summary>
		public CorsairChannels Channels { get; }

		/// <summary>
		/// Contains number of leds and array with their positions of all the devices.
		/// </summary>
		public CorsairLedPositions LedPositions { get; private set; } 
		#endregion

		/// <summary>
		/// Creates a instance of CorsairDevice
		/// </summary>
		/// <param name="deviceNative">The native device info</param>
		public CorsairDevice(CorsairDeviceNative deviceNative, int id, CorsairDeviceMetadata corsairDeviceMetadata) : base(corsairDeviceMetadata)
		{
			Native = deviceNative;
			Id = id;

			Model = Marshal.PtrToStringAnsi(Native.model);
			Channels = new CorsairChannels(Native.channels);
	}

		/// <summary>
		/// Load the leds details from the cue sdk
		/// </summary>
		private void Load()
		{
			LedPositions = CUESDK.CUESDK.GetLedPositionsByDeviceIndex(Id);

			Channels.Load(LedPositions.LedPosition);
		}

		#region Device

		protected override Task<Color> GetColorInternal()
		{
			throw new NotImplementedException();
		}

		protected override Task<byte> GetBrightnessPercentageInternal()
		{
			throw new NotImplementedException();
		}

		protected override Task SetBrightnessPercentageInternal(byte brightness)
		{
			throw new NotImplementedException();
		}

		protected override Task ConnectInternal()
		{
			Load();
			return Task.CompletedTask;
		}

		protected override Task DisconnectInternal()
		{
			return Task.CompletedTask;
		}

		protected override Task SetColorInternal(Color color)
		{
			var ledsColor = LedPositions.LedPosition.Select(x => new CorsairLedColor
			{
				LedId = x.LedId,
				Blue = color.B,
				Green = color.G,
				Red = color.R
			}).ToArray();

			CUESDK.CUESDK.SetLedsColorsBufferByDeviceIndex(Id, LedPositions.LedPosition.Length, ledsColor);
			CUESDK.CUESDK.SetLedsColorsFlushBuffer();

			return Task.CompletedTask;
		}

		protected override Task TurnOnInternal()
		{
			throw new NotImplementedException();
		}

		protected override Task TurnOffInternal()
		{
			throw new NotImplementedException();
		}

        protected override Task SetGradientInternal(GradientPoint gradientPoint, int relativeSmoothness)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}