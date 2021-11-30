#ifndef MODE_H
#define MODE_H

#include <stdio.h>

typedef enum {
    // CTB
    circle = 0,
    slider = 1,
    spinner = 2
} types;

typedef enum {
    isnull = -1,

    // CTB
    // Cirlce       Slider          Spinner
    inpDig  = 0,    inpAsc  = 1,
    jmpStr  = 2,    jmpEnd  = 3,
    pntLft  = 4,    pntRgt  = 5,
    inc     = 6,    dec     = 7,    ran = 12,
    mulc    = 8,    divc    = 9,
    outDig  = 10,   outAsc  = 11,
} instruction;

typedef struct {
    char *line;
    int fileline;

    // Hit Object/Circle
    int x, y, time, hsound, *hsample;
    types type;
    
    // Slider
    int sliders;
    double length;
    // TODO curveType|curvePoints, edgeSounds, edgeSets

    // Spinner
    int endtime;

    instruction command;
} allHO;

typedef struct {
    allHO *aho;
    size_t numAho;
} objects;

typedef struct {
    objects *getobject;
    void (*runSet)(void);
    void (*runStart)(void);
    char *(*returnAll)(int i);
} Mode;

char *etsCommand(instruction com);
char *etsType(types typ);

#endif