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
    circle = 0,
    slider = 1,
    spinner = 2
} types;

typedef enum {
    isnull = -1,
    // Cirlce       Slider          Spinner
    inpDig  = 0,    inpAsc  = 1,
    jmpStr  = 2,    jmpEnd  = 3,
    pntLft  = 4,    pntRgt  = 5,
    inc     = 6,    dec     = 7,    ran = 12,
    mulc    = 8,    divc    = 9,
    outDig  = 10,   outAsc  = 11
} instruction;

typedef struct {
    char *line;
    int fileline;
    int y;
    types type;
    instruction command;
} allHO;

typedef struct {
    allHO *aho;
    size_t numAho;
} objects;

Mode ctbInit();
void freeingCTB();

#endif