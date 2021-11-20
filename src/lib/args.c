#include "args.h"

args arg;
argsExport argExport;
argsLog argLog;

void initArgs() {
    arg.ignore = false;
    arg.step = false;
    arg.run = false;
    arg.debug = false;
    arg.all = false;
    arg.exporting = false;
    arg.logging = false;

    argExport.exportingNewCombo = false;
    argExport.exportingModeHit = false;
    
    argLog.loggingEvery = false;
    argLog.loggingAllObjects = false;
    argLog.loggingDebug = false;
}

void setIgnore(bool set) {
    arg.ignore = set;
}
void setStep(bool set) {
    arg.step = set;
}
void setRun(bool set) {
    arg.run = set;
}
void setDebug(bool set) {
    arg.debug = set;
}
void setAll(bool set) {
    arg.all = set;
}
void setExporting(bool set) {
    arg.exporting = set;
}
void setLogging(bool set) {
    arg.logging = set;
}
bool getIgnore() {
    return arg.ignore;
}
bool getStep() {
    return arg.step;
}
bool getRun() {
    return arg.run;
}
bool getDebug() {
    return arg.debug;
}
bool getAll() {
    return arg.all;
}
bool getExporting() {
    return arg.exporting;
}
bool getLogging() {
    return arg.logging;
}

void setENewCombo(bool set) {
    argExport.exportingNewCombo = set;
}
void setEModeHit(bool set) {
    argExport.exportingModeHit = set;
}
bool getENewCombo() {
    return argExport.exportingNewCombo;
}
bool getEModeHit() {
    return argExport.exportingModeHit;
}
void dialougeENewCombo() {
    printf("Do you want all objects to have a New Combo attribute? (y/n) ");
}
void dialougeEModeHit() {
    printf("Do you want to include \"Mode: 2\" and \"[HitObjects]\"? (y/n) ");
}

void setLEvery(bool set) {
    argLog.loggingEvery = set;
}
void setLAllObjects(bool set) {
    argLog.loggingAllObjects = set;
}
void setLDebug(bool set) {
    argLog.loggingDebug = set;
}
bool getLEvery() {
    return argLog.loggingEvery;
}
bool getLAllObjects() {
    return argLog.loggingAllObjects;
}
bool getLDebug() {
    return argLog.loggingDebug;
}
void dialougeLEvery() {
    printf("Do you want to log everything printed? (y/n) ");
}
void dialougeLAllObjects() {
    printf("Do you want to log everything debugged? (y/n) ");
}
void dialougeLDebug() {
    printf("Do you want to log all objects listed? (y/n) ");
}

void assignExportAndLog() {
    if (getExporting() || getLogging()) {
		int siz = 0;

		if (getExporting()) {
			siz = siz + 2;
		}
		if (getLogging()) {
			siz = siz + 3;
		}

		if (siz > 1) {
			FunCallback *function = (FunCallback *)malloc(siz * sizeof (FunCallback));

			if (getExporting()) {
				(function + 0)->function = dialougeENewCombo;
                (function + 0)->setfunction = setENewCombo;

				(function + 1)->function = dialougeEModeHit;
                (function + 1)->setfunction = setEModeHit;
			}
			
			// TODO it works but maybe one day optimize this
			if (getLogging()) {
				if (getExporting()) {
					(function + 2)->function = dialougeLEvery;
                    (function + 2)->setfunction = setLEvery;

					(function + 3)->function = dialougeLAllObjects;
                    (function + 3)->setfunction = setLAllObjects;

					(function + 4)->function = dialougeLDebug;
                    (function + 4)->setfunction = setLDebug;
				} else {
					(function + 0)->function = dialougeLEvery;
                    (function + 0)->setfunction = setLEvery;

					(function + 1)->function = dialougeLAllObjects;
                    (function + 1)->setfunction = setLAllObjects;

					(function + 2)->function = dialougeLDebug;
                    (function + 2)->setfunction = setLDebug;
				}
			}

			char *temp = (char *)malloc(sizeof (char));
			for (int i = 0; i < siz; i++) {
				while (!((function + i)->input == 'y' || (function + i)->input == 'n')) {
					(*(function + i)->function)();
					scanf("%s", temp);
					if (strcmp(temp, "yes") == 0 || strcmp(temp, "y") == 0
					|| strcmp(temp, "no") == 0 || strcmp(temp, "n") == 0) {
						(function + i)->input = *(temp);
					}
				}
			}
			free(temp);

			for (int i = 0; i < siz; i++) {
                (*(function + i)->setfunction)((function + i)->input == 'y' ? true : false);
			}
            free(function);
		}
	}
}