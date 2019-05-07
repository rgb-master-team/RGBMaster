# chroma-yeelight-music
A .NET app that runs in the background and synchronises the matching colour of the background sound to both Razer Chroma's devices &amp; Xiaomi Yeelight bulbs.

# Contribution
Feel free to suggest any idea you have for this app, or even make one yourself and open a PR. I'll review it and continue maintaining this small app :)

# TODOs
- [ ] Add configuration settings (transition of the lights & effects - smooth or sudden, etc.)
- [ ] Allow selection of the discovered devices in the area
- [ ] Design a simple but nice looking UI
- [ ] Change synchronisation model to the bulbs to an async (currently Razer's `Colore` library asynchronously sets the colour, but we set the bulbs colours with messages sent through sockets synchronously)
- [ ] Add calculation of RGB values for the dominant or average sound in the host system
- [ ] Add options such as frequency/time between color changes
- [ ] Introduce a normal color palette that allows simply syncing colours between the devices (regardless of sound in the background)
