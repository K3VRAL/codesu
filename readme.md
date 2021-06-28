# Codesu

Inspired by various programming languages. You use the osu!editor in order to code. It only uses the Y values of every object so the X values will not affect the outcome of the program. This was done so to prevent stiff movement in osu!ctb. I haven't tested this for osu!std, osu!taiko and (I guess this will not work for) osu!mania. The time value, however, needs to be in order or the code will operate differently than how you expected it to.

# Types of programming

`osu!ctbrainfuck` was inspired by brainfuck while also including more commands

`osu!taiko` TODO

`osu!std` TODO

`osu!mania` TODO

# How to run a file

[.NET 5](https://dotnet.microsoft.com/download/dotnet/5.0) is required before using the program

If you downloaded the release file, you would need to use the terminal, `cd` into the release folder and write:

All OSes: `dotnet cproject.dll [file]`

or if you would like to compile from the source code

`dotnet run -p cproject.csproj [file]`

Where `[file]` is exchanged for the `.osu` file that you programmed.

You can also use arguments to get more or less information:

Debugging: `-d`

Ignores warnings: `-i`

Steps: `-s`

Displays all and sorted items: `-a`

# Commands and placements

## osu!ctbrainfuck

    HitObjects
        Y-val   Command     Info
        0-63    Input       | Circle: Digits        Slider: ASCII

        64-127  Jumping     | Circle: [             Slider: ]

        128-191 Pointer     | Circle: Left          Slider: Right

        192-255 Memorycell  | Circle: Increment     Slider: Decrement   Spinner: Random

        256-319 Memorycell  | Circle: Mulcrement    Slider: Divcrement

        320-384 Output      | Circle: Digits        Slider: ASCII

## osu!taiko

TODO

## osu!std

TODO

## osu!mania

TODO

# Definitions

## osu!ctbrainfuck

`Digits`      Input will use raw digits               |   Output will use raw digits

`ASCII`       Input will accept ASCII to raw digits   |   Output will convert raw digits to ASCII

`Left`        Move pointer one to the left

`Right`       Move pointer one to the right

`Increment`   Add current cell by 1

`Decrement`   Subtract current cell by 1

`Random`      Will replace current cell by a random number from 0 to 65535

`Mulcrement`  Multiplies current cell by 2

`Divcrement`  Divides current cell by 2

## osu!taiko

TODO

## osu!std

TODO

## osu!mania

TODO

# TODO

All file items will be used to check for all/most relevant and mandatory requirements to function is osu!; mainly osu!ctb (Maybe include some not all? A lot of these ideas may not be included but might?)

    General
        AF      Prints  -   Time to get groovy
        AL      Console -   Time before program starts
        CD      Prints  -   3, 2, 1
        SS      Prints  -   We hitting {Normal, Soft, Drums}
        MD      Prints  -   code!su
        LB/WS   Prints  -   We're in a movie

    Editor
        BM      Prints  -   Found some noticable bookmarks at {Bookmarks}
        GS      Console -   Console Size

    Metadata
        TT/TU   Prints  -   Title: {Title}/{TitleUnicode}
        AS/AU   Prints  -   Artist: {Artist}/{Artist/Unicode}
        CT      Prints  -   Creator: {Creator}
        VR      Prints  -   Version: {Version}

    Difficulty
        HP      Console -   End Program
        CS      Console -   Text Size
        OD      Console -   Text Spacing
        AT      Console -   Char Time Printed

    TimingPoints
        ?
