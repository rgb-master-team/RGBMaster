using Common;
using Provider;
using Q42.HueApi;
using Q42.HueApi.ColorConverters;
using Q42.HueApi.ColorConverters.Gamut;
using Q42.HueApi.ColorConverters.HSB;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace Hue
{
    public class HueLightDevice : Device
    {
        private readonly Q42.HueApi.Interfaces.ILocalHueClient localHueClient;
        private readonly Q42.HueApi.Light internalLight;

        public HueLightDevice(Guid discoveringProviderGuid, Q42.HueApi.Interfaces.ILocalHueClient localHueClient, Q42.HueApi.Light internalLight) : base(new HueLightDeviceMetadata(discoveringProviderGuid, internalLight.Name))
        {
            this.localHueClient = localHueClient;
            this.internalLight = internalLight;
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
            var command = new LightCommand
            {
                Brightness = brightness
            };

            var task = localHueClient.SendCommandAsync(command, new List<string> {internalLight.Id });
            task.ConfigureAwait(false);
            task.Wait();
        }

        protected override void SetColorInternal(Color color)
        {
            var command = new LightCommand();
            command.SetColor(new RGBColor(color.R, color.G, color.B));

            var task = localHueClient.SendCommandAsync(command, new List<string> { internalLight.Id });
            task.ConfigureAwait(false);
            task.Wait();
        }

        protected override void TurnOffInternal()
        {
            var command = new LightCommand();
            command.TurnOff();            

            var task = localHueClient.SendCommandAsync(command, new List<string> { internalLight.Id });
            task.ConfigureAwait(false);
            task.Wait();
        }

        protected override void TurnOnInternal()
        {
            var command = new LightCommand();
            command.TurnOn();

            var task = localHueClient.SendCommandAsync(command, new List<string> { internalLight.Id });
            task.ConfigureAwait(false);
            task.Wait();
        }
    }
}
