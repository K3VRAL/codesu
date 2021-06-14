# Codesu

Inspired by Brainfuck but not a carbon copy. You use the osu!editor in order to code. It only uses the Y values of every object so the X values will not affect the outcome of the program. This was done so to prevent movement in osu!ctb. I haven't tested this for osu!std, osu!taiko and (I guess this will not work for) osu!mania. The time value, however, needs to be in order or the code will operate differently than how you expected it to.

# How to use

`dotnet run --project cproject.csproj [file]`

Where `[file]` is exchanged for the `.osu` file that you programmed.

You can also use arguments to get more or less information:

Debugging: `-d`

Ignores warnings: `-i`

Steps: `-s`

Displays all and sorted items: `-a`

# How it works

    HitObjects
        Y-va    Command     Info
        0-63    Input       | Circle: Digits        Slider: ASCII

        64-127  Jumping     | Circle: [             Slider: ]

        128-191 Pointer     | Circle: Left          Slider: Right

        192-255 Memorycell  | Circle: Increment     Slider: Decrement   Spinner: Random

        256-319 Memorycell  | Circle: Mulcrement    Slider: Divcrement

        320-384 Output      | Circle: Digits        Slider: ASCII

# TODO

All file items will be used to check for all/most relevant and mandatory requirements to function is osu!; mainly osu!ctb (Maybe include some not all? A lot of these ideas may not be included but might?)

`General`
        AF      Prints  -   Time to get groovy
        AL      Console -   Time before program starts
        CD      Prints  -   3, 2, 1
        SS      Prints  -   We hitting {Normal, Soft, Drums}
        MD      Prints  -   {2} programming!ctb
        LB/WS   Prints  -   {1} We're in a movie

`Editor`
        BM      Prints  -   Found some noticable bookmarks at {Bookmarks}
        GS      Console -   Console Size

`Metadata`
        TT/TU   Prints  -   Title: {Title}/{TitleUnicode}
        AS/AU   Prints  -   Artist: {Artist}/{Artist/Unicode}
        CT      Prints  -   Creator: {Creator}
        VR      Prints  -   Version: {Version}

`Difficulty`
        HP      Console -   End Program
        CS      Console -   Text Size
        OD      Console -   Text Spacing
        AT      Console -   Char Time Printed

`TimingPoints`
        ?

# Definitions

`Digits`      Input will use raw digits               |   Output will use raw digits

`ASCII`       Input will accept ASCII to raw digits   |   Output will convert raw digits to ASCII

`Left`        Move pointer one to the left

`Right`       Move pointer one to the right

`Increment`   Add current cell by 1

`Decrement`   Subtract current cell by 1

`Random`      Will replace current cell by a random number of 0 or 1

`Mulcrement`  Multiplies current cell by 2

`Divcrement`  Divides current cell by 2