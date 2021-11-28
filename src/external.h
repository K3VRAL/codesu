#ifndef EXTERNAL_H
#define EXTERNAL_H

#include <stdio.h>
#include <stdarg.h>

#include "lib/args.h"
#include "lib/files.h"

typedef struct {
    void (*runSet)(void);
    void (*runStart)(void);
} Mode;

void dataExternal(Mode run);
void dataPrint(char *input, ...);
void dataStep();

#endif