# Codesu

Inspired by various programming languages. You use the osu!editor in order to code. It only uses the Y values of every object so the X values will not affect the outcome of the program. This was done so to prevent stiff movement in osu!ctb. I haven't tested this for osu!std, osu!taiko and (I guess this will not work for) osu!mania. The time value, however, needs to be in order or the code will operate differently than how you expected it to.

# Types of programming

`osu!ctbrainfuck` was inspired by brainfuck while also including more commands

`osu!taiko` TODO

`osu!std` TODO

`osu!mania` TODO

# How to run a file

[.NET 5](https://dotnet.microsoft.com/download/dotnet/5.0) is required before using the program

[Download here](https://github.com/K3VRAL/codesu/releases)

If you downloaded the release file, you would need to use the terminal, `cd` into the release folder and write:

All OSes: `dotnet cproject.dll [file]`

or if you would like to compile from the source code

`dotnet run -p cproject.csproj [file]`

Where `[file]` is exchanged for the `.osu` file that you programmed.

You can also use arguments `-` to get more or less information:

Debugging: `-d`

Ignores warnings: `-i`

Steps: `-s`

Displays all and sorted items: `-a`

Exports (removes all unnecessary comments and newlines): `-e`

Logging: `-l`

Force Run program despite if Displaying All Objects, Exports and/or Logging is enabled: `-r`

## Examples

TODO Replace examples/ctb with examples/std,taiko,mania when developed with those languages

Runs helloworld.osu and ignores all warnings `dotnet run -p cproject.csproj examples/ctb/helloworld.osu -i`

Runs 99bottles.osu while ignoring, debugging, and stepping `dotnet run -p cproject.csproj examples/ctb/99bottles.osu -dis`

Runs fizzbuzz.osu while ignoring, debugging, stepping, displays all, and force running because of displaying all `dotnet run -p cproject.csproj examples/ctb/fizzbuzz.osu -d -si -ar`

TLDR: It doesn't matter if you combine the arguments (but it is recommended)

# Commands and placements

## osu!std

TODO

## osu!taiko

TODO

## osu!ctbrainfuck

    HitObjects
        Y-val   Command     Info
        0-63    Input       | Circle: Digits        Slider: ASCII

        64-127  Jumping     | Circle: [             Slider: ]

        128-191 Pointer     | Circle: Left          Slider: Right

        192-255 Memorycell  | Circle: Increment     Slider: Decrement   Spinner: Random

        256-319 Memorycell  | Circle: Mulcrement    Slider: Divcrement

        320-384 Output      | Circle: Digits        Slider: ASCII

## osu!mania

TODO

# Definitions

## osu!std

TODO

## osu!taiko

TODO

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

## osu!mania

TODO
