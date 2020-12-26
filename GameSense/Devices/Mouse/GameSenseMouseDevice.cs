using GameSense.API;
using GameSense.API.Handlers.ColorDefinitions;
using GameSense.API.Handlers.ColorDefinitions.Static;
using Provider;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace GameSense.Devices.Mouse
{
    public class GameSenseMouseDevice : Device
    {
        private readonly GSAPI gsAPI;

        public GameSenseMouseDevice(GSAPI gsAPI, GameSenseMouseDeviceMetadata gameSenseMouseDeviceMetadata) : base(gameSenseMouseDeviceMetadata)
        {
            this.gsAPI = gsAPI;
        }
        protected override async Task ConnectInternal()
        {
            await RunForAllPossibleDevTypesAndZones(async (gameSenseDeviceType, gameSenseZone) =>
            {
                await gsAPI.BindGameEvent(new GSApiBindEventPayload()
                {
                    Game = GameSenseConstants.RGB_MASTER_GAME_NAME,
                    Event = $"{GameSenseConstants.RGB_MASTER_SET_COLOR_EVENT_NAME}_{gameSenseDeviceType}_{gameSenseZone}",
                    Handlers = new List<API.Handlers.GSApiHandler>()
                        {
                            new GSApiColorHandler()
                            {
                                DeviceType = gameSenseDeviceType,
                                Zone = gameSenseZone,
                                Mode = GameSenseConstants.DYNAMIC_COLOR_MODE,
                                ContextFrameKey = GameSenseConstants.DYNAMIC_COLOR_CONTEXT_FRAME_KEY,
                                Rate = new API.Handlers.Rate.GSApiRateDefinition()
                            }
                        },
                    IconID = GameSenseConstants.RGB_MASTER_ICON_ID
                }).ConfigureAwait(false);
            });
        }

        protected override async Task DisconnectInternal()
        {
            await RunForAllPossibleDevTypesAndZones(async (gameSenseDeviceType, gameSenseZone) =>
            {
                await gsAPI.RemoveGameEvent(new GSApiRemoveGameEventPayload()
                {
                    Game = GameSenseConstants.RGB_MASTER_GAME_NAME,
                    Event = $"{GameSenseConstants.RGB_MASTER_SET_COLOR_EVENT_NAME}_{gameSenseDeviceType}_{gameSenseZone}",
                }).ConfigureAwait(false);
            });
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

            await RunForAllPossibleDevTypesAndZones(async (gameSenseDeviceType, gameSenseZone) =>
            {
                await gsAPI.SendGameEvent(new GSApiSendGameEventPayload()
                {
                    Game = GameSenseConstants.RGB_MASTER_GAME_NAME,
                    Event = $"{GameSenseConstants.RGB_MASTER_SET_COLOR_EVENT_NAME}_{gameSenseDeviceType}_{gameSenseZone}",
                    Data = new GSApiSendGameEventDataPayload()
                    {
                        Frame = frameObject
                    }
                }).ConfigureAwait(false);
            });
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

        // ONLY TEMPORARY UNTIL WE START POPULATING DICTIONARIES THAT MATCH DEVICE HIDS WITH RELEVANT ZONES AND DEVICE TYPES!
        private async Task RunForAllPossibleDevTypesAndZones(Func<string, string, Task> asyncFunction)
        {
            var tasks = new List<Task>();

            foreach (var gameSenseDeviceType in GameSenseConstants.MOUSE_POSSIBLE_DEVICE_TYPES)
            {
                foreach (var gameSenseZone in GameSenseConstants.MOUSE_POSSIBLE_ZONES)
                {
                    tasks.Add(Task.Run(async() => await asyncFunction(gameSenseDeviceType, gameSenseZone)));
                }
            }

            await Task.WhenAll(tasks);
        }
    }
}
