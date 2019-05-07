# chroma-yeelight
A .NET app that runs in the background and synchronises colours to both Razer Chroma's devices &amp; Xiaomi Yeelight bulbs.

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
