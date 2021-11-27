#ifndef CTBRAINFUCK_H
#define CTBRAINFUCK_H

#include <stdbool.h>
#include <string.h>
#include <limits.h>

#include "external.h"

#include "lib/args.h"
#include "lib/files.h"

typedef int yLocation[];

typedef enum {
    isnull = -1,
    // Cirlce           Slider          Spinner
    inpDig      = 0,    inpAsc  = 1,
    loopStart   = 2,    loopEnd = 3,
    left        = 4,    right   = 5,
    inc         = 6,    dec     = 7,    ran = 12,
    mulc        = 8,    divc    = 9,
    outDig      = 10,   outAsc  = 11
} instruction;

typedef struct {
    char *line;
    int fileline;
    int time;
    instruction command;
} allHO;

typedef struct {
    allHO *aho;
    size_t numAho;
} objects;

Mode *ctbInit();
void freeingCTB();

#endif