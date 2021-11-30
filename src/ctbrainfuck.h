#ifndef CTBRAINFUCK_H
#define CTBRAINFUCK_H

#include <stdbool.h>
#include <string.h>
#include <limits.h>

#include "external.h"

#include "lib/args.h"
#include "lib/files.h"

Mode ctbInit();
void freeingCTB();

#endif