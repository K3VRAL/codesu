# Codesu

Codesu is an interpreter inspired by various programming languages. It takes the map file and turns it into usable code depending on the mode type for programming. You use the osu!editor in order to code.

# Types of programming

`osu!ctbrainfuck` was inspired by Brainfuck

`osu!taikodots` was inspired by ASCIIDots

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

Runs 99bottles.osu while ignoring, debugging, and stepping `dotnet run -p cproject.csproj -dis examples/ctb/99bottles.osu`

Runs fizzbuzz.osu while ignoring, debugging, stepping, displays all, and force running because of displaying all `dotnet run -p -si cproject.csproj examples/ctb/fizzbuzz.osu -d -ar`

TLDR: It doesn't matter if you combine the arguments (but it is recommended)

# Commands and placements

## osu!std

TODO

## osu!taiko

HitSound Value will determine the command of the object.

X-axis and Y-axis will determine the placement of the object in the 0-512 width of x and 0-384 height of y.
If there are overlapping axis, the program will quit.

Time Placement will determine the order of the object with the other objects time placement.

    HitObjects
        HSound              Info
        0           |       Circle: Start.            Slider: End&            Spinner: Mirror\

        2           |       Circle: Verticle|         Slider: Horizontal-     Spinner: Mirror/

        4           |       Circle: Right>            Slider: Left<           Spinner: Crossing+

        6           |       Circle: Upward^           Slider: Downwardsv      Spinner: Outputs""

        8           |       Circle: Reflects(         Slider: Reflects)       Spinner: Duplicates*

        10          |       Circle: SetAddress@       Slider: SetValue#       Spinner: Output$

        12          |       Circle: Input?            Slider: Control Flow~   Spinner: Redirect!

        14          |       Circle: Operations[]      Slider: Operations{}    Spinner: Teleport%

## osu!ctbrainfuck

Y-axis will determine the command of the object.

Time Placement will determine the order of the object with the other objects time placement.

    HitObjects
        Y-val   Command     Info
        0-63    Input       | Circle: Digits,         Slider: ASCII;

        64-127  Jumping     | Circle: Loop[           Slider: Loop]

        128-191 Pointer     | Circle: Left<           Slider: Right>

        192-255 Memorycell  | Circle: Increment+      Slider: Decrement-      Spinner: Random~

        256-319 Memorycell  | Circle: Mulcrement*     Slider: Divcrement/

        320-384 Output      | Circle: Digits.         Slider: ASCII:

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
