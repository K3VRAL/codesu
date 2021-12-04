#ifndef EXTERNAL_H
#define EXTERNAL_H

#include <stdio.h>
#include <stdarg.h>
#include <stdbool.h>
#include <limits.h>
#ifdef __linux__
#include <termios.h>
#endif
#include <unistd.h>

#include "mode.h"

#include "lib/args.h"
#include "lib/codesuinfo.h"
#include "lib/files.h"

void dataExternal(Mode mode);
void dataPrint(char *input, ...);
void dataDebug(char *input, ...);
void dataStep(bool should);

extern FILE *restore;

#endif