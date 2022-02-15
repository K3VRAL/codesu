#ifndef CTBRAINFUCK_H
#define CTBRAINFUCK_H

#include <stdbool.h>
#include <string.h>
#include <limits.h>

#include "external.h"
#include "args.h"
#include "files.h"

Mode ctbInit(void);
void freeingCTB(void);

#endif