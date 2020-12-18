using Common;
using GameSense.API;
using GameSense.API.Handlers.ColorDefinitions;
using GameSense.API.Handlers.ColorDefinitions.Static;
using Provider;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace GameSense.Devices.Headset
{
    public class GameSenseHeadsetDevice : Device
    {
        private readonly GSAPI gsAPI;

        public GameSenseHeadsetDevice(GSAPI gsAPI, GameSenseHeadsetDeviceMetadata gameSenseHeadsetDeviceMetadata) : base(gameSenseHeadsetDeviceMetadata)
        {
            this.gsAPI = gsAPI;
        }

        protected override async Task ConnectInternal()
        {
            await gsAPI.BindGameEvent(new GSApiBindEventPayload()
            {
                Game = GameSenseConstants.RGB_MASTER_GAME_NAME,
                Event = GameSenseConstants.RGB_MASTER_SET_COLOR_EVENT_NAME,
                Handlers = new List<API.Handlers.GSApiHandler>()
                {
                    new GSApiColorHandler()
                    {
                        DeviceType = GameSenseConstants.HEADSET_DEVICE_TYPE,
                        Mode = GameSenseConstants.DYNAMIC_COLOR_MODE,
                        Zone = GameSenseConstants.HEADSET_ZONE_EARCUP,
                        ContextFrameKey = GameSenseConstants.DYNAMIC_COLOR_CONTEXT_FRAME_KEY,
                        Rate = new API.Handlers.Rate.GSApiRateDefinition()
                    }
                },
                IconID = GameSenseConstants.RGB_MASTER_ICON_ID
            }).ConfigureAwait(false);
        }

        protected override async Task DisconnectInternal()
        {
            await gsAPI.RemoveGameEvent(new GSApiRemoveGameEventPayload()
            {
                Game = GameSenseConstants.RGB_MASTER_GAME_NAME,
                Event = GameSenseConstants.RGB_MASTER_SET_COLOR_EVENT_NAME
            }).ConfigureAwait(false);
        }

        protected override Task<byte> GetBrightnessPercentageInternal()
        {
            throw new NotImplementedException();
        }

        protected override Task<Color> GetColorInternal()
        {
            throw new NotImplementedException();
        }

        protected override Task SetBrightnessPercentageInternal(byte brightness)
        {
            throw new NotImplementedException();
        }

        protected override async Task SetColorInternal(Color color)
        {
            // GameSenseConstants.DYNAMIC_COLOR_CONTEXT_FRAME_KEY

            var frameObject = new Dictionary<string, object>
            {
                {
                    GameSenseConstants.DYNAMIC_COLOR_CONTEXT_FRAME_KEY,
                    new GSApiColorHandlerStaticColorDefinition()
                    {
                        Red = color.R,
                        Green = color.G,
                        Blue = color.B
                    }
                }
            };

            await gsAPI.SendGameEvent(new GSApiSendGameEventPayload()
            {
                Game = GameSenseConstants.RGB_MASTER_GAME_NAME,
                Event = GameSenseConstants.RGB_MASTER_SET_COLOR_EVENT_NAME,
                Data = new GSApiSendGameEventDataPayload()
                {
                    Frame = frameObject
                }
            }).ConfigureAwait(false);
        }

        protected override Task SetColorSmoothlyInternal(Color color, int relativeSmoothness)
        {
            throw new NotImplementedException();
        }

        protected override Task TurnOffInternal()
        {
            throw new NotImplementedException();
        }

        protected override Task TurnOnInternal()
        {
            throw new NotImplementedException();
        }
    }
}
