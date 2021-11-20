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

void initArgs();

void setIgnore(bool set);
void setStep(bool set);
void setRun(bool set);
void setDebug(bool set);
void setAll(bool set);
void setExporting(bool set);
void setLogging(bool set);
bool getIgnore();
bool getStep();
bool getRun();
bool getDebug();
bool getAll();
bool getExporting();
bool getLogging();

void setENewCombo(bool set);
void setEModeHit(bool set);
bool getENewCombo();
bool getEModeHit();
void dialougeENewCombo();
void dialougeEModeHit();

void setLEvery(bool set);
void setLAllObjects(bool set);
void setLDebug(bool set);
bool getLEvery();
bool getLAllObjects();
bool getLDebug();
void dialougeLEvery();
void dialougeLAllObjects();
void dialougeLDebug();

typedef struct {
    void (*function)(void);
    char input;
    void (*setfunction)(bool);
} FunCallback;
void assignExportAndLog();

#endif