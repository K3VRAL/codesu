# codesu!

codesu! is an interpreter that uses osu maps (specifically .osu files) in order to interpret and execute commands line by line. Since there are 4 different gamemodes, there are (will be) multiple programmings languages inspired and supported so each mode is unique and different from one another. The best IDE in order to program in codesu! is the osu!editor in osu!.

## Table of Contents

**[Installation](#install)**

**[Running Application](#run)**

**[Documentation](#doc)**

<a name='install'></a>

## Installation

The already compiled binaries for Linux and Windows in 64-bit and 32-bit versions should be in the [releases](https://github.com/K3VRAL/codesu/releases) webpage, but it is more recommended that you compile from Source.

## Compiling

Before you can do much, you will need:

- make
- p7zip
- compiler will be specified below

### Linux

To compile from Source on Linux, you will need GCC. Once you have GCC, go to your terminal and type down `git clone https://github.com/K3VRAL/codesu.git`. Once done, go into the folder you cloned and now compile with `make Linux64` for 64-bit users, or `make Linux32` for 32-bit users. For debugging capabilities (such as using `GDB`) you can simply do `make` which will enable the `-g` flag in GCC; this is currently for 64-bit users so if you intend to use the `-g` flag on a 32-bit machine, it's recommended you edit the Makefile yourself.

If you wish to compile from Linux to Windows, you will need mingw-w64-gcc. Once you have mingw-w64-gcc, go to your terminal and compile with `make Windows64` for 64-bit windows users, or `make Windows32` for 32-bit windows users.

### Windows

**I give up trying to get this to work on Windows; try to see if this works but it mostly likely won't. Just use WSL to use this application.**

To compile from Source on Windows, you will need mingw-w64-gcc (but you can just use 'regular GCC' since sometimes thats defaulted to mingw-w64-gcc). Once you have mingw-w64-gcc, go to your command prompt and type down `git clone https://github.com/K3VRAL/codesu.git`. Once done, go into the folder you cloned and now compile with `make Windows64` for 64-bit users, or `make Windows32` for 32-bit users.

If you wish to compile from Windows to Linux, you will need mingw-w64-gcc. Once you have mingw-w64-gcc, go to your command prompt and compile with `make Linux64` for 64-bit linux users, or `make Linux32` for 32-bit linux users.

<a name='run'></a>

## Running Application

To be able to run/interpret any .osu file after installing, you will need to go to your Terminal (for Linux) or Command Prompt (for Windows). You then execute the program by typing `./codesu` for Linux or `./codesu.exe` for Windows **(probably doesn't work)**, and then with the map and any arguments you wish to use for further inspection or easy of use.

<a name='doc'></a>

## Documentation

### [codesu!](./doc/codesu.md)

### [codesu!ctbrainfuck](./doc/ctbrainfuck.md)