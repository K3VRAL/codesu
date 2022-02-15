#ifndef ARGS_H
#define ARGS_H

#include <ctype.h>
#include <stdio.h>
#include <stdbool.h>
#include <stdlib.h>
#include <string.h>

#include "files.h"
#include "external.h"

typedef struct {
	bool ignore;
	bool step;
	bool run;
	bool debug;
	bool all;
	bool exporting;
	bool logging;
	bool help;
} args;

typedef struct {
	bool exportingNewCombo;
	bool exportingModeHit;
} argsExport;

typedef struct {
	bool loggingEvery;
	bool loggingAllObjects;
	bool loggingDebug;
} argsLog;

typedef struct {
    void (*function)(void);
    char input;
    bool *set;
} FunCallback;

void assignExportAndLog(void);

extern args arg;
extern argsExport argExport;
extern argsLog argLog;

#endif