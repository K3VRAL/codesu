# codesu!
codesu! is an interpreter that uses osu maps (specifically .osu files) in order to interpret and execute commands line by line. Since there are 4 different gamemodes, there are (will be) multiple programmings languages inspired and supported so each mode is unique and different from one another. The best IDE in order to program in codesu! is the osu!editor in osu!.

## Table of Contents

**[Installation](#install)**

**[Documentation](#doc)**

<a name='install'></a>
## Installation
The already compiled binaries for Linux and Windows should be in the [link here](https://github.com/K3VRAL/codesu/releases).

To compile from Source, for Linux users, you will need GCC, and for Windows users, you will need i686-w64-mingw32-gcc.

For Linux, go to your terminal and type down `git clone https://github.com/K3VRAL/codesu.git`. Once done, you can now compile with `make Linux64` for 64-bit users, or `make Linux32` for 32-bit users. For debugging capabilities (such as using `GDB`) you can simply do `make` which will enable the `-g` flag in GCC; sadly, this is only for 64-bit users so if you intend to use the `-g` flag on a 32-bit machine, it's recommended you edit the Makefile yourself.

<a name='doc'></a>
## Documentation
### [codesu!](./doc/codesu.md)
#### [codesu!ctbrainfuck](./doc/ctbrainfuck.md)