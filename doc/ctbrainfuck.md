# Name
codesu!ctbrainfuck - A programming language inspired by Brainfuck so programmers who attempt to learn this would develop a useful sense of appreciation towards modern day programming languages.

# Description
codesu!ctbrainfuck only interprets maps that has their `Mode: ` set to `2`. This will allow for the following commands to be executed if specific conditions of each line is met.

Since codesu!ctbrainfuck is based off of Brainfuck, it will have a lot of similarities tied to it. codesu!ctbrainfuck has all the original commands plus a few extra more to make the process of making applications on this language be a little bit more easier.

Here are some great feedback for this language; "Fuck my brain, I can't read this obfuscated piece of shit", "I would rather spend my entire programming carrier learning PHP then touch this from a mile away with a stick", and "Do you have depression?".

# Resources
All values given below will be based off of the Y-Value of the osu!editor or the osu! file format method for the .osu extension.

## 0 - 63
### Circle | InpDig,
Requires an input from the user that be only an integer. Any other input that is not an integer will give and error and exit the program. The input will be stored onto the current memory cell that the memory pointer is pointing to.
### Slider | InpASC;
Requires an input from the user where any input is allowed. It will then convert that raw ASCII input to an integer value. The input will be stored onto the current memory cell that the memory pointer is pointing to.

## 64 - 127
### Circle | Loop[
Requires at least an equal amount of open bracket loops and closed bracket loops for the program to run. Will go forward in the code to find the ending bracket for that said bracket. Once it has found it, it will make a check to see if the current memorycell is at a value of 0 which, if it is, will place the current program to that line.
### Slider | Loop]
Requires at least an equal amount of open bracket loops and closed bracket loops for the program to run. Will go backwards in the code to find the beginning bracket for that said bracket. Once it has found it, it will make a check to see if the current memorycell is not at a value of 0 which, if it is, will place the current program to that line.

## 128 - 191
### Circle | Left<
Moves the memory pointer from the memory one memory cell to the left.
### Slider | Right>
Moves the memory pointer from the memory one memory cell to the right. 

## 192 - 255
### Circle | Inecrement+
Adds one more to the current memory cell that the memory pointer is pointing to.
### Slider | Decrement-
Subtract one less to the current memory cell that the memory pointer is pointing to.
### Spinner | Random~
Replaces the memory cell that the memory pointer is pointer to by a random number between numbers 0 to 65535.

## 256 - 319
### Circle | Mulcrement*
Multiplies to the current memory cell that the memory pointer is pointing to by two.
### Slider | Divcrement/
Divides to the current memory cell that the memory pointer is pointing to by two.

## 320 - 384
### Circle | OutDig.
This will output the values inside the current memory cell that the memory pointer is pointing to in integers.
### Slider | OutASC:
This will output the values inside the current memory cell that the memory pointer is pointing to in ASCII format.

# Authors
K 3 V R A L