<p align="center"><img width="150px" src="./Logo/1024.png" alt="RGBMaster"></p>

# RGB Master
> A .NET app that runs in the background and synchronises colours to different integratable devices. i.e Razer Chroma devices, Xiaomi Yeelight bulbs.

[![Discord](https://img.shields.io/discord/717472380934422560.svg?label=&logo=discord&logoColor=ffffff&color=7389D8&labelColor=6A7EC2)](https://discord.gg/zWbe3UV)
[![Percentage of issues still open](http://isitmaintained.com/badge/open/rgb-master-team/RGBMaster.svg)](http://isitmaintained.com/project/rgb-master-team/RGBMaster "Percentage of issues still open")
[![Average time to resolve an issue](http://isitmaintained.com/badge/resolution/rgb-master-team/RGBMaster.svg)](http://isitmaintained.com/project/rgb-master-team/RGBMaster "Average time to resolve an issue")
[![GitHub stars](https://img.shields.io/github/stars/rgb-master-team/RGBMaster.svg)](https://github.com/rgb-master-team/RGBMaster/stargazers)

# About

The project attempts to use official libraries when possible and focuses on the synchronisation between the devices, rather than the way we integrate with the device and its API/SDK. It uses Razer's `Colore` library for syncing colours via Razer's SDK through .NET bindings, and YeelightAPI library for .NET for that matter.

## Table of Contents

- [Supported integrations](#integrations)
- [Supported "Effects"](#effects)
- [Supported "Flows"](#flows)
- [Contribution](#contrib)
- [TODOs](#todos)

<a name="integrations"></a>
## Supported integrations
- [x] Razer Chroma
- [x] Yeelight
- [x] Logitech
- [x] MagicHome
- [ ] Corsair - WIP
- [ ] Asus Aura - WIP
- [ ] SteelSeries - WIP
- [ ] ShellyRGB - WIP

<a name="effects"></a>
## Supported "Effects"
RGBMaster allows choosing a desired effect for the way colors are chosen.
- Music effect - Sync devices with a color that represents the tone of the current playback music in the running host.
- Dominant effect - Sync devices with the dominant color of the displayed frames in the screen. Might be useful for movies, gaming, etc.

<a name="flows"></a>
## Supported "Flows"
RGBMaster should allow choosing a flow, that will decide the way and timing of which the Effect is applied between devices.
For example - setting the order of devices receiving the color.
As of now, all devices are concurrently being changed to a certain color every time.

<a name="contrib"></a>
## Contribution
Feel free to suggest any idea you have for this app, or even make one yourself and open a PR. I'll review it and continue maintaining this small app :)

<a name="todos"></a>
## TODOs
- General
  - [ ] Design a simple but nice looking UI
  - [ ] Allow selection of the discovered devices in the area
  - [ ] Change synchronisation model to the bulbs to an async (currently Razer's `Colore` library asynchronously sets the colour, but we set the bulbs colours with messages sent through sockets synchronously)
  - [ ] Add calculation of RGB values for the dominant or average sound in the host system
  - [ ] Introduce a normal color palette that allows simply syncing colours between the devices (regardless of sound in the background)
- Music Mode -
  - [ ] Add configuration settings for effects (transition of the music effect - smooth or sudden, etc.)
  - [ ] Add options such as frequency/time between color changes
  - [ ] Allow defining the color spectrum for each volume level
  - [ ] Allow defining the spectrum levels themselves (volume levels that trigger changes)
- Misc
  - [ ] Add some basic explanation
  - [ ] Add references to libraries used for this to work
