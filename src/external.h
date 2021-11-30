#ifndef EXTERNAL_H
#define EXTERNAL_H

#include <stdio.h>
#include <stdarg.h>
#include <limits.h>
#include <unistd.h>

#include "mode.h"

#include "lib/args.h"
#include "lib/codesuinfo.h"
#include "lib/files.h"

void dataExternal(Mode mode);
void dataPrint(char *input, ...);
void dataStep();

#endif