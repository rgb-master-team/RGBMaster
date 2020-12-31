using Common;
using GameSense.API;
using GameSense.API.Handlers.ColorDefinitions;
using GameSense.API.Handlers.ColorDefinitions.Static;
using GameSense.DeviceScanner;
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
        private readonly ScannedSteelSeriesDeviceMapping gameSenseDeviceMapping;
        private readonly GSAPI gsAPI;

        public GameSenseHeadsetDevice(GSAPI gsAPI, ScannedSteelSeriesDeviceMapping gameSenseDeviceMapping, GameSenseHeadsetDeviceMetadata gameSenseHeadsetDeviceMetadata) : base(gameSenseHeadsetDeviceMetadata)
        {
            this.gsAPI = gsAPI;
            this.gameSenseDeviceMapping = gameSenseDeviceMapping;
        }

        // TODO - THIS SERIOUSLY CAN'T STAY HERE LOL
        private string GetFixedEventName(string gameSensebasicEventName, string gameSenseAppliedZone)
        {
            return $"{gameSensebasicEventName}_{gameSenseAppliedZone}_{DeviceMetadata.RgbMasterDeviceGuid}".ToUpper();
        }

        protected override async Task ConnectInternal()
        {
            var gameSenseDeviceType = gameSenseDeviceMapping.GameSenseDeviceType;

            await RunForAllZones(async (zone) =>
            {
                await gsAPI.BindGameEvent(new GSApiBindEventPayload()
                {
                    Game = GameSenseConstants.RGB_MASTER_GAME_NAME,
                    Event = GetFixedEventName(GameSenseConstants.RGB_MASTER_SET_COLOR_EVENT_NAME, zone),
                    Handlers = new List<API.Handlers.GSApiHandler>()
                {
                    new GSApiColorHandler()
                    {
                        DeviceType = gameSenseDeviceMapping.GameSenseDeviceType,
                        Mode = GameSenseConstants.DYNAMIC_COLOR_MODE,
                        Zone = zone,
                        ContextFrameKey = GameSenseConstants.DYNAMIC_COLOR_CONTEXT_FRAME_KEY,
                        Rate = new API.Handlers.Rate.GSApiRateDefinition()
                    }
                },
                    IconID = GameSenseConstants.RGB_MASTER_ICON_ID
                }).ConfigureAwait(false);
            }).ConfigureAwait(false);
        }

        protected override async Task DisconnectInternal()
        {
            await RunForAllZones(async (zone) =>
            {
                await gsAPI.RemoveGameEvent(new GSApiRemoveGameEventPayload()
                {
                    Game = GameSenseConstants.RGB_MASTER_GAME_NAME,
                    Event = GetFixedEventName(GameSenseConstants.RGB_MASTER_SET_COLOR_EVENT_NAME, zone)
                }).ConfigureAwait(false);
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

            await RunForAllZones(async (zone) =>
            {
                await gsAPI.SendGameEvent(new GSApiSendGameEventPayload()
                {
                    Game = GameSenseConstants.RGB_MASTER_GAME_NAME,
                    Event = GetFixedEventName(GameSenseConstants.RGB_MASTER_SET_COLOR_EVENT_NAME, zone),
                    Data = new GSApiSendGameEventDataPayload()
                    {
                        Frame = frameObject
                    }
                }).ConfigureAwait(false);
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

        // TODO - MOVE THIS TO THE BASE CLASS AND REFACTOR SOME BASE STEELSERIES DEVICE CLASS.
        private async Task RunForAllZones(Func<string, Task> asyncFunction, IEnumerable<string> zones = null)
        {
            // If no zones were wanted just take them all.
            zones = zones ?? gameSenseDeviceMapping.GameSenseZones;

            var tasks = new List<Task>();

            foreach (var gameSenseZone in zones)
            {
                tasks.Add(Task.Run(async () => await asyncFunction(gameSenseZone)));
            }

            await Task.WhenAll(tasks);
        }
    }
}
