using System;
using System.Collections.Generic;
using System.Text;

namespace GameSense
{
    public static class GameSenseConstants
    {
        public const string RGB_MASTER_GAME_NAME = "RGBMASTER";
        public const string RGB_MASTER_GAME_DEVELOPER = "RGBMASTER_TEAM";

        // We want to keep using "RGBMASTER" prefixes to avoid collisions with saved or pre-defined names.
        // When we used SET_COLOR, the SDK did not perform our task (failed silently).
        public const string RGB_MASTER_SET_COLOR_EVENT_NAME = "RGBMASTER_SET_COLOR";

        public const int RGB_MASTER_ICON_ID = 40;

        // This value represents headset, since we couldn't get "headset" device-type to work;
        // however, we are sure we don't accidentally modify other devices rather than headsets
        // due to using "earcups" zone.
        public const string HEADSET_DEVICE_TYPE = "rgb-2-zone";
        public const string HEADSET_ZONE_EARCUP = "earcups";
        public const string DYNAMIC_COLOR_MODE = "context-color";

        public const string DYNAMIC_COLOR_CONTEXT_FRAME_KEY = "rgb-master-dynamic-color";
    }
}
