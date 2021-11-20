#ifndef CTBRAINFUCK_H
#define CTBRAINFUCK_H

#include <stdbool.h>
#include <limits.h>

#include "lib/files.h"

typedef int yLocation[];

typedef enum {
    normal = 0,
    slider = 1,
    spinner = 2
} objecttype;

typedef enum {
    inpDig = 0, inpAsc = 1,
    loopStart = 2, loopEnd = 3,
    left = 4, right = 5,
    inc = 6, dec = 7,
    mulc = 8, divc = 9,
    outDig = 10, outAsc = 11
} instruction;

typedef struct {
    char *object;
    int fileline;
    objecttype otype;
    int x, y, time;
    instruction command;
} allHO;

void runCatchTheBeat();

#endif