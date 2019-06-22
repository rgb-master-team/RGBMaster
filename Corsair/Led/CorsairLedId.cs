namespace Corsair.Led
{
    /// <summary>
    /// Contains shared list of all leds on all devices (keyboard, mouse, mouse mat, headset, headset stand, DIY, memory module, cooler) and all models/physical layouts.
    /// </summary>
    public enum CorsairLedId
    {
        /// <summary>
        /// Dummy value
        /// </summary>
        Invalid,

        /// <summary>
        /// For keyboard escape led
        /// </summary>
        Escape,

        /// <summary>
        /// For keyboard F1 led
        /// </summary>
        F1,

        /// <summary>
        /// For keyboard F2 led
        /// </summary>
        F2,

        /// <summary>
        /// For keyboard F3 led
        /// </summary>
        F3,

        /// <summary>
        /// For keyboard F4 led
        /// </summary>
        F4,

        /// <summary>
        /// For keyboard F5 led
        /// </summary>
        F5,

        /// <summary>
        /// For keyboard F6 led
        /// </summary>
        F6,

        /// <summary>
        /// For keyboard F7 led
        /// </summary>
        F7,

        /// <summary>
        /// For keyboard F8 led
        /// </summary>
        F8,

        /// <summary>
        /// For keyboard F9 led
        /// </summary>
        F9,

        /// <summary>
        /// For keyboard F10 led
        /// </summary>
        F10,

        /// <summary>
        /// For keyboard F11 led
        /// </summary>
        F11,

        /// <summary>
        /// For keyboard grave accent and tilde led
        /// </summary>
        GraveAccentAndTilde,

        /// <summary>
        /// For keyboard 1 led
        /// </summary>
        Key_1,

		/// <summary>
		/// For keyboard 2 led
		/// </summary>
		Key_2,

		/// <summary>
		/// For keyboard 3 led
		/// </summary>
		Key_3,

		/// <summary>
		/// For keyboard 4 led
		/// </summary>
		Key_4,

		/// <summary>
		/// For keyboard 5 led
		/// </summary>
		Key_5,

		/// <summary>
		/// For keyboard 6 led
		/// </summary>
		Key_6,

		/// <summary>
		/// For keyboard 7 led
		/// </summary>
		Key_7,

		/// <summary>
		/// For keyboard 8 led
		/// </summary>
		Key_8,

		/// <summary>
		/// For keyboard 9 led
		/// </summary>
		Key_9,

		/// <summary>
		/// For keyboard 0 led
		/// </summary>
		Key_0,

        /// <summary>
        /// For keyboard minus and underscore led
        /// </summary>
        MinusAndUnderscore,

        /// <summary>
        /// For keyboard tab led
        /// </summary>
        Tab,

        /// <summary>
        /// For keyboard q led
        /// </summary>
        Q,

        /// <summary>
        /// For keyboard w led
        /// </summary>
        W,

        /// <summary>
        /// For keyboard e led
        /// </summary>
        E,

        /// <summary>
        /// For keyboard r led
        /// </summary>
        R = 29,

        /// <summary>
        /// For keyboard t led
        /// </summary>
        T = 30,

        /// <summary>
        /// For keyboard y led
        /// </summary>
        Y = 31,

        /// <summary>
        /// For keyboard u led
        /// </summary>
        U = 32,

        /// <summary>
        /// For keyboard i led
        /// </summary>
        I = 33,

        /// <summary>
        /// For keyboard o led
        /// </summary>
        O = 34,

        /// <summary>
        /// For keyboard p led
        /// </summary>
        P = 35,

        /// <summary>
        /// For keyboard bracket(left) led
        /// </summary>
        BracketLeft = 36,

        /// <summary>
        /// For keyboard caps lock led
        /// </summary>
        CapsLock = 37,

        /// <summary>
        /// For keyboard a led
        /// </summary>
        A = 38,

        /// <summary>
        /// For keyboard s led
        /// </summary>
        S = 39,

        /// <summary>
        /// For keyboard d led
        /// </summary>
        D = 40,

        /// <summary>
        /// For keyboard f led
        /// </summary>
        F = 41,

        /// <summary>
        /// For keyboard g led
        /// </summary>
        G = 42,

        /// <summary>
        /// For keyboard h led
        /// </summary>
        H = 43,

        /// <summary>
        /// For keyboard j led
        /// </summary>
        J = 44,

        /// <summary>
        /// For keyboard k led
        /// </summary>
        K = 45,

        /// <summary>
        /// For keyboard l led
        /// </summary>
        L = 46,

        /// <summary>
        /// For keyboard semicolon and colon led
        /// </summary>
        SemicolonAndColon = 47,

        /// <summary>
        /// For keyboard apostrophe and double quote led
        /// </summary>
        ApostropheAndDoubleQuote = 48,

        /// <summary>
        /// For keyboard left shift led
        /// </summary>
        LeftShift = 49,

        /// <summary>
        /// For keyboard non-US backslash led
        /// </summary>
        NonUsBackslash = 50,

        /// <summary>
        /// For keyboard z led
        /// </summary>
        Z = 51,

        /// <summary>
        /// For keyboard x led
        /// </summary>
        X = 52,

        /// <summary>
        /// For keyboard c led
        /// </summary>
        C = 53,

        /// <summary>
        /// For keyboard v led
        /// </summary>
        V = 54,

        /// <summary>
        /// For keyboard b led
        /// </summary>
        B = 55,

        /// <summary>
        /// For keyboard n led
        /// </summary>
        N = 56,

        /// <summary>
        /// For keyboard m led
        /// </summary>
        M = 57,

        /// <summary>
        /// For keyboard comma and less than led
        /// </summary>
        CommaAndLessThan = 58,

        /// <summary>
        /// For keyboard period and Bigger than led
        /// </summary>
        PeriodAndBiggerThan = 59,

        /// <summary>
        /// For keyboard slash and question mark led
        /// </summary>
        SlashAndQuestionMark = 60,

        /// <summary>
        /// For keyboard left ctrl led
        /// </summary>
        LeftCtrl = 61,

        /// <summary>
        /// For keyboard left gui led
        /// </summary>
        LeftGui = 62,

        /// <summary>
        /// For keyboard left alt led
        /// </summary>
        LeftAlt = 63,

        /// <summary>
        /// For keyboard lang 2 led
        /// </summary>
        Lang2 = 64,

        /// <summary>
        /// For keyboard space led
        /// </summary>
        Space = 65,

        /// <summary>
        /// For keyboard lang 1 led
        /// </summary>
        Lang1 = 66,

        /// <summary>
        /// For keyboard international 2 led
        /// </summary>
        International2 = 67,

        /// <summary>
        /// For keyboard right alt led
        /// </summary>
        RightAlt = 68,

        /// <summary>
        /// For keyboard right gui led
        /// </summary>
        RightGui = 69,

        /// <summary>
        /// For keyboard application led
        /// </summary>
        Application = 70,

        /// <summary>
        /// For keyboard led programming led
        /// </summary>
        LedProgramming = 71,

        /// <summary>
        /// For keyboard brightness led
        /// </summary>
        Brightness = 72,

        /// <summary>
        /// For keyboard F12 led
        /// </summary>
        F12 = 73,

        /// <summary>
        /// For keyboard print screen led
        /// </summary>
        PrintScreen = 74,

        /// <summary>
        /// For keyboard scroll lock led
        /// </summary>
        ScrollLock = 75,

        /// <summary>
        /// For keyboard pause break led
        /// </summary>
        PauseBreak = 76,

        /// <summary>
        /// For keyboard insert led
        /// </summary>
        Insert = 77,

        /// <summary>
        /// For keyboard home led
        /// </summary>
        Home = 78,

        /// <summary>
        /// For keyboard page up led
        /// </summary>
        PageUp = 79,

        /// <summary>
        /// For keyboard bracket(right) led
        /// </summary>
        BracketRight = 80,

        /// <summary>
        /// For keyboard backslash led
        /// </summary>
        Backslash = 81,

        /// <summary>
        /// For keyboard non-US tilde led
        /// </summary>
        NonUsTilde = 82,

        /// <summary>
        /// For keyboard enter led
        /// </summary>
        Enter = 83,

        /// <summary>
        /// For keyboard international 1 led
        /// </summary>
        International1 = 84,

        /// <summary>
        /// For keyboard equals and plus led
        /// </summary>
        EqualsAndPlus = 85,

        /// <summary>
        /// For keyboard international 3 led
        /// </summary>
        International3 = 86,

        /// <summary>
        /// For keyboard backspace led
        /// </summary>
        Backspace = 87,

        /// <summary>
        /// For keyboard delete led
        /// </summary>
        Delete = 88,

        /// <summary>
        /// For keyboard end led
        /// </summary>
        End = 89,

        /// <summary>
        /// For keyboard page down led
        /// </summary>
        PageDown = 90,

        /// <summary>
        /// For keyboard right shift led
        /// </summary>
        RightShift = 91,

        /// <summary>
        /// For keyboard right ctrl led
        /// </summary>
        RightCtrl = 92,

        /// <summary>
        /// For keyboard up arrow led
        /// </summary>
        UpArrow = 93,

        /// <summary>
        /// For keyboard left arrow led
        /// </summary>
        LeftArrow = 94,

        /// <summary>
        /// For keyboard down arrow led
        /// </summary>
        DownArrow = 95,

        /// <summary>
        /// For keyboard right arrow led
        /// </summary>
        RightArrow = 96,

        /// <summary>
        /// For keyboard win lock led
        /// </summary>
        WinLock = 97,

        /// <summary>
        /// For keyboard mute led
        /// </summary>
        Mute = 98,

        /// <summary>
        /// For keyboard stop led
        /// </summary>
        Stop = 99,

        /// <summary>
        /// For keyboard scan previous track led
        /// </summary>
        ScanPreviousTrack = 100,

        /// <summary>
        /// For keyboard play/pause led
        /// </summary>
        PlayPause = 101,

        /// <summary>
        /// For keyboard scan next track led
        /// </summary>
        ScanNextTrack = 102,

        /// <summary>
        /// For keyboard num lock led
        /// </summary>
        NumLock = 103,

        /// <summary>
        /// For keyboard keypad slash led
        /// </summary>
        KeypadSlash = 104,

        /// <summary>
        /// For keyboard keypad asterisk led
        /// </summary>
        KeypadAsterisk = 105,

        /// <summary>
        /// For keyboard keypad minus led
        /// </summary>
        KeypadMinus = 106,

        /// <summary>
        /// For keyboard keypad plus led
        /// </summary>
        KeypadPlus = 107,

        /// <summary>
        /// For keyboard keypad enter led
        /// </summary>
        KeypadEnter = 108,

        /// <summary>
        /// For keyboard keypad 7 led
        /// </summary>
        Keypad7 = 109,

        /// <summary>
        /// For keyboard keypad 8 led
        /// </summary>
        Keypad8 = 110,

        /// <summary>
        /// For keyboard keypad 9 led
        /// </summary>
        Keypad9 = 111,

        /// <summary>
        /// For keyboard keypad comma led
        /// </summary>
        KeypadComma = 112,

        /// <summary>
        /// For keyboard keypad 4 led
        /// </summary>
        Keypad4 = 113,

        /// <summary>
        /// For keyboard keypad 5 led
        /// </summary>
        Keypad5 = 114,

        /// <summary>
        /// For keyboard keypad 6 led
        /// </summary>
        Keypad6 = 115,

        /// <summary>
        /// For keyboard keypad 1 led
        /// </summary>
        Keypad1 = 116,

        /// <summary>
        /// For keyboard keypad 2 led
        /// </summary>
        Keypad2 = 117,

        /// <summary>
        /// For keyboard keypad 3 led
        /// </summary>
        Keypad3 = 118,

        /// <summary>
        /// For keyboard keypad 0 led
        /// </summary>
        Keypad0 = 119,

        /// <summary>
        /// For keyboard keypad period and delete led
        /// </summary>
        KeypadPeriodAndDelete = 120,

        /// <summary>
        /// For keyboard G1 led
        /// </summary>
        G1 = 121,

        /// <summary>
        /// For keyboard G2 led
        /// </summary>
        G2 = 122,

        /// <summary>
        /// For keyboard G3 led
        /// </summary>
        G3 = 123,

        /// <summary>
        /// For keyboard G4 led
        /// </summary>
        G4 = 124,

        /// <summary>
        /// For keyboard G5 led
        /// </summary>
        G5 = 125,

        /// <summary>
        /// For keyboard G6 led
        /// </summary>
        G6 = 126,

        /// <summary>
        /// For keyboard G7 led
        /// </summary>
        G7 = 127,

        /// <summary>
        /// For keyboard G8 led
        /// </summary>
        G8 = 128,

        /// <summary>
        /// For keyboard G9 led
        /// </summary>
        G9 = 129,

        /// <summary>
        /// For keyboard G10 led
        /// </summary>
        G10 = 130,

        /// <summary>
        /// For keyboard volume up led
        /// </summary>
        VolumeUp = 131,

        /// <summary>
        /// For keyboard volume down led
        /// </summary>
        VolumeDown = 132,

        /// <summary>
        /// For keyboard MR led
        /// </summary>
        MR = 133,

        /// <summary>
        /// For keyboard M1 led
        /// </summary>
        M1 = 134,

        /// <summary>
        /// For keyboard M2 led
        /// </summary>
        M2 = 135,

        /// <summary>
        /// For keyboard M3 led
        /// </summary>
        M3 = 136,

        /// <summary>
        /// For keyboard G11 led
        /// </summary>
        G11 = 137,

        /// <summary>
        /// For keyboard G12 led
        /// </summary>
        G12 = 138,

        /// <summary>
        /// For keyboard G13 led
        /// </summary>
        G13 = 139,

        /// <summary>
        /// For keyboard G14 led
        /// </summary>
        G14 = 140,

        /// <summary>
        /// For keyboard G15 led
        /// </summary>
        G15 = 141,

        /// <summary>
        /// For keyboard G16 led
        /// </summary>
        G16 = 142,

        /// <summary>
        /// For keyboard G17 led
        /// </summary>
        G17 = 143,

        /// <summary>
        /// For keyboard G18 led
        /// </summary>
        G18 = 144,

        /// <summary>
        /// For keyboard international 5 led
        /// </summary>
        International5 = 145,

        /// <summary>
        /// For keyboard international 4 led
        /// </summary>
        International4 = 146,

        /// <summary>
        /// For keyboard fn led
        /// </summary>
        Fn = 147,

        /// <summary>
        /// For mouse 1 led
        /// </summary>
        CLM_1 = 148,

        /// <summary>
        /// For mouse 2 led
        /// </summary>
        CLM_2 = 149,

        /// <summary>
        /// For mouse 3 led
        /// </summary>
        CLM_3 = 150,

        /// <summary>
        /// For mouse 4 led
        /// </summary>
        CLM_4 = 151,

        /// <summary>
        /// For headset left logo led
        /// </summary>
        CLH_LeftLogo = 152,

        /// <summary>
        /// For headset right logo led
        /// </summary>
        CLH_RightLogo = 153,

        /// <summary>
        /// For keyboard logo led
        /// </summary>
        Logo = 154,

        /// <summary>
        /// For mousemat zone 1 led
        /// </summary>
        CLMM_Zone1 = 155,

        /// <summary>
        /// For mousemat zone 2 led
        /// </summary>
        CLMM_Zone2 = 156,

        /// <summary>
        /// For mousemat zone 3 led
        /// </summary>
        CLMM_Zone3 = 157,

        /// <summary>
        /// For mousemat zone 4 led
        /// </summary>
        CLMM_Zone4 = 158,

        /// <summary>
        /// For mousemat zone 5 led
        /// </summary>
        CLMM_Zone5 = 159,

        /// <summary>
        /// For mousemat zone 6 led
        /// </summary>
        CLMM_Zone6 = 160,

        /// <summary>
        /// For mousemat zone 7 led
        /// </summary>
        CLMM_Zone7 = 161,

        /// <summary>
        /// For mousemat zone 8 led
        /// </summary>
        CLMM_Zone8 = 162,

        /// <summary>
        /// For mousemat zone 9 led
        /// </summary>
        CLMM_Zone9 = 163,

        /// <summary>
        /// For mousemat zone 10 led
        /// </summary>
        CLMM_Zone10 = 164,

        /// <summary>
        /// For mousemat zone 11 led
        /// </summary>
        CLMM_Zone11 = 165,

        /// <summary>
        /// For mousemat zone 12 led
        /// </summary>
        CLMM_Zone12 = 166,

        /// <summary>
        /// For mousemat zone 13 led
        /// </summary>
        CLMM_Zone13 = 167,

        /// <summary>
        /// For mousemat zone 14 led
        /// </summary>
        CLMM_Zone14 = 168,

        /// <summary>
        /// For mousemat zone 15 led
        /// </summary>
        CLMM_Zone15 = 169,

        /// <summary>
        /// For keyboard light pipe zone 1 led
        /// </summary>
        CLKLP_Zone1 = 170,

        /// <summary>
        /// For keyboard light pipe zone 2 led
        /// </summary>
        CLKLP_Zone2 = 171,

        /// <summary>
        /// For keyboard light pipe zone 3 led
        /// </summary>
        CLKLP_Zone3 = 172,

        /// <summary>
        /// For keyboard light pipe zone 4 led
        /// </summary>
        CLKLP_Zone4 = 173,

        /// <summary>
        /// For keyboard light pipe zone 5 led
        /// </summary>
        CLKLP_Zone5 = 174,

        /// <summary>
        /// For keyboard light pipe zone 6 led
        /// </summary>
        CLKLP_Zone6 = 175,

        /// <summary>
        /// For keyboard light pipe zone 7 led
        /// </summary>
        CLKLP_Zone7 = 176,

        /// <summary>
        /// For keyboard light pipe zone 8 led
        /// </summary>
        CLKLP_Zone8 = 177,

        /// <summary>
        /// For keyboard light pipe zone 9 led
        /// </summary>
        CLKLP_Zone9 = 178,

        /// <summary>
        /// For keyboard light pipe zone 10 led
        /// </summary>
        CLKLP_Zone10 = 179,

        /// <summary>
        /// For keyboard light pipe zone 11 led
        /// </summary>
        CLKLP_Zone11 = 180,

        /// <summary>
        /// For keyboard light pipe zone 12 led
        /// </summary>
        CLKLP_Zone12 = 181,

        /// <summary>
        /// For keyboard light pipe zone 13 led
        /// </summary>
        CLKLP_Zone13 = 182,

        /// <summary>
        /// For keyboard light pipe zone 14 led
        /// </summary>
        CLKLP_Zone14 = 183,

        /// <summary>
        /// For keyboard light pipe zone 15 led
        /// </summary>
        CLKLP_Zone15 = 184,

        /// <summary>
        /// For keyboard light pipe zone 16 led
        /// </summary>
        CLKLP_Zone16 = 185,

        /// <summary>
        /// For keyboard light pipe zone 17 led
        /// </summary>
        CLKLP_Zone17 = 186,

        /// <summary>
        /// For keyboard light pipe zone 18 led
        /// </summary>
        CLKLP_Zone18 = 187,

        /// <summary>
        /// For keyboard light pipe zone 19 led
        /// </summary>
        CLKLP_Zone19 = 188,

        /// <summary>
        /// For mouse 5 led
        /// </summary>
        CLM_5 = 189,

        /// <summary>
        /// For mouse 6 led
        /// </summary>
        CLM_6 = 190,

        /// <summary>
        /// For headset stand zone 1 led
        /// </summary>
        CLHSS_Zone1 = 191,

        /// <summary>
        /// For headset stand zone 2 led
        /// </summary>
        CLHSS_Zone2 = 192,

        /// <summary>
        /// For headset stand zone 3 led
        /// </summary>
        CLHSS_Zone3 = 193,

        /// <summary>
        /// For headset stand zone 4 led
        /// </summary>
        CLHSS_Zone4 = 194,

        /// <summary>
        /// For headset stand zone 5 led
        /// </summary>
        CLHSS_Zone5 = 195,

        /// <summary>
        /// For headset stand zone 6 led
        /// </summary>
        CLHSS_Zone6 = 196,

        /// <summary>
        /// For headset stand zone 7 led
        /// </summary>
        CLHSS_Zone7 = 197,

        /// <summary>
        /// For headset stand zone 8 led
        /// </summary>
        CLHSS_Zone8 = 198,

        /// <summary>
        /// For headset stand zone 9 led
        /// </summary>
        CLHSS_Zone9 = 199,

        /// <summary>
        /// For first channel of the DIY-devices led 1
        /// </summary>
        CLD_C1_1 = 200,

        /// <summary>
        /// For first channel of the DIY-devices led 2
        /// </summary>
        CLD_C1_2 = 201,

        /// <summary>
        /// For first channel of the DIY-devices led 3
        /// </summary>
        CLD_C1_3 = 202,

        /// <summary>
        /// For first channel of the DIY-devices led 4
        /// </summary>
        CLD_C1_4 = 203,

        /// <summary>
        /// For first channel of the DIY-devices led 5
        /// </summary>
        CLD_C1_5 = 204,

        /// <summary>
        /// For first channel of the DIY-devices led 6
        /// </summary>
        CLD_C1_6 = 205,

        /// <summary>
        /// For first channel of the DIY-devices led 7
        /// </summary>
        CLD_C1_7 = 206,

        /// <summary>
        /// For first channel of the DIY-devices led 8
        /// </summary>
        CLD_C1_8 = 207,

        /// <summary>
        /// For first channel of the DIY-devices led 9
        /// </summary>
        CLD_C1_9 = 208,

        /// <summary>
        /// For first channel of the DIY-devices led 10
        /// </summary>
        CLD_C1_10 = 209,

        /// <summary>
        /// For first channel of the DIY-devices led 11
        /// </summary>
        CLD_C1_11 = 210,

        /// <summary>
        /// For first channel of the DIY-devices led 12
        /// </summary>
        CLD_C1_12 = 211,

        /// <summary>
        /// For first channel of the DIY-devices led 13
        /// </summary>
        CLD_C1_13 = 212,

        /// <summary>
        /// For first channel of the DIY-devices led 14
        /// </summary>
        CLD_C1_14 = 213,

        /// <summary>
        /// For first channel of the DIY-devices led 15
        /// </summary>
        CLD_C1_15 = 214,

        /// <summary>
        /// For first channel of the DIY-devices led 16
        /// </summary>
        CLD_C1_16 = 215,

        /// <summary>
        /// For first channel of the DIY-devices led 17
        /// </summary>
        CLD_C1_17 = 216,

        /// <summary>
        /// For first channel of the DIY-devices led 18
        /// </summary>
        CLD_C1_18 = 217,

        /// <summary>
        /// For first channel of the DIY-devices led 19
        /// </summary>
        CLD_C1_19 = 218,

        /// <summary>
        /// For first channel of the DIY-devices led 20
        /// </summary>
        CLD_C1_20 = 219,

        /// <summary>
        /// For first channel of the DIY-devices led 21
        /// </summary>
        CLD_C1_21 = 220,

        /// <summary>
        /// For first channel of the DIY-devices led 22
        /// </summary>
        CLD_C1_22 = 221,

        /// <summary>
        /// For first channel of the DIY-devices led 23
        /// </summary>
        CLD_C1_23 = 222,

        /// <summary>
        /// For first channel of the DIY-devices led 24
        /// </summary>
        CLD_C1_24 = 223,

        /// <summary>
        /// For first channel of the DIY-devices led 25
        /// </summary>
        CLD_C1_25 = 224,

        /// <summary>
        /// For first channel of the DIY-devices led 26
        /// </summary>
        CLD_C1_26 = 225,

        /// <summary>
        /// For first channel of the DIY-devices led 27
        /// </summary>
        CLD_C1_27 = 226,

        /// <summary>
        /// For first channel of the DIY-devices led 28
        /// </summary>
        CLD_C1_28 = 227,

        /// <summary>
        /// For first channel of the DIY-devices led 29
        /// </summary>
        CLD_C1_29 = 228,

        /// <summary>
        /// For first channel of the DIY-devices led 30
        /// </summary>
        CLD_C1_30 = 229,

        /// <summary>
        /// For first channel of the DIY-devices led 31
        /// </summary>
        CLD_C1_31 = 230,

        /// <summary>
        /// For first channel of the DIY-devices led 32
        /// </summary>
        CLD_C1_32 = 231,

        /// <summary>
        /// For first channel of the DIY-devices led 33
        /// </summary>
        CLD_C1_33 = 232,

        /// <summary>
        /// For first channel of the DIY-devices led 34
        /// </summary>
        CLD_C1_34 = 233,

        /// <summary>
        /// For first channel of the DIY-devices led 35
        /// </summary>
        CLD_C1_35 = 234,

        /// <summary>
        /// For first channel of the DIY-devices led 36
        /// </summary>
        CLD_C1_36 = 235,

        /// <summary>
        /// For first channel of the DIY-devices led 37
        /// </summary>
        CLD_C1_37 = 236,

        /// <summary>
        /// For first channel of the DIY-devices led 38
        /// </summary>
        CLD_C1_38 = 237,

        /// <summary>
        /// For first channel of the DIY-devices led 39
        /// </summary>
        CLD_C1_39 = 238,

        /// <summary>
        /// For first channel of the DIY-devices led 40
        /// </summary>
        CLD_C1_40 = 239,

        /// <summary>
        /// For first channel of the DIY-devices led 41
        /// </summary>
        CLD_C1_41 = 240,

        /// <summary>
        /// For first channel of the DIY-devices led 42
        /// </summary>
        CLD_C1_42 = 241,

        /// <summary>
        /// For first channel of the DIY-devices led 43
        /// </summary>
        CLD_C1_43 = 242,

        /// <summary>
        /// For first channel of the DIY-devices led 44
        /// </summary>
        CLD_C1_44 = 243,

        /// <summary>
        /// For first channel of the DIY-devices led 45
        /// </summary>
        CLD_C1_45 = 244,

        /// <summary>
        /// For first channel of the DIY-devices led 46
        /// </summary>
        CLD_C1_46 = 245,

        /// <summary>
        /// For first channel of the DIY-devices led 47
        /// </summary>
        CLD_C1_47 = 246,

        /// <summary>
        /// For first channel of the DIY-devices led 48
        /// </summary>
        CLD_C1_48 = 247,

        /// <summary>
        /// For first channel of the DIY-devices led 49
        /// </summary>
        CLD_C1_49 = 248,

        /// <summary>
        /// For first channel of the DIY-devices led 50
        /// </summary>
        CLD_C1_50 = 249,

        /// <summary>
        /// For first channel of the DIY-devices led 51
        /// </summary>
        CLD_C1_51 = 250,

        /// <summary>
        /// For first channel of the DIY-devices led 52
        /// </summary>
        CLD_C1_52 = 251,

        /// <summary>
        /// For first channel of the DIY-devices led 53
        /// </summary>
        CLD_C1_53 = 252,

        /// <summary>
        /// For first channel of the DIY-devices led 54
        /// </summary>
        CLD_C1_54 = 253,

        /// <summary>
        /// For first channel of the DIY-devices led 55
        /// </summary>
        CLD_C1_55 = 254,

        /// <summary>
        /// For first channel of the DIY-devices led 56
        /// </summary>
        CLD_C1_56 = 255,

        /// <summary>
        /// For first channel of the DIY-devices led 57
        /// </summary>
        CLD_C1_57 = 256,

        /// <summary>
        /// For first channel of the DIY-devices led 58
        /// </summary>
        CLD_C1_58 = 257,

        /// <summary>
        /// For first channel of the DIY-devices led 59
        /// </summary>
        CLD_C1_59 = 258,

        /// <summary>
        /// For first channel of the DIY-devices led 60
        /// </summary>
        CLD_C1_60 = 259,

        /// <summary>
        /// For first channel of the DIY-devices led 61
        /// </summary>
        CLD_C1_61 = 260,

        /// <summary>
        /// For first channel of the DIY-devices led 62
        /// </summary>
        CLD_C1_62 = 261,

        /// <summary>
        /// For first channel of the DIY-devices led 63
        /// </summary>
        CLD_C1_63 = 262,

        /// <summary>
        /// For first channel of the DIY-devices led 64
        /// </summary>
        CLD_C1_64 = 263,

        /// <summary>
        /// For first channel of the DIY-devices led 65
        /// </summary>
        CLD_C1_65 = 264,

        /// <summary>
        /// For first channel of the DIY-devices led 66
        /// </summary>
        CLD_C1_66 = 265,

        /// <summary>
        /// For first channel of the DIY-devices led 67
        /// </summary>
        CLD_C1_67 = 266,

        /// <summary>
        /// For first channel of the DIY-devices led 68
        /// </summary>
        CLD_C1_68 = 267,

        /// <summary>
        /// For first channel of the DIY-devices led 69
        /// </summary>
        CLD_C1_69 = 268,

        /// <summary>
        /// For first channel of the DIY-devices led 70
        /// </summary>
        CLD_C1_70 = 269,

        /// <summary>
        /// For first channel of the DIY-devices led 71
        /// </summary>
        CLD_C1_71 = 270,

        /// <summary>
        /// For first channel of the DIY-devices led 72
        /// </summary>
        CLD_C1_72 = 271,

        /// <summary>
        /// For first channel of the DIY-devices led 73
        /// </summary>
        CLD_C1_73 = 272,

        /// <summary>
        /// For first channel of the DIY-devices led 74
        /// </summary>
        CLD_C1_74 = 273,

        /// <summary>
        /// For first channel of the DIY-devices led 75
        /// </summary>
        CLD_C1_75 = 274,

        /// <summary>
        /// For first channel of the DIY-devices led 76
        /// </summary>
        CLD_C1_76 = 275,

        /// <summary>
        /// For first channel of the DIY-devices led 77
        /// </summary>
        CLD_C1_77 = 276,

        /// <summary>
        /// For first channel of the DIY-devices led 78
        /// </summary>
        CLD_C1_78 = 277,

        /// <summary>
        /// For first channel of the DIY-devices led 79
        /// </summary>
        CLD_C1_79 = 278,

        /// <summary>
        /// For first channel of the DIY-devices led 80
        /// </summary>
        CLD_C1_80 = 279,

        /// <summary>
        /// For first channel of the DIY-devices led 81
        /// </summary>
        CLD_C1_81 = 280,

        /// <summary>
        /// For first channel of the DIY-devices led 82
        /// </summary>
        CLD_C1_82 = 281,

        /// <summary>
        /// For first channel of the DIY-devices led 83
        /// </summary>
        CLD_C1_83 = 282,

        /// <summary>
        /// For first channel of the DIY-devices led 84
        /// </summary>
        CLD_C1_84 = 283,

        /// <summary>
        /// For first channel of the DIY-devices led 85
        /// </summary>
        CLD_C1_85 = 284,

        /// <summary>
        /// For first channel of the DIY-devices led 86
        /// </summary>
        CLD_C1_86 = 285,

        /// <summary>
        /// For first channel of the DIY-devices led 87
        /// </summary>
        CLD_C1_87 = 286,

        /// <summary>
        /// For first channel of the DIY-devices led 88
        /// </summary>
        CLD_C1_88 = 287,

        /// <summary>
        /// For first channel of the DIY-devices led 89
        /// </summary>
        CLD_C1_89 = 288,

        /// <summary>
        /// For first channel of the DIY-devices led 90
        /// </summary>
        CLD_C1_90 = 289,

        /// <summary>
        /// For first channel of the DIY-devices led 91
        /// </summary>
        CLD_C1_91 = 290,

        /// <summary>
        /// For first channel of the DIY-devices led 92
        /// </summary>
        CLD_C1_92 = 291,

        /// <summary>
        /// For first channel of the DIY-devices led 93
        /// </summary>
        CLD_C1_93 = 292,

        /// <summary>
        /// For first channel of the DIY-devices led 94
        /// </summary>
        CLD_C1_94 = 293,

        /// <summary>
        /// For first channel of the DIY-devices led 95
        /// </summary>
        CLD_C1_95 = 294,

        /// <summary>
        /// For first channel of the DIY-devices led 96
        /// </summary>
        CLD_C1_96 = 295,

        /// <summary>
        /// For first channel of the DIY-devices led 97
        /// </summary>
        CLD_C1_97 = 296,

        /// <summary>
        /// For first channel of the DIY-devices led 98
        /// </summary>
        CLD_C1_98 = 297,

        /// <summary>
        /// For first channel of the DIY-devices led 99
        /// </summary>
        CLD_C1_99 = 298,

        /// <summary>
        /// For first channel of the DIY-devices led 100
        /// </summary>
        CLD_C1_100 = 299,

        /// <summary>
        /// For first channel of the DIY-devices led 101
        /// </summary>
        CLD_C1_101 = 300,

        /// <summary>
        /// For first channel of the DIY-devices led 102
        /// </summary>
        CLD_C1_102 = 301,

        /// <summary>
        /// For first channel of the DIY-devices led 103
        /// </summary>
        CLD_C1_103 = 302,

        /// <summary>
        /// For first channel of the DIY-devices led 104
        /// </summary>
        CLD_C1_104 = 303,

        /// <summary>
        /// For first channel of the DIY-devices led 105
        /// </summary>
        CLD_C1_105 = 304,

        /// <summary>
        /// For first channel of the DIY-devices led 106
        /// </summary>
        CLD_C1_106 = 305,

        /// <summary>
        /// For first channel of the DIY-devices led 107
        /// </summary>
        CLD_C1_107 = 306,

        /// <summary>
        /// For first channel of the DIY-devices led 108
        /// </summary>
        CLD_C1_108 = 307,

        /// <summary>
        /// For first channel of the DIY-devices led 109
        /// </summary>
        CLD_C1_109 = 308,

        /// <summary>
        /// For first channel of the DIY-devices led 110
        /// </summary>
        CLD_C1_110 = 309,

        /// <summary>
        /// For first channel of the DIY-devices led 111
        /// </summary>
        CLD_C1_111 = 310,

        /// <summary>
        /// For first channel of the DIY-devices led 112
        /// </summary>
        CLD_C1_112 = 311,

        /// <summary>
        /// For first channel of the DIY-devices led 113
        /// </summary>
        CLD_C1_113 = 312,

        /// <summary>
        /// For first channel of the DIY-devices led 114
        /// </summary>
        CLD_C1_114 = 313,

        /// <summary>
        /// For first channel of the DIY-devices led 115
        /// </summary>
        CLD_C1_115 = 314,

        /// <summary>
        /// For first channel of the DIY-devices led 116
        /// </summary>
        CLD_C1_116 = 315,

        /// <summary>
        /// For first channel of the DIY-devices led 117
        /// </summary>
        CLD_C1_117 = 316,

        /// <summary>
        /// For first channel of the DIY-devices led 118
        /// </summary>
        CLD_C1_118 = 317,

        /// <summary>
        /// For first channel of the DIY-devices led 119
        /// </summary>
        CLD_C1_119 = 318,

        /// <summary>
        /// For first channel of the DIY-devices led 120
        /// </summary>
        CLD_C1_120 = 319,

        /// <summary>
        /// For first channel of the DIY-devices led 121
        /// </summary>
        CLD_C1_121 = 320,

        /// <summary>
        /// For first channel of the DIY-devices led 122
        /// </summary>
        CLD_C1_122 = 321,

        /// <summary>
        /// For first channel of the DIY-devices led 123
        /// </summary>
        CLD_C1_123 = 322,

        /// <summary>
        /// For first channel of the DIY-devices led 124
        /// </summary>
        CLD_C1_124 = 323,

        /// <summary>
        /// For first channel of the DIY-devices led 125
        /// </summary>
        CLD_C1_125 = 324,

        /// <summary>
        /// For first channel of the DIY-devices led 126
        /// </summary>
        CLD_C1_126 = 325,

        /// <summary>
        /// For first channel of the DIY-devices led 127
        /// </summary>
        CLD_C1_127 = 326,

        /// <summary>
        /// For first channel of the DIY-devices led 128
        /// </summary>
        CLD_C1_128 = 327,

        /// <summary>
        /// For first channel of the DIY-devices led 129
        /// </summary>
        CLD_C1_129 = 328,

        /// <summary>
        /// For first channel of the DIY-devices led 130
        /// </summary>
        CLD_C1_130 = 329,

        /// <summary>
        /// For first channel of the DIY-devices led 131
        /// </summary>
        CLD_C1_131 = 330,

        /// <summary>
        /// For first channel of the DIY-devices led 132
        /// </summary>
        CLD_C1_132 = 331,

        /// <summary>
        /// For first channel of the DIY-devices led 133
        /// </summary>
        CLD_C1_133 = 332,

        /// <summary>
        /// For first channel of the DIY-devices led 134
        /// </summary>
        CLD_C1_134 = 333,

        /// <summary>
        /// For first channel of the DIY-devices led 135
        /// </summary>
        CLD_C1_135 = 334,

        /// <summary>
        /// For first channel of the DIY-devices led 136
        /// </summary>
        CLD_C1_136 = 335,

        /// <summary>
        /// For first channel of the DIY-devices led 137
        /// </summary>
        CLD_C1_137 = 336,

        /// <summary>
        /// For first channel of the DIY-devices led 138
        /// </summary>
        CLD_C1_138 = 337,

        /// <summary>
        /// For first channel of the DIY-devices led 139
        /// </summary>
        CLD_C1_139 = 338,

        /// <summary>
        /// For first channel of the DIY-devices led 140
        /// </summary>
        CLD_C1_140 = 339,

        /// <summary>
        /// For first channel of the DIY-devices led 141
        /// </summary>
        CLD_C1_141 = 340,

        /// <summary>
        /// For first channel of the DIY-devices led 142
        /// </summary>
        CLD_C1_142 = 341,

        /// <summary>
        /// For first channel of the DIY-devices led 143
        /// </summary>
        CLD_C1_143 = 342,

        /// <summary>
        /// For first channel of the DIY-devices led 144
        /// </summary>
        CLD_C1_144 = 343,

        /// <summary>
        /// For first channel of the DIY-devices led 145
        /// </summary>
        CLD_C1_145 = 344,

        /// <summary>
        /// For first channel of the DIY-devices led 146
        /// </summary>
        CLD_C1_146 = 345,

        /// <summary>
        /// For first channel of the DIY-devices led 147
        /// </summary>
        CLD_C1_147 = 346,

        /// <summary>
        /// For first channel of the DIY-devices led 148
        /// </summary>
        CLD_C1_148 = 347,

        /// <summary>
        /// For first channel of the DIY-devices led 149
        /// </summary>
        CLD_C1_149 = 348,

        /// <summary>
        /// For first channel of the DIY-devices led 150
        /// </summary>
        CLD_C1_150 = 349,

        /// <summary>
        /// For second channel of the DIY-devices led 1
        /// </summary>
        CLD_C2_1 = 350,

        /// <summary>
        /// For second channel of the DIY-devices led 2
        /// </summary>
        CLD_C2_2 = 351,

        /// <summary>
        /// For second channel of the DIY-devices led 3
        /// </summary>
        CLD_C2_3 = 352,

        /// <summary>
        /// For second channel of the DIY-devices led 4
        /// </summary>
        CLD_C2_4 = 353,

        /// <summary>
        /// For second channel of the DIY-devices led 5
        /// </summary>
        CLD_C2_5 = 354,

        /// <summary>
        /// For second channel of the DIY-devices led 6
        /// </summary>
        CLD_C2_6 = 355,

        /// <summary>
        /// For second channel of the DIY-devices led 7
        /// </summary>
        CLD_C2_7 = 356,

        /// <summary>
        /// For second channel of the DIY-devices led 8
        /// </summary>
        CLD_C2_8 = 357,

        /// <summary>
        /// For second channel of the DIY-devices led 9
        /// </summary>
        CLD_C2_9 = 358,

        /// <summary>
        /// For second channel of the DIY-devices led 10
        /// </summary>
        CLD_C2_10 = 359,

        /// <summary>
        /// For second channel of the DIY-devices led 11
        /// </summary>
        CLD_C2_11 = 360,

        /// <summary>
        /// For second channel of the DIY-devices led 12
        /// </summary>
        CLD_C2_12 = 361,

        /// <summary>
        /// For second channel of the DIY-devices led 13
        /// </summary>
        CLD_C2_13 = 362,

        /// <summary>
        /// For second channel of the DIY-devices led 14
        /// </summary>
        CLD_C2_14 = 363,

        /// <summary>
        /// For second channel of the DIY-devices led 15
        /// </summary>
        CLD_C2_15 = 364,

        /// <summary>
        /// For second channel of the DIY-devices led 16
        /// </summary>
        CLD_C2_16 = 365,

        /// <summary>
        /// For second channel of the DIY-devices led 17
        /// </summary>
        CLD_C2_17 = 366,

        /// <summary>
        /// For second channel of the DIY-devices led 18
        /// </summary>
        CLD_C2_18 = 367,

        /// <summary>
        /// For second channel of the DIY-devices led 19
        /// </summary>
        CLD_C2_19 = 368,

        /// <summary>
        /// For second channel of the DIY-devices led 20
        /// </summary>
        CLD_C2_20 = 369,

        /// <summary>
        /// For second channel of the DIY-devices led 21
        /// </summary>
        CLD_C2_21 = 370,

        /// <summary>
        /// For second channel of the DIY-devices led 22
        /// </summary>
        CLD_C2_22 = 371,

        /// <summary>
        /// For second channel of the DIY-devices led 23
        /// </summary>
        CLD_C2_23 = 372,

        /// <summary>
        /// For second channel of the DIY-devices led 24
        /// </summary>
        CLD_C2_24 = 373,

        /// <summary>
        /// For second channel of the DIY-devices led 25
        /// </summary>
        CLD_C2_25 = 374,

        /// <summary>
        /// For second channel of the DIY-devices led 26
        /// </summary>
        CLD_C2_26 = 375,

        /// <summary>
        /// For second channel of the DIY-devices led 27
        /// </summary>
        CLD_C2_27 = 376,

        /// <summary>
        /// For second channel of the DIY-devices led 28
        /// </summary>
        CLD_C2_28 = 377,

        /// <summary>
        /// For second channel of the DIY-devices led 29
        /// </summary>
        CLD_C2_29 = 378,

        /// <summary>
        /// For second channel of the DIY-devices led 30
        /// </summary>
        CLD_C2_30 = 379,

        /// <summary>
        /// For second channel of the DIY-devices led 31
        /// </summary>
        CLD_C2_31 = 380,

        /// <summary>
        /// For second channel of the DIY-devices led 32
        /// </summary>
        CLD_C2_32 = 381,

        /// <summary>
        /// For second channel of the DIY-devices led 33
        /// </summary>
        CLD_C2_33 = 382,

        /// <summary>
        /// For second channel of the DIY-devices led 34
        /// </summary>
        CLD_C2_34 = 383,

        /// <summary>
        /// For second channel of the DIY-devices led 35
        /// </summary>
        CLD_C2_35 = 384,

        /// <summary>
        /// For second channel of the DIY-devices led 36
        /// </summary>
        CLD_C2_36 = 385,

        /// <summary>
        /// For second channel of the DIY-devices led 37
        /// </summary>
        CLD_C2_37 = 386,

        /// <summary>
        /// For second channel of the DIY-devices led 38
        /// </summary>
        CLD_C2_38 = 387,

        /// <summary>
        /// For second channel of the DIY-devices led 39
        /// </summary>
        CLD_C2_39 = 388,

        /// <summary>
        /// For second channel of the DIY-devices led 40
        /// </summary>
        CLD_C2_40 = 389,

        /// <summary>
        /// For second channel of the DIY-devices led 41
        /// </summary>
        CLD_C2_41 = 390,

        /// <summary>
        /// For second channel of the DIY-devices led 42
        /// </summary>
        CLD_C2_42 = 391,

        /// <summary>
        /// For second channel of the DIY-devices led 43
        /// </summary>
        CLD_C2_43 = 392,

        /// <summary>
        /// For second channel of the DIY-devices led 44
        /// </summary>
        CLD_C2_44 = 393,

        /// <summary>
        /// For second channel of the DIY-devices led 45
        /// </summary>
        CLD_C2_45 = 394,

        /// <summary>
        /// For second channel of the DIY-devices led 46
        /// </summary>
        CLD_C2_46 = 395,

        /// <summary>
        /// For second channel of the DIY-devices led 47
        /// </summary>
        CLD_C2_47 = 396,

        /// <summary>
        /// For second channel of the DIY-devices led 48
        /// </summary>
        CLD_C2_48 = 397,

        /// <summary>
        /// For second channel of the DIY-devices led 49
        /// </summary>
        CLD_C2_49 = 398,

        /// <summary>
        /// For second channel of the DIY-devices led 50
        /// </summary>
        CLD_C2_50 = 399,

        /// <summary>
        /// For second channel of the DIY-devices led 51
        /// </summary>
        CLD_C2_51 = 400,

        /// <summary>
        /// For second channel of the DIY-devices led 52
        /// </summary>
        CLD_C2_52 = 401,

        /// <summary>
        /// For second channel of the DIY-devices led 53
        /// </summary>
        CLD_C2_53 = 402,

        /// <summary>
        /// For second channel of the DIY-devices led 54
        /// </summary>
        CLD_C2_54 = 403,

        /// <summary>
        /// For second channel of the DIY-devices led 55
        /// </summary>
        CLD_C2_55 = 404,

        /// <summary>
        /// For second channel of the DIY-devices led 56
        /// </summary>
        CLD_C2_56 = 405,

        /// <summary>
        /// For second channel of the DIY-devices led 57
        /// </summary>
        CLD_C2_57 = 406,

        /// <summary>
        /// For second channel of the DIY-devices led 58
        /// </summary>
        CLD_C2_58 = 407,

        /// <summary>
        /// For second channel of the DIY-devices led 59
        /// </summary>
        CLD_C2_59 = 408,

        /// <summary>
        /// For second channel of the DIY-devices led 60
        /// </summary>
        CLD_C2_60 = 409,

        /// <summary>
        /// For second channel of the DIY-devices led 61
        /// </summary>
        CLD_C2_61 = 410,

        /// <summary>
        /// For second channel of the DIY-devices led 62
        /// </summary>
        CLD_C2_62 = 411,

        /// <summary>
        /// For second channel of the DIY-devices led 63
        /// </summary>
        CLD_C2_63 = 412,

        /// <summary>
        /// For second channel of the DIY-devices led 64
        /// </summary>
        CLD_C2_64 = 413,

        /// <summary>
        /// For second channel of the DIY-devices led 65
        /// </summary>
        CLD_C2_65 = 414,

        /// <summary>
        /// For second channel of the DIY-devices led 66
        /// </summary>
        CLD_C2_66 = 415,

        /// <summary>
        /// For second channel of the DIY-devices led 67
        /// </summary>
        CLD_C2_67 = 416,

        /// <summary>
        /// For second channel of the DIY-devices led 68
        /// </summary>
        CLD_C2_68 = 417,

        /// <summary>
        /// For second channel of the DIY-devices led 69
        /// </summary>
        CLD_C2_69 = 418,

        /// <summary>
        /// For second channel of the DIY-devices led 70
        /// </summary>
        CLD_C2_70 = 419,

        /// <summary>
        /// For second channel of the DIY-devices led 71
        /// </summary>
        CLD_C2_71 = 420,

        /// <summary>
        /// For second channel of the DIY-devices led 72
        /// </summary>
        CLD_C2_72 = 421,

        /// <summary>
        /// For second channel of the DIY-devices led 73
        /// </summary>
        CLD_C2_73 = 422,

        /// <summary>
        /// For second channel of the DIY-devices led 74
        /// </summary>
        CLD_C2_74 = 423,

        /// <summary>
        /// For second channel of the DIY-devices led 75
        /// </summary>
        CLD_C2_75 = 424,

        /// <summary>
        /// For second channel of the DIY-devices led 76
        /// </summary>
        CLD_C2_76 = 425,

        /// <summary>
        /// For second channel of the DIY-devices led 77
        /// </summary>
        CLD_C2_77 = 426,

        /// <summary>
        /// For second channel of the DIY-devices led 78
        /// </summary>
        CLD_C2_78 = 427,

        /// <summary>
        /// For second channel of the DIY-devices led 79
        /// </summary>
        CLD_C2_79 = 428,

        /// <summary>
        /// For second channel of the DIY-devices led 80
        /// </summary>
        CLD_C2_80 = 429,

        /// <summary>
        /// For second channel of the DIY-devices led 81
        /// </summary>
        CLD_C2_81 = 430,

        /// <summary>
        /// For second channel of the DIY-devices led 82
        /// </summary>
        CLD_C2_82 = 431,

        /// <summary>
        /// For second channel of the DIY-devices led 83
        /// </summary>
        CLD_C2_83 = 432,

        /// <summary>
        /// For second channel of the DIY-devices led 84
        /// </summary>
        CLD_C2_84 = 433,

        /// <summary>
        /// For second channel of the DIY-devices led 85
        /// </summary>
        CLD_C2_85 = 434,

        /// <summary>
        /// For second channel of the DIY-devices led 86
        /// </summary>
        CLD_C2_86 = 435,

        /// <summary>
        /// For second channel of the DIY-devices led 87
        /// </summary>
        CLD_C2_87 = 436,

        /// <summary>
        /// For second channel of the DIY-devices led 88
        /// </summary>
        CLD_C2_88 = 437,

        /// <summary>
        /// For second channel of the DIY-devices led 89
        /// </summary>
        CLD_C2_89 = 438,

        /// <summary>
        /// For second channel of the DIY-devices led 90
        /// </summary>
        CLD_C2_90 = 439,

        /// <summary>
        /// For second channel of the DIY-devices led 91
        /// </summary>
        CLD_C2_91 = 440,

        /// <summary>
        /// For second channel of the DIY-devices led 92
        /// </summary>
        CLD_C2_92 = 441,

        /// <summary>
        /// For second channel of the DIY-devices led 93
        /// </summary>
        CLD_C2_93 = 442,

        /// <summary>
        /// For second channel of the DIY-devices led 94
        /// </summary>
        CLD_C2_94 = 443,

        /// <summary>
        /// For second channel of the DIY-devices led 95
        /// </summary>
        CLD_C2_95 = 444,

        /// <summary>
        /// For second channel of the DIY-devices led 96
        /// </summary>
        CLD_C2_96 = 445,

        /// <summary>
        /// For second channel of the DIY-devices led 97
        /// </summary>
        CLD_C2_97 = 446,

        /// <summary>
        /// For second channel of the DIY-devices led 98
        /// </summary>
        CLD_C2_98 = 447,

        /// <summary>
        /// For second channel of the DIY-devices led 99
        /// </summary>
        CLD_C2_99 = 448,

        /// <summary>
        /// For second channel of the DIY-devices led 100
        /// </summary>
        CLD_C2_100 = 449,

        /// <summary>
        /// For second channel of the DIY-devices led 101
        /// </summary>
        CLD_C2_101 = 450,

        /// <summary>
        /// For second channel of the DIY-devices led 102
        /// </summary>
        CLD_C2_102 = 451,

        /// <summary>
        /// For second channel of the DIY-devices led 103
        /// </summary>
        CLD_C2_103 = 452,

        /// <summary>
        /// For second channel of the DIY-devices led 104
        /// </summary>
        CLD_C2_104 = 453,

        /// <summary>
        /// For second channel of the DIY-devices led 105
        /// </summary>
        CLD_C2_105 = 454,

        /// <summary>
        /// For second channel of the DIY-devices led 106
        /// </summary>
        CLD_C2_106 = 455,

        /// <summary>
        /// For second channel of the DIY-devices led 107
        /// </summary>
        CLD_C2_107 = 456,

        /// <summary>
        /// For second channel of the DIY-devices led 108
        /// </summary>
        CLD_C2_108 = 457,

        /// <summary>
        /// For second channel of the DIY-devices led 109
        /// </summary>
        CLD_C2_109 = 458,

        /// <summary>
        /// For second channel of the DIY-devices led 110
        /// </summary>
        CLD_C2_110 = 459,

        /// <summary>
        /// For second channel of the DIY-devices led 111
        /// </summary>
        CLD_C2_111 = 460,

        /// <summary>
        /// For second channel of the DIY-devices led 112
        /// </summary>
        CLD_C2_112 = 461,

        /// <summary>
        /// For second channel of the DIY-devices led 113
        /// </summary>
        CLD_C2_113 = 462,

        /// <summary>
        /// For second channel of the DIY-devices led 114
        /// </summary>
        CLD_C2_114 = 463,

        /// <summary>
        /// For second channel of the DIY-devices led 115
        /// </summary>
        CLD_C2_115 = 464,

        /// <summary>
        /// For second channel of the DIY-devices led 116
        /// </summary>
        CLD_C2_116 = 465,

        /// <summary>
        /// For second channel of the DIY-devices led 117
        /// </summary>
        CLD_C2_117 = 466,

        /// <summary>
        /// For second channel of the DIY-devices led 118
        /// </summary>
        CLD_C2_118 = 467,

        /// <summary>
        /// For second channel of the DIY-devices led 119
        /// </summary>
        CLD_C2_119 = 468,

        /// <summary>
        /// For second channel of the DIY-devices led 120
        /// </summary>
        CLD_C2_120 = 469,

        /// <summary>
        /// For second channel of the DIY-devices led 121
        /// </summary>
        CLD_C2_121 = 470,

        /// <summary>
        /// For second channel of the DIY-devices led 122
        /// </summary>
        CLD_C2_122 = 471,

        /// <summary>
        /// For second channel of the DIY-devices led 123
        /// </summary>
        CLD_C2_123 = 472,

        /// <summary>
        /// For second channel of the DIY-devices led 124
        /// </summary>
        CLD_C2_124 = 473,

        /// <summary>
        /// For second channel of the DIY-devices led 125
        /// </summary>
        CLD_C2_125 = 474,

        /// <summary>
        /// For second channel of the DIY-devices led 126
        /// </summary>
        CLD_C2_126 = 475,

        /// <summary>
        /// For second channel of the DIY-devices led 127
        /// </summary>
        CLD_C2_127 = 476,

        /// <summary>
        /// For second channel of the DIY-devices led 128
        /// </summary>
        CLD_C2_128 = 477,

        /// <summary>
        /// For second channel of the DIY-devices led 129
        /// </summary>
        CLD_C2_129 = 478,

        /// <summary>
        /// For second channel of the DIY-devices led 130
        /// </summary>
        CLD_C2_130 = 479,

        /// <summary>
        /// For second channel of the DIY-devices led 131
        /// </summary>
        CLD_C2_131 = 480,

        /// <summary>
        /// For second channel of the DIY-devices led 132
        /// </summary>
        CLD_C2_132 = 481,

        /// <summary>
        /// For second channel of the DIY-devices led 133
        /// </summary>
        CLD_C2_133 = 482,

        /// <summary>
        /// For second channel of the DIY-devices led 134
        /// </summary>
        CLD_C2_134 = 483,

        /// <summary>
        /// For second channel of the DIY-devices led 135
        /// </summary>
        CLD_C2_135 = 484,

        /// <summary>
        /// For second channel of the DIY-devices led 136
        /// </summary>
        CLD_C2_136 = 485,

        /// <summary>
        /// For second channel of the DIY-devices led 137
        /// </summary>
        CLD_C2_137 = 486,

        /// <summary>
        /// For second channel of the DIY-devices led 138
        /// </summary>
        CLD_C2_138 = 487,

        /// <summary>
        /// For second channel of the DIY-devices led 139
        /// </summary>
        CLD_C2_139 = 488,

        /// <summary>
        /// For second channel of the DIY-devices led 140
        /// </summary>
        CLD_C2_140 = 489,

        /// <summary>
        /// For second channel of the DIY-devices led 141
        /// </summary>
        CLD_C2_141 = 490,

        /// <summary>
        /// For second channel of the DIY-devices led 142
        /// </summary>
        CLD_C2_142 = 491,

        /// <summary>
        /// For second channel of the DIY-devices led 143
        /// </summary>
        CLD_C2_143 = 492,

        /// <summary>
        /// For second channel of the DIY-devices led 144
        /// </summary>
        CLD_C2_144 = 493,

        /// <summary>
        /// For second channel of the DIY-devices led 145
        /// </summary>
        CLD_C2_145 = 494,

        /// <summary>
        /// For second channel of the DIY-devices led 146
        /// </summary>
        CLD_C2_146 = 495,

        /// <summary>
        /// For second channel of the DIY-devices led 147
        /// </summary>
        CLD_C2_147 = 496,

        /// <summary>
        /// For second channel of the DIY-devices led 148
        /// </summary>
        CLD_C2_148 = 497,

        /// <summary>
        /// For second channel of the DIY-devices led 149
        /// </summary>
        CLD_C2_149 = 498,

        /// <summary>
        /// For second channel of the DIY-devices led 150
        /// </summary>
        CLD_C2_150 = 499,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem1 = 500,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem2 = 501,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem3 = 502,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem4 = 503,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem5 = 504,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem6 = 505,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem7 = 506,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem8 = 507,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem9 = 508,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem10 = 509,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem11 = 510,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem12 = 511,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem13 = 512,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem14 = 513,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem15 = 514,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem16 = 515,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem17 = 516,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem18 = 517,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem19 = 518,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem20 = 519,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem21 = 520,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem22 = 521,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem23 = 522,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem24 = 523,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem25 = 524,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem26 = 525,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem27 = 526,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem28 = 527,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem29 = 528,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem30 = 529,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem31 = 530,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem32 = 531,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem33 = 532,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem34 = 533,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem35 = 534,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem36 = 535,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem37 = 536,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem38 = 537,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem39 = 538,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem40 = 539,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem41 = 540,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem42 = 541,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem43 = 542,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem44 = 543,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem45 = 544,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem46 = 545,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem47 = 546,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem48 = 547,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem49 = 548,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem50 = 549,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem51 = 550,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem52 = 551,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem53 = 552,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem54 = 553,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem55 = 554,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem56 = 555,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem57 = 556,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem58 = 557,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem59 = 558,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem60 = 559,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem61 = 560,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem62 = 561,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem63 = 562,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem64 = 563,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem65 = 564,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem66 = 565,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem67 = 566,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem68 = 567,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem69 = 568,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem70 = 569,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem71 = 570,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem72 = 571,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem73 = 572,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem74 = 573,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem75 = 574,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem76 = 575,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem77 = 576,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem78 = 577,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem79 = 578,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem80 = 579,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem81 = 580,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem82 = 581,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem83 = 582,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem84 = 583,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem85 = 584,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem86 = 585,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem87 = 586,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem88 = 587,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem89 = 588,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem90 = 589,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem91 = 590,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem92 = 591,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem93 = 592,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem94 = 593,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem95 = 594,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem96 = 595,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem97 = 596,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem98 = 597,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem99 = 598,

        /// <summary>
        /// Reserved for custom led
        /// </summary>
        Oem100 = 599,

        /// <summary>
        /// For memory module led 1
        /// </summary>
        CLDRAM_1 = 600,

        /// <summary>
        /// For memory module led 2
        /// </summary>
        CLDRAM_2 = 601,

        /// <summary>
        /// For memory module led 3
        /// </summary>
        CLDRAM_3 = 602,

        /// <summary>
        /// For memory module led 4
        /// </summary>
        CLDRAM_4 = 603,

        /// <summary>
        /// For memory module led 5
        /// </summary>
        CLDRAM_5 = 604,

        /// <summary>
        /// For memory module led 6
        /// </summary>
        CLDRAM_6 = 605,

        /// <summary>
        /// For memory module led 7
        /// </summary>
        CLDRAM_7 = 606,

        /// <summary>
        /// For memory module led 8
        /// </summary>
        CLDRAM_8 = 607,

        /// <summary>
        /// For memory module led 9
        /// </summary>
        CLDRAM_9 = 608,

        /// <summary>
        /// For memory module led 10
        /// </summary>
        CLDRAM_10 = 609,

        /// <summary>
        /// For memory module led 11
        /// </summary>
        CLDRAM_11 = 610,

        /// <summary>
        /// For memory module led 12
        /// </summary>
        CLDRAM_12 = 611,

        /// <summary>
        /// For third channel of the DIY-devices led 1
        /// </summary>
        CLD_C3_1 = 612,

        /// <summary>
        /// For third channel of the DIY-devices led 2
        /// </summary>
        CLD_C3_2 = 613,

        /// <summary>
        /// For third channel of the DIY-devices led 3
        /// </summary>
        CLD_C3_3 = 614,

        /// <summary>
        /// For third channel of the DIY-devices led 4
        /// </summary>
        CLD_C3_4 = 615,

        /// <summary>
        /// For third channel of the DIY-devices led 5
        /// </summary>
        CLD_C3_5 = 616,

        /// <summary>
        /// For third channel of the DIY-devices led 6
        /// </summary>
        CLD_C3_6 = 617,

        /// <summary>
        /// For third channel of the DIY-devices led 7
        /// </summary>
        CLD_C3_7 = 618,

        /// <summary>
        /// For third channel of the DIY-devices led 8
        /// </summary>
        CLD_C3_8 = 619,

        /// <summary>
        /// For third channel of the DIY-devices led 9
        /// </summary>
        CLD_C3_9 = 620,

        /// <summary>
        /// For third channel of the DIY-devices led 10
        /// </summary>
        CLD_C3_10 = 621,

        /// <summary>
        /// For third channel of the DIY-devices led 11
        /// </summary>
        CLD_C3_11 = 622,

        /// <summary>
        /// For third channel of the DIY-devices led 12
        /// </summary>
        CLD_C3_12 = 623,

        /// <summary>
        /// For third channel of the DIY-devices led 13
        /// </summary>
        CLD_C3_13 = 624,

        /// <summary>
        /// For third channel of the DIY-devices led 14
        /// </summary>
        CLD_C3_14 = 625,

        /// <summary>
        /// For third channel of the DIY-devices led 15
        /// </summary>
        CLD_C3_15 = 626,

        /// <summary>
        /// For third channel of the DIY-devices led 16
        /// </summary>
        CLD_C3_16 = 627,

        /// <summary>
        /// For third channel of the DIY-devices led 17
        /// </summary>
        CLD_C3_17 = 628,

        /// <summary>
        /// For third channel of the DIY-devices led 18
        /// </summary>
        CLD_C3_18 = 629,

        /// <summary>
        /// For third channel of the DIY-devices led 19
        /// </summary>
        CLD_C3_19 = 630,

        /// <summary>
        /// For third channel of the DIY-devices led 20
        /// </summary>
        CLD_C3_20 = 631,

        /// <summary>
        /// For third channel of the DIY-devices led 21
        /// </summary>
        CLD_C3_21 = 632,

        /// <summary>
        /// For third channel of the DIY-devices led 22
        /// </summary>
        CLD_C3_22 = 633,

        /// <summary>
        /// For third channel of the DIY-devices led 23
        /// </summary>
        CLD_C3_23 = 634,

        /// <summary>
        /// For third channel of the DIY-devices led 24
        /// </summary>
        CLD_C3_24 = 635,

        /// <summary>
        /// For third channel of the DIY-devices led 25
        /// </summary>
        CLD_C3_25 = 636,

        /// <summary>
        /// For third channel of the DIY-devices led 26
        /// </summary>
        CLD_C3_26 = 637,

        /// <summary>
        /// For third channel of the DIY-devices led 27
        /// </summary>
        CLD_C3_27 = 638,

        /// <summary>
        /// For third channel of the DIY-devices led 28
        /// </summary>
        CLD_C3_28 = 639,

        /// <summary>
        /// For third channel of the DIY-devices led 29
        /// </summary>
        CLD_C3_29 = 640,

        /// <summary>
        /// For third channel of the DIY-devices led 30
        /// </summary>
        CLD_C3_30 = 641,

        /// <summary>
        /// For third channel of the DIY-devices led 31
        /// </summary>
        CLD_C3_31 = 642,

        /// <summary>
        /// For third channel of the DIY-devices led 32
        /// </summary>
        CLD_C3_32 = 643,

        /// <summary>
        /// For third channel of the DIY-devices led 33
        /// </summary>
        CLD_C3_33 = 644,

        /// <summary>
        /// For third channel of the DIY-devices led 34
        /// </summary>
        CLD_C3_34 = 645,

        /// <summary>
        /// For third channel of the DIY-devices led 35
        /// </summary>
        CLD_C3_35 = 646,

        /// <summary>
        /// For third channel of the DIY-devices led 36
        /// </summary>
        CLD_C3_36 = 647,

        /// <summary>
        /// For third channel of the DIY-devices led 37
        /// </summary>
        CLD_C3_37 = 648,

        /// <summary>
        /// For third channel of the DIY-devices led 38
        /// </summary>
        CLD_C3_38 = 649,

        /// <summary>
        /// For third channel of the DIY-devices led 39
        /// </summary>
        CLD_C3_39 = 650,

        /// <summary>
        /// For third channel of the DIY-devices led 40
        /// </summary>
        CLD_C3_40 = 651,

        /// <summary>
        /// For third channel of the DIY-devices led 41
        /// </summary>
        CLD_C3_41 = 652,

        /// <summary>
        /// For third channel of the DIY-devices led 42
        /// </summary>
        CLD_C3_42 = 653,

        /// <summary>
        /// For third channel of the DIY-devices led 43
        /// </summary>
        CLD_C3_43 = 654,

        /// <summary>
        /// For third channel of the DIY-devices led 44
        /// </summary>
        CLD_C3_44 = 655,

        /// <summary>
        /// For third channel of the DIY-devices led 45
        /// </summary>
        CLD_C3_45 = 656,

        /// <summary>
        /// For third channel of the DIY-devices led 46
        /// </summary>
        CLD_C3_46 = 657,

        /// <summary>
        /// For third channel of the DIY-devices led 47
        /// </summary>
        CLD_C3_47 = 658,

        /// <summary>
        /// For third channel of the DIY-devices led 48
        /// </summary>
        CLD_C3_48 = 659,

        /// <summary>
        /// For third channel of the DIY-devices led 49
        /// </summary>
        CLD_C3_49 = 660,

        /// <summary>
        /// For third channel of the DIY-devices led 50
        /// </summary>
        CLD_C3_50 = 661,

        /// <summary>
        /// For third channel of the DIY-devices led 51
        /// </summary>
        CLD_C3_51 = 662,

        /// <summary>
        /// For third channel of the DIY-devices led 52
        /// </summary>
        CLD_C3_52 = 663,

        /// <summary>
        /// For third channel of the DIY-devices led 53
        /// </summary>
        CLD_C3_53 = 664,

        /// <summary>
        /// For third channel of the DIY-devices led 54
        /// </summary>
        CLD_C3_54 = 665,

        /// <summary>
        /// For third channel of the DIY-devices led 55
        /// </summary>
        CLD_C3_55 = 666,

        /// <summary>
        /// For third channel of the DIY-devices led 56
        /// </summary>
        CLD_C3_56 = 667,

        /// <summary>
        /// For third channel of the DIY-devices led 57
        /// </summary>
        CLD_C3_57 = 668,

        /// <summary>
        /// For third channel of the DIY-devices led 58
        /// </summary>
        CLD_C3_58 = 669,

        /// <summary>
        /// For third channel of the DIY-devices led 59
        /// </summary>
        CLD_C3_59 = 670,

        /// <summary>
        /// For third channel of the DIY-devices led 60
        /// </summary>
        CLD_C3_60 = 671,

        /// <summary>
        /// For third channel of the DIY-devices led 61
        /// </summary>
        CLD_C3_61 = 672,

        /// <summary>
        /// For third channel of the DIY-devices led 62
        /// </summary>
        CLD_C3_62 = 673,

        /// <summary>
        /// For third channel of the DIY-devices led 63
        /// </summary>
        CLD_C3_63 = 674,

        /// <summary>
        /// For third channel of the DIY-devices led 64
        /// </summary>
        CLD_C3_64 = 675,

        /// <summary>
        /// For third channel of the DIY-devices led 65
        /// </summary>
        CLD_C3_65 = 676,

        /// <summary>
        /// For third channel of the DIY-devices led 66
        /// </summary>
        CLD_C3_66 = 677,

        /// <summary>
        /// For third channel of the DIY-devices led 67
        /// </summary>
        CLD_C3_67 = 678,

        /// <summary>
        /// For third channel of the DIY-devices led 68
        /// </summary>
        CLD_C3_68 = 679,

        /// <summary>
        /// For third channel of the DIY-devices led 69
        /// </summary>
        CLD_C3_69 = 680,

        /// <summary>
        /// For third channel of the DIY-devices led 70
        /// </summary>
        CLD_C3_70 = 681,

        /// <summary>
        /// For third channel of the DIY-devices led 71
        /// </summary>
        CLD_C3_71 = 682,

        /// <summary>
        /// For third channel of the DIY-devices led 72
        /// </summary>
        CLD_C3_72 = 683,

        /// <summary>
        /// For third channel of the DIY-devices led 73
        /// </summary>
        CLD_C3_73 = 684,

        /// <summary>
        /// For third channel of the DIY-devices led 74
        /// </summary>
        CLD_C3_74 = 685,

        /// <summary>
        /// For third channel of the DIY-devices led 75
        /// </summary>
        CLD_C3_75 = 686,

        /// <summary>
        /// For third channel of the DIY-devices led 76
        /// </summary>
        CLD_C3_76 = 687,

        /// <summary>
        /// For third channel of the DIY-devices led 77
        /// </summary>
        CLD_C3_77 = 688,

        /// <summary>
        /// For third channel of the DIY-devices led 78
        /// </summary>
        CLD_C3_78 = 689,

        /// <summary>
        /// For third channel of the DIY-devices led 79
        /// </summary>
        CLD_C3_79 = 690,

        /// <summary>
        /// For third channel of the DIY-devices led 80
        /// </summary>
        CLD_C3_80 = 691,

        /// <summary>
        /// For third channel of the DIY-devices led 81
        /// </summary>
        CLD_C3_81 = 692,

        /// <summary>
        /// For third channel of the DIY-devices led 82
        /// </summary>
        CLD_C3_82 = 693,

        /// <summary>
        /// For third channel of the DIY-devices led 83
        /// </summary>
        CLD_C3_83 = 694,

        /// <summary>
        /// For third channel of the DIY-devices led 84
        /// </summary>
        CLD_C3_84 = 695,

        /// <summary>
        /// For third channel of the DIY-devices led 85
        /// </summary>
        CLD_C3_85 = 696,

        /// <summary>
        /// For third channel of the DIY-devices led 86
        /// </summary>
        CLD_C3_86 = 697,

        /// <summary>
        /// For third channel of the DIY-devices led 87
        /// </summary>
        CLD_C3_87 = 698,

        /// <summary>
        /// For third channel of the DIY-devices led 88
        /// </summary>
        CLD_C3_88 = 699,

        /// <summary>
        /// For third channel of the DIY-devices led 89
        /// </summary>
        CLD_C3_89 = 700,

        /// <summary>
        /// For third channel of the DIY-devices led 90
        /// </summary>
        CLD_C3_90 = 701,

        /// <summary>
        /// For third channel of the DIY-devices led 91
        /// </summary>
        CLD_C3_91 = 702,

        /// <summary>
        /// For third channel of the DIY-devices led 92
        /// </summary>
        CLD_C3_92 = 703,

        /// <summary>
        /// For third channel of the DIY-devices led 93
        /// </summary>
        CLD_C3_93 = 704,

        /// <summary>
        /// For third channel of the DIY-devices led 94
        /// </summary>
        CLD_C3_94 = 705,

        /// <summary>
        /// For third channel of the DIY-devices led 95
        /// </summary>
        CLD_C3_95 = 706,

        /// <summary>
        /// For third channel of the DIY-devices led 96
        /// </summary>
        CLD_C3_96 = 707,

        /// <summary>
        /// For third channel of the DIY-devices led 97
        /// </summary>
        CLD_C3_97 = 708,

        /// <summary>
        /// For third channel of the DIY-devices led 98
        /// </summary>
        CLD_C3_98 = 709,

        /// <summary>
        /// For third channel of the DIY-devices led 99
        /// </summary>
        CLD_C3_99 = 710,

        /// <summary>
        /// For third channel of the DIY-devices led 100
        /// </summary>
        CLD_C3_100 = 711,

        /// <summary>
        /// For third channel of the DIY-devices led 101
        /// </summary>
        CLD_C3_101 = 712,

        /// <summary>
        /// For third channel of the DIY-devices led 102
        /// </summary>
        CLD_C3_102 = 713,

        /// <summary>
        /// For third channel of the DIY-devices led 103
        /// </summary>
        CLD_C3_103 = 714,

        /// <summary>
        /// For third channel of the DIY-devices led 104
        /// </summary>
        CLD_C3_104 = 715,

        /// <summary>
        /// For third channel of the DIY-devices led 105
        /// </summary>
        CLD_C3_105 = 716,

        /// <summary>
        /// For third channel of the DIY-devices led 106
        /// </summary>
        CLD_C3_106 = 717,

        /// <summary>
        /// For third channel of the DIY-devices led 107
        /// </summary>
        CLD_C3_107 = 718,

        /// <summary>
        /// For third channel of the DIY-devices led 108
        /// </summary>
        CLD_C3_108 = 719,

        /// <summary>
        /// For third channel of the DIY-devices led 109
        /// </summary>
        CLD_C3_109 = 720,

        /// <summary>
        /// For third channel of the DIY-devices led 110
        /// </summary>
        CLD_C3_110 = 721,

        /// <summary>
        /// For third channel of the DIY-devices led 111
        /// </summary>
        CLD_C3_111 = 722,

        /// <summary>
        /// For third channel of the DIY-devices led 112
        /// </summary>
        CLD_C3_112 = 723,

        /// <summary>
        /// For third channel of the DIY-devices led 113
        /// </summary>
        CLD_C3_113 = 724,

        /// <summary>
        /// For third channel of the DIY-devices led 114
        /// </summary>
        CLD_C3_114 = 725,

        /// <summary>
        /// For third channel of the DIY-devices led 115
        /// </summary>
        CLD_C3_115 = 726,

        /// <summary>
        /// For third channel of the DIY-devices led 116
        /// </summary>
        CLD_C3_116 = 727,

        /// <summary>
        /// For third channel of the DIY-devices led 117
        /// </summary>
        CLD_C3_117 = 728,

        /// <summary>
        /// For third channel of the DIY-devices led 118
        /// </summary>
        CLD_C3_118 = 729,

        /// <summary>
        /// For third channel of the DIY-devices led 119
        /// </summary>
        CLD_C3_119 = 730,

        /// <summary>
        /// For third channel of the DIY-devices led 120
        /// </summary>
        CLD_C3_120 = 731,

        /// <summary>
        /// For third channel of the DIY-devices led 121
        /// </summary>
        CLD_C3_121 = 732,

        /// <summary>
        /// For third channel of the DIY-devices led 122
        /// </summary>
        CLD_C3_122 = 733,

        /// <summary>
        /// For third channel of the DIY-devices led 123
        /// </summary>
        CLD_C3_123 = 734,

        /// <summary>
        /// For third channel of the DIY-devices led 124
        /// </summary>
        CLD_C3_124 = 735,

        /// <summary>
        /// For third channel of the DIY-devices led 125
        /// </summary>
        CLD_C3_125 = 736,

        /// <summary>
        /// For third channel of the DIY-devices led 126
        /// </summary>
        CLD_C3_126 = 737,

        /// <summary>
        /// For third channel of the DIY-devices led 127
        /// </summary>
        CLD_C3_127 = 738,

        /// <summary>
        /// For third channel of the DIY-devices led 128
        /// </summary>
        CLD_C3_128 = 739,

        /// <summary>
        /// For third channel of the DIY-devices led 129
        /// </summary>
        CLD_C3_129 = 740,

        /// <summary>
        /// For third channel of the DIY-devices led 130
        /// </summary>
        CLD_C3_130 = 741,

        /// <summary>
        /// For third channel of the DIY-devices led 131
        /// </summary>
        CLD_C3_131 = 742,

        /// <summary>
        /// For third channel of the DIY-devices led 132
        /// </summary>
        CLD_C3_132 = 743,

        /// <summary>
        /// For third channel of the DIY-devices led 133
        /// </summary>
        CLD_C3_133 = 744,

        /// <summary>
        /// For third channel of the DIY-devices led 134
        /// </summary>
        CLD_C3_134 = 745,

        /// <summary>
        /// For third channel of the DIY-devices led 135
        /// </summary>
        CLD_C3_135 = 746,

        /// <summary>
        /// For third channel of the DIY-devices led 136
        /// </summary>
        CLD_C3_136 = 747,

        /// <summary>
        /// For third channel of the DIY-devices led 137
        /// </summary>
        CLD_C3_137 = 748,

        /// <summary>
        /// For third channel of the DIY-devices led 138
        /// </summary>
        CLD_C3_138 = 749,

        /// <summary>
        /// For third channel of the DIY-devices led 139
        /// </summary>
        CLD_C3_139 = 750,

        /// <summary>
        /// For third channel of the DIY-devices led 140
        /// </summary>
        CLD_C3_140 = 751,

        /// <summary>
        /// For third channel of the DIY-devices led 141
        /// </summary>
        CLD_C3_141 = 752,

        /// <summary>
        /// For third channel of the DIY-devices led 142
        /// </summary>
        CLD_C3_142 = 753,

        /// <summary>
        /// For third channel of the DIY-devices led 143
        /// </summary>
        CLD_C3_143 = 754,

        /// <summary>
        /// For third channel of the DIY-devices led 144
        /// </summary>
        CLD_C3_144 = 755,

        /// <summary>
        /// For third channel of the DIY-devices led 145
        /// </summary>
        CLD_C3_145 = 756,

        /// <summary>
        /// For third channel of the DIY-devices led 146
        /// </summary>
        CLD_C3_146 = 757,

        /// <summary>
        /// For third channel of the DIY-devices led 147
        /// </summary>
        CLD_C3_147 = 758,

        /// <summary>
        /// For third channel of the DIY-devices led 148
        /// </summary>
        CLD_C3_148 = 759,

        /// <summary>
        /// For third channel of the DIY-devices led 149
        /// </summary>
        CLD_C3_149 = 760,

        /// <summary>
        /// For third channel of the DIY-devices led 150
        /// </summary>
        CLD_C3_150 = 761,

        /// <summary>
        /// For first channel of the liquid coolers led 1
        /// </summary>
        CLLC_C1_1 = 762,

        /// <summary>
        /// For first channel of the liquid coolers led 2
        /// </summary>
        CLLC_C1_2 = 763,

        /// <summary>
        /// For first channel of the liquid coolers led 3
        /// </summary>
        CLLC_C1_3 = 764,

        /// <summary>
        /// For first channel of the liquid coolers led 4
        /// </summary>
        CLLC_C1_4 = 765,

        /// <summary>
        /// For first channel of the liquid coolers led 5
        /// </summary>
        CLLC_C1_5 = 766,

        /// <summary>
        /// For first channel of the liquid coolers led 6
        /// </summary>
        CLLC_C1_6 = 767,

        /// <summary>
        /// For first channel of the liquid coolers led 7
        /// </summary>
        CLLC_C1_7 = 768,

        /// <summary>
        /// For first channel of the liquid coolers led 8
        /// </summary>
        CLLC_C1_8 = 769,

        /// <summary>
        /// For first channel of the liquid coolers led 9
        /// </summary>
        CLLC_C1_9 = 770,

        /// <summary>
        /// For first channel of the liquid coolers led 10
        /// </summary>
        CLLC_C1_10 = 771,

        /// <summary>
        /// For first channel of the liquid coolers led 11
        /// </summary>
        CLLC_C1_11 = 772,

        /// <summary>
        /// For first channel of the liquid coolers led 12
        /// </summary>
        CLLC_C1_12 = 773,

        /// <summary>
        /// For first channel of the liquid coolers led 13
        /// </summary>
        CLLC_C1_13 = 774,

        /// <summary>
        /// For first channel of the liquid coolers led 14
        /// </summary>
        CLLC_C1_14 = 775,

        /// <summary>
        /// For first channel of the liquid coolers led 15
        /// </summary>
        CLLC_C1_15 = 776,

        /// <summary>
        /// For first channel of the liquid coolers led 16
        /// </summary>
        CLLC_C1_16 = 777,

        /// <summary>
        /// For first channel of the liquid coolers led 17
        /// </summary>
        CLLC_C1_17 = 778,

        /// <summary>
        /// For first channel of the liquid coolers led 18
        /// </summary>
        CLLC_C1_18 = 779,

        /// <summary>
        /// For first channel of the liquid coolers led 19
        /// </summary>
        CLLC_C1_19 = 780,

        /// <summary>
        /// For first channel of the liquid coolers led 20
        /// </summary>
        CLLC_C1_20 = 781,

        /// <summary>
        /// For first channel of the liquid coolers led 21
        /// </summary>
        CLLC_C1_21 = 782,

        /// <summary>
        /// For first channel of the liquid coolers led 22
        /// </summary>
        CLLC_C1_22 = 783,

        /// <summary>
        /// For first channel of the liquid coolers led 23
        /// </summary>
        CLLC_C1_23 = 784,

        /// <summary>
        /// For first channel of the liquid coolers led 24
        /// </summary>
        CLLC_C1_24 = 785,

        /// <summary>
        /// For first channel of the liquid coolers led 25
        /// </summary>
        CLLC_C1_25 = 786,

        /// <summary>
        /// For first channel of the liquid coolers led 26
        /// </summary>
        CLLC_C1_26 = 787,

        /// <summary>
        /// For first channel of the liquid coolers led 27
        /// </summary>
        CLLC_C1_27 = 788,

        /// <summary>
        /// For first channel of the liquid coolers led 28
        /// </summary>
        CLLC_C1_28 = 789,

        /// <summary>
        /// For first channel of the liquid coolers led 29
        /// </summary>
        CLLC_C1_29 = 790,

        /// <summary>
        /// For first channel of the liquid coolers led 30
        /// </summary>
        CLLC_C1_30 = 791,

        /// <summary>
        /// For first channel of the liquid coolers led 31
        /// </summary>
        CLLC_C1_31 = 792,

        /// <summary>
        /// For first channel of the liquid coolers led 32
        /// </summary>
        CLLC_C1_32 = 793,

        /// <summary>
        /// For first channel of the liquid coolers led 33
        /// </summary>
        CLLC_C1_33 = 794,

        /// <summary>
        /// For first channel of the liquid coolers led 34
        /// </summary>
        CLLC_C1_34 = 795,

        /// <summary>
        /// For first channel of the liquid coolers led 35
        /// </summary>
        CLLC_C1_35 = 796,

        /// <summary>
        /// For first channel of the liquid coolers led 36
        /// </summary>
        CLLC_C1_36 = 797,

        /// <summary>
        /// For first channel of the liquid coolers led 37
        /// </summary>
        CLLC_C1_37 = 798,

        /// <summary>
        /// For first channel of the liquid coolers led 38
        /// </summary>
        CLLC_C1_38 = 799,

        /// <summary>
        /// For first channel of the liquid coolers led 39
        /// </summary>
        CLLC_C1_39 = 800,

        /// <summary>
        /// For first channel of the liquid coolers led 40
        /// </summary>
        CLLC_C1_40 = 801,

        /// <summary>
        /// For first channel of the liquid coolers led 41
        /// </summary>
        CLLC_C1_41 = 802,

        /// <summary>
        /// For first channel of the liquid coolers led 42
        /// </summary>
        CLLC_C1_42 = 803,

        /// <summary>
        /// For first channel of the liquid coolers led 43
        /// </summary>
        CLLC_C1_43 = 804,

        /// <summary>
        /// For first channel of the liquid coolers led 44
        /// </summary>
        CLLC_C1_44 = 805,

        /// <summary>
        /// For first channel of the liquid coolers led 45
        /// </summary>
        CLLC_C1_45 = 806,

        /// <summary>
        /// For first channel of the liquid coolers led 46
        /// </summary>
        CLLC_C1_46 = 807,

        /// <summary>
        /// For first channel of the liquid coolers led 47
        /// </summary>
        CLLC_C1_47 = 808,

        /// <summary>
        /// For first channel of the liquid coolers led 48
        /// </summary>
        CLLC_C1_48 = 809,

        /// <summary>
        /// For first channel of the liquid coolers led 49
        /// </summary>
        CLLC_C1_49 = 810,

        /// <summary>
        /// For first channel of the liquid coolers led 50
        /// </summary>
        CLLC_C1_50 = 811,

        /// <summary>
        /// For first channel of the liquid coolers led 51
        /// </summary>
        CLLC_C1_51 = 812,

        /// <summary>
        /// For first channel of the liquid coolers led 52
        /// </summary>
        CLLC_C1_52 = 813,

        /// <summary>
        /// For first channel of the liquid coolers led 53
        /// </summary>
        CLLC_C1_53 = 814,

        /// <summary>
        /// For first channel of the liquid coolers led 54
        /// </summary>
        CLLC_C1_54 = 815,

        /// <summary>
        /// For first channel of the liquid coolers led 55
        /// </summary>
        CLLC_C1_55 = 816,

        /// <summary>
        /// For first channel of the liquid coolers led 56
        /// </summary>
        CLLC_C1_56 = 817,

        /// <summary>
        /// For first channel of the liquid coolers led 57
        /// </summary>
        CLLC_C1_57 = 818,

        /// <summary>
        /// For first channel of the liquid coolers led 58
        /// </summary>
        CLLC_C1_58 = 819,

        /// <summary>
        /// For first channel of the liquid coolers led 59
        /// </summary>
        CLLC_C1_59 = 820,

        /// <summary>
        /// For first channel of the liquid coolers led 60
        /// </summary>
        CLLC_C1_60 = 821,

        /// <summary>
        /// For first channel of the liquid coolers led 61
        /// </summary>
        CLLC_C1_61 = 822,

        /// <summary>
        /// For first channel of the liquid coolers led 62
        /// </summary>
        CLLC_C1_62 = 823,

        /// <summary>
        /// For first channel of the liquid coolers led 63
        /// </summary>
        CLLC_C1_63 = 824,

        /// <summary>
        /// For first channel of the liquid coolers led 64
        /// </summary>
        CLLC_C1_64 = 825,

        /// <summary>
        /// For first channel of the liquid coolers led 65
        /// </summary>
        CLLC_C1_65 = 826,

        /// <summary>
        /// For first channel of the liquid coolers led 66
        /// </summary>
        CLLC_C1_66 = 827,

        /// <summary>
        /// For first channel of the liquid coolers led 67
        /// </summary>
        CLLC_C1_67 = 828,

        /// <summary>
        /// For first channel of the liquid coolers led 68
        /// </summary>
        CLLC_C1_68 = 829,

        /// <summary>
        /// For first channel of the liquid coolers led 69
        /// </summary>
        CLLC_C1_69 = 830,

        /// <summary>
        /// For first channel of the liquid coolers led 70
        /// </summary>
        CLLC_C1_70 = 831,

        /// <summary>
        /// For first channel of the liquid coolers led 71
        /// </summary>
        CLLC_C1_71 = 832,

        /// <summary>
        /// For first channel of the liquid coolers led 72
        /// </summary>
        CLLC_C1_72 = 833,

        /// <summary>
        /// For first channel of the liquid coolers led 73
        /// </summary>
        CLLC_C1_73 = 834,

        /// <summary>
        /// For first channel of the liquid coolers led 74
        /// </summary>
        CLLC_C1_74 = 835,

        /// <summary>
        /// For first channel of the liquid coolers led 75
        /// </summary>
        CLLC_C1_75 = 836,

        /// <summary>
        /// For first channel of the liquid coolers led 76
        /// </summary>
        CLLC_C1_76 = 837,

        /// <summary>
        /// For first channel of the liquid coolers led 77
        /// </summary>
        CLLC_C1_77 = 838,

        /// <summary>
        /// For first channel of the liquid coolers led 78
        /// </summary>
        CLLC_C1_78 = 839,

        /// <summary>
        /// For first channel of the liquid coolers led 79
        /// </summary>
        CLLC_C1_79 = 840,

        /// <summary>
        /// For first channel of the liquid coolers led 80
        /// </summary>
        CLLC_C1_80 = 841,

        /// <summary>
        /// For first channel of the liquid coolers led 81
        /// </summary>
        CLLC_C1_81 = 842,

        /// <summary>
        /// For first channel of the liquid coolers led 82
        /// </summary>
        CLLC_C1_82 = 843,

        /// <summary>
        /// For first channel of the liquid coolers led 83
        /// </summary>
        CLLC_C1_83 = 844,

        /// <summary>
        /// For first channel of the liquid coolers led 84
        /// </summary>
        CLLC_C1_84 = 845,

        /// <summary>
        /// For first channel of the liquid coolers led 85
        /// </summary>
        CLLC_C1_85 = 846,

        /// <summary>
        /// For first channel of the liquid coolers led 86
        /// </summary>
        CLLC_C1_86 = 847,

        /// <summary>
        /// For first channel of the liquid coolers led 87
        /// </summary>
        CLLC_C1_87 = 848,

        /// <summary>
        /// For first channel of the liquid coolers led 88
        /// </summary>
        CLLC_C1_88 = 849,

        /// <summary>
        /// For first channel of the liquid coolers led 89
        /// </summary>
        CLLC_C1_89 = 850,

        /// <summary>
        /// For first channel of the liquid coolers led 90
        /// </summary>
        CLLC_C1_90 = 851,

        /// <summary>
        /// For first channel of the liquid coolers led 91
        /// </summary>
        CLLC_C1_91 = 852,

        /// <summary>
        /// For first channel of the liquid coolers led 92
        /// </summary>
        CLLC_C1_92 = 853,

        /// <summary>
        /// For first channel of the liquid coolers led 93
        /// </summary>
        CLLC_C1_93 = 854,

        /// <summary>
        /// For first channel of the liquid coolers led 94
        /// </summary>
        CLLC_C1_94 = 855,

        /// <summary>
        /// For first channel of the liquid coolers led 95
        /// </summary>
        CLLC_C1_95 = 856,

        /// <summary>
        /// For first channel of the liquid coolers led 96
        /// </summary>
        CLLC_C1_96 = 857,

        /// <summary>
        /// For first channel of the liquid coolers led 97
        /// </summary>
        CLLC_C1_97 = 858,

        /// <summary>
        /// For first channel of the liquid coolers led 98
        /// </summary>
        CLLC_C1_98 = 859,

        /// <summary>
        /// For first channel of the liquid coolers led 99
        /// </summary>
        CLLC_C1_99 = 860,

        /// <summary>
        /// For first channel of the liquid coolers led 100
        /// </summary>
        CLLC_C1_100 = 861,

        /// <summary>
        /// For first channel of the liquid coolers led 101
        /// </summary>
        CLLC_C1_101 = 862,

        /// <summary>
        /// For first channel of the liquid coolers led 102
        /// </summary>
        CLLC_C1_102 = 863,

        /// <summary>
        /// For first channel of the liquid coolers led 103
        /// </summary>
        CLLC_C1_103 = 864,

        /// <summary>
        /// For first channel of the liquid coolers led 104
        /// </summary>
        CLLC_C1_104 = 865,

        /// <summary>
        /// For first channel of the liquid coolers led 105
        /// </summary>
        CLLC_C1_105 = 866,

        /// <summary>
        /// For first channel of the liquid coolers led 106
        /// </summary>
        CLLC_C1_106 = 867,

        /// <summary>
        /// For first channel of the liquid coolers led 107
        /// </summary>
        CLLC_C1_107 = 868,

        /// <summary>
        /// For first channel of the liquid coolers led 108
        /// </summary>
        CLLC_C1_108 = 869,

        /// <summary>
        /// For first channel of the liquid coolers led 109
        /// </summary>
        CLLC_C1_109 = 870,

        /// <summary>
        /// For first channel of the liquid coolers led 110
        /// </summary>
        CLLC_C1_110 = 871,

        /// <summary>
        /// For first channel of the liquid coolers led 111
        /// </summary>
        CLLC_C1_111 = 872,

        /// <summary>
        /// For first channel of the liquid coolers led 112
        /// </summary>
        CLLC_C1_112 = 873,

        /// <summary>
        /// For first channel of the liquid coolers led 113
        /// </summary>
        CLLC_C1_113 = 874,

        /// <summary>
        /// For first channel of the liquid coolers led 114
        /// </summary>
        CLLC_C1_114 = 875,

        /// <summary>
        /// For first channel of the liquid coolers led 115
        /// </summary>
        CLLC_C1_115 = 876,

        /// <summary>
        /// For first channel of the liquid coolers led 116
        /// </summary>
        CLLC_C1_116 = 877,

        /// <summary>
        /// For first channel of the liquid coolers led 117
        /// </summary>
        CLLC_C1_117 = 878,

        /// <summary>
        /// For first channel of the liquid coolers led 118
        /// </summary>
        CLLC_C1_118 = 879,

        /// <summary>
        /// For first channel of the liquid coolers led 119
        /// </summary>
        CLLC_C1_119 = 880,

        /// <summary>
        /// For first channel of the liquid coolers led 120
        /// </summary>
        CLLC_C1_120 = 881,

        /// <summary>
        /// For first channel of the liquid coolers led 121
        /// </summary>
        CLLC_C1_121 = 882,

        /// <summary>
        /// For first channel of the liquid coolers led 122
        /// </summary>
        CLLC_C1_122 = 883,

        /// <summary>
        /// For first channel of the liquid coolers led 123
        /// </summary>
        CLLC_C1_123 = 884,

        /// <summary>
        /// For first channel of the liquid coolers led 124
        /// </summary>
        CLLC_C1_124 = 885,

        /// <summary>
        /// For first channel of the liquid coolers led 125
        /// </summary>
        CLLC_C1_125 = 886,

        /// <summary>
        /// For first channel of the liquid coolers led 126
        /// </summary>
        CLLC_C1_126 = 887,

        /// <summary>
        /// For first channel of the liquid coolers led 127
        /// </summary>
        CLLC_C1_127 = 888,

        /// <summary>
        /// For first channel of the liquid coolers led 128
        /// </summary>
        CLLC_C1_128 = 889,

        /// <summary>
        /// For first channel of the liquid coolers led 129
        /// </summary>
        CLLC_C1_129 = 890,

        /// <summary>
        /// For first channel of the liquid coolers led 130
        /// </summary>
        CLLC_C1_130 = 891,

        /// <summary>
        /// For first channel of the liquid coolers led 131
        /// </summary>
        CLLC_C1_131 = 892,

        /// <summary>
        /// For first channel of the liquid coolers led 132
        /// </summary>
        CLLC_C1_132 = 893,

        /// <summary>
        /// For first channel of the liquid coolers led 133
        /// </summary>
        CLLC_C1_133 = 894,

        /// <summary>
        /// For first channel of the liquid coolers led 134
        /// </summary>
        CLLC_C1_134 = 895,

        /// <summary>
        /// For first channel of the liquid coolers led 135
        /// </summary>
        CLLC_C1_135 = 896,

        /// <summary>
        /// For first channel of the liquid coolers led 136
        /// </summary>
        CLLC_C1_136 = 897,

        /// <summary>
        /// For first channel of the liquid coolers led 137
        /// </summary>
        CLLC_C1_137 = 898,

        /// <summary>
        /// For first channel of the liquid coolers led 138
        /// </summary>
        CLLC_C1_138 = 899,

        /// <summary>
        /// For first channel of the liquid coolers led 139
        /// </summary>
        CLLC_C1_139 = 900,

        /// <summary>
        /// For first channel of the liquid coolers led 140
        /// </summary>
        CLLC_C1_140 = 901,

        /// <summary>
        /// For first channel of the liquid coolers led 141
        /// </summary>
        CLLC_C1_141 = 902,

        /// <summary>
        /// For first channel of the liquid coolers led 142
        /// </summary>
        CLLC_C1_142 = 903,

        /// <summary>
        /// For first channel of the liquid coolers led 143
        /// </summary>
        CLLC_C1_143 = 904,

        /// <summary>
        /// For first channel of the liquid coolers led 144
        /// </summary>
        CLLC_C1_144 = 905,

        /// <summary>
        /// For first channel of the liquid coolers led 145
        /// </summary>
        CLLC_C1_145 = 906,

        /// <summary>
        /// For first channel of the liquid coolers led 146
        /// </summary>
        CLLC_C1_146 = 907,

        /// <summary>
        /// For first channel of the liquid coolers led 147
        /// </summary>
        CLLC_C1_147 = 908,

        /// <summary>
        /// For first channel of the liquid coolers led 148
        /// </summary>
        CLLC_C1_148 = 909,

        /// <summary>
        /// For first channel of the liquid coolers led 149
        /// </summary>
        CLLC_C1_149 = 910,

        /// <summary>
        /// For first channel of the liquid coolers led 150
        /// </summary>
        CLLC_C1_150 = 911,

        /// <summary>
        /// The last available led
        /// </summary>
        Last = CLLC_C1_150
    }
}
