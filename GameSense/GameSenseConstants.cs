using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        // List of all zoned-device-types
        public const string RGB_1_ZONE = "rgb-1-zone";
        public const string RGB_2_ZONE = "rgb-2-zone";
        public const string RGB_3_ZONE = "rgb-3-zone";
        public const string RGB_5_ZONE = "rgb-5-zone";
        public const string RGB_8_ZONE = "rgb-8-zone";
        public const string RGB_12_ZONE = "rgb-12-zone";
        public const string RGB_17_ZONE = "rgb-17-zone";
        public const string RGB_24_ZONE = "rgb-24-zone";
        public const string RGB_103_ZONE = "rgb-103-zone";
        public const string RGB_PER_KEY_ZONES = "rgb-per-key-zones";


        // This value represents headset, since we couldn't get "headset" device-type to work;
        // however, we are sure we don't accidentally modify other devices rather than headsets
        // due to using "earcups" zone.
        public const string HEADSET_ZONE_EARCUP = "earcups";
        public const string DYNAMIC_COLOR_MODE = "context-color";
        public static readonly IReadOnlyList<string> HEADSET_POSSIBLE_ZONES = new List<string>() { HEADSET_ZONE_EARCUP }.AsReadOnly();

        // Mouse
        public static readonly IReadOnlyList<string> MOUSE_POSSIBLE_DEVICE_TYPES = new List<string>() { RGB_2_ZONE, RGB_3_ZONE, RGB_8_ZONE }.AsReadOnly();
        public const string MOUSE_ZONE_WHEEL = "wheel";
        public const string MOUSE_ZONE_LOGO = "logo";
        public const string MOUSE_ZONE_BASE = "base"; // Sensei Wireless base
        public static readonly IReadOnlyList<string> MOUSE_POSSIBLE_ZONES = new List<string>() { MOUSE_ZONE_BASE, MOUSE_ZONE_LOGO, MOUSE_ZONE_WHEEL }.AsReadOnly();

        // Keyboard
        public static readonly IReadOnlyList<string> KEYBOARD_POSSIBLE_DEVICE_TYPES = new List<string>() { RGB_3_ZONE, RGB_5_ZONE, RGB_PER_KEY_ZONES }.AsReadOnly();
        public const string KEYBOARD_ZONE_FN = "function-keys";
        public const string KEYBOARD_ZONE_MAIN = "main-keyboard";
        public const string KEYBOARD_ZONE_KEYPAD = "keypad";
        public const string KEYBOARD_ZONE_NUMKEYS = "number-keys";
        public const string KEYBOARD_ZONE_MACROKEYS = "macro-keys";
        public static readonly IReadOnlyList<string> KEYBOARD_POSSIBLE_ZONES = new List<string>() { KEYBOARD_ZONE_FN, KEYBOARD_ZONE_KEYPAD, KEYBOARD_ZONE_MACROKEYS, KEYBOARD_ZONE_MAIN, KEYBOARD_ZONE_NUMKEYS }.AsReadOnly();


        public const string DYNAMIC_COLOR_CONTEXT_FRAME_KEY = "rgb-master-dynamic-color";
    }
}
