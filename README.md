# chroma-yeelight
A .NET app that runs in the background and synchronises colours to both Razer Chroma's devices &amp; Xiaomi Yeelight bulbs.
It uses Razer's `Colore` library for syncing colours via Razer's SDK through .NET bindings, and Yeelight library for .NET.

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
  - [ ] Add configuration settings (transition of the lights & effects - smooth or sudden, etc.)
  - [ ] Add options such as frequency/time between color changes
- Misc
  - [ ] Add some basic explanation
  - [ ] Add references to libraries used for this to work

# Open discussions
- Investigate possibility of retrieving colours from the Razer SDK - currently, there's a limitation in Razers SDK - it is only possible to set the colours of Chroma devices, but not fetching them. We should see if there's an option for a workaround, or at least a compromise (such as analysing Chroma profiles in order to allow running Chroma profiles between Chroma devices & Yeelight)
