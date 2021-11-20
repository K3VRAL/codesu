#ifndef ARGS_H
#define ARGS_H

#include <stdio.h>
#include <stdbool.h>
#include <stdlib.h>
#include <string.h>

typedef struct {
	bool ignore;
	bool step;
	bool run;
	bool debug;
	bool all;
	bool exporting;
	bool logging;
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
    bool set;
} FunCallback;

void initArgs();

void dialougeENewCombo();
void dialougeEModeHit();
void dialougeLEvery();
void dialougeLAllObjects();
void dialougeLDebug();

void assignExportAndLog();

extern args arg;
extern argsExport argExport;
extern argsLog argLog;

#endif