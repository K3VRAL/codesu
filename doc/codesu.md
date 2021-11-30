# Name
codesu - An programming interpreter for osu maps.

# Synopsis
codesu [\<args>] \<path> [\<args>] [-d] [-i] [-s] [-a] [-e] [-l] [-r]

# Description
codesu is an interpreter that uses osu maps (specifically .osu files) as a programming language in order to interpret and execute commands line by line. Since there are 4 different gamemodes, there are multiple programmings languages inspired and supported so each mode is unique and different from one another. The best IDE, in order to program in codesu, is the osu!editor in osu!.

# Options
**-d**

Debugs by listing all the available details that each line is producing.

**-i**

Ignores warnings given before executing. These warnings are usually only for comments.

**-s**

Takes a step for each command.

**-a**

Before running the script, it displays all the information of each line, also displaying their command relative to their mode. This will immediately exit the program so the user is able to process the information given by this argument. This can be ignored by using the flag [-r].

**-e**

Before running the script, it gives the user some exporting options. On completion, the program will export then immediately exit. This can be ignored by using the flag [-r].

**-l**

Before running the script, it gives the user some logging options. On completion, the program will log then immediately exit. This can be ignored by using the flag [-r].

**-r**

Forces the to not exit and run regardless of the flags given to exit the program immediately. Once each flag has been executed, the program will run normally.

# Authors
K 3 V R A L