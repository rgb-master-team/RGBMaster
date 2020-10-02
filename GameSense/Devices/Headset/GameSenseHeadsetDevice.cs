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

        protected override Task ConnectInternal()
        {
            gsAPI.BindGameEvent(new GSApiBindEventPayload()
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
            });

            return Task.CompletedTask;
        }

        protected override Task DisconnectInternal()
        {
            gsAPI.RemoveGameEvent(new GSApiRemoveGameEventPayload()
            {
                Game = GameSenseConstants.RGB_MASTER_GAME_NAME,
                Event = GameSenseConstants.RGB_MASTER_SET_COLOR_EVENT_NAME
            });

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

            gsAPI.SendGameEvent(new GSApiSendGameEventPayload()
            {
                Game = GameSenseConstants.RGB_MASTER_GAME_NAME,
                Event = GameSenseConstants.RGB_MASTER_SET_COLOR_EVENT_NAME,
                Data = new GSApiSendGameEventDataPayload()
                {
                    Frame = frameObject
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
