# codesu!
codesu! is an interpreter that uses osu maps (specifically .osu files) in order to interpret and execute commands line by line. Since there are 4 different gamemodes, there are (will be) multiple programmings languages inspired and supported so each mode is unique and different from one another. The best IDE in order to program in codesu! is the osu!editor in osu!.

## Table of Contents

**[Installation](#install)**

**[Documentation](#doc)**

<a name='install'></a>
## Installation
The already compiled binaries for Linux and Windows in 64-bit and 32-bit versions should be in the [releases](https://github.com/K3VRAL/codesu/releases) webpage, but it is more recommended that you compile from Source.

To compile from Source on Linux, you will need GCC. Once you have GCC, go to your terminal and type down `git clone https://github.com/K3VRAL/codesu.git`. Once done, go into the folder you cloned and now compile with `make Linux64` for 64-bit users, or `make Linux32` for 32-bit users. For debugging capabilities (such as using `GDB`) you can simply do `make` which will enable the `-g` flag in GCC; this is currently for 64-bit users so if you intend to use the `-g` flag on a 32-bit machine, it's recommended you edit the Makefile yourself.

To compile from Source on Windows, you will need mingw-w64. Once you have mingw-w64, go to your command prompt and type down `git clone https://github.com/K3VRAL/codesu.git`. Once done, go into the folder you cloned and now compile with `make Windows64` for 64-bit users, or `make Windows32` for 32-bit users.

<a name='doc'></a>
## Documentation
### [codesu!](./doc/codesu.md)
#### [codesu!ctbrainfuck](./doc/ctbrainfuck.md)