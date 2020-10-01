using Common;
using GameSense.API;
using GameSense.API.Handlers.ColorDefinitions;
using GameSense.API.Handlers.ColorDefinitions.Static;
using Provider;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace GameSense.Devices.Headset
{
    public class GameSenseHeadsetDevice : Device
    {
        private readonly GSAPI gsAPI;

        public GameSenseHeadsetDevice(GSAPI gsAPI) : base(new GameSenseHeadsetDeviceMetadata())
        {
            this.gsAPI = gsAPI;
        }

        protected override Task ConnectInternal()
        {
            return Task.CompletedTask;
        }

        protected override Task DisconnectInternal()
        {
            return Task.CompletedTask;
        }

        protected override byte GetBrightnessPercentageInternal()
        {
            throw new NotImplementedException();
        }

        protected override Color GetColorInternal()
        {
            throw new NotImplementedException();
        }

        protected override void SetBrightnessPercentageInternal(byte brightness)
        {
            throw new NotImplementedException();
        }

        protected override void SetColorInternal(Color color)
        {
            gsAPI.BindGameEvent(new GSApiBindEventPayload()
            {
                Game = "RGBMaster",
                Event = "SET_COLOR",
                Handlers = new List<API.Handlers.GSApiHandler>()
                {
                    new GSApiColorHandler()
                    {
                        Color = new GSApiColorHandlerStaticColorDefinition()
                        {
                            Red = color.R,
                            Green = color.G,
                            Blue = color.B
                        },
                        DeviceType = "headset",
                        Mode = "color",
                        Zone = "earcups"
                    }
                }
            });
        }

        protected override void TurnOffInternal()
        {
            throw new NotImplementedException();
        }

        protected override void TurnOnInternal()
        {
            throw new NotImplementedException();
        }
    }
}
