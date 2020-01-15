# RGBMaster
A .NET app that runs in the background and synchronises colours to different integratable devices. i.e Razer Chroma devices, Xiaomi Yeelight bulbs.

The project attempts to use official libraries when possible and focuses on the synchronisation between the devices, rather than the way we integrate with the device and its API/SDK. It uses Razer's `Colore` library for syncing colours via Razer's SDK through .NET bindings, and YeelightAPI library for .NET for that matter.

# Supported integrations
- [x] Razer Chroma
- [x] Yeelight
- [ ] Corsair - WIP
- [ ] Asus Aura - WIP
- [x] Logitech
- [ ] SteelSeries - WIP

# Supported "Effects"
RGBMaster allows choosing a desired effect for the way colors are chosen.
- [x] Music effect - Sync devices with a color that represents the tone of the current playback music in the running host.
- [x] Dominant effect - Sync devices with the dominant color of the displayed frames in the screen. Might be useful for movies, gaming, etc.

# Supported "Flows"
RGBMaster should allow choosing a flow, that will decide the way and timing of which the Effect is applied between devices.
For example - setting the order of devices receiving the color.
As of now, all devices are concurrently being changed to a certain color every time.

# Contribution
Feel free to suggest any idea you have for this app, or even make one yourself and open a PR. I'll review it and continue maintaining this small app :)

# TODOs
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
