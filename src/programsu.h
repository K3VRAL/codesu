#ifndef PROGRAMSU_H
#define PROGRAMSU_H

#include <stdio.h>
#include <stdbool.h>

#include "ctbrainfuck.h"
#include "external.h"

#include "lib/codesuinfo.h"
#include "lib/files.h"

typedef struct {
    Mode target;
} FunCallbackMode;

bool programsuRun();
void freeingProgramsu();

#endif