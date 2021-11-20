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

void dialougeENewCombo() {
    printf("Do you want all objects to have a New Combo attribute? (y/n) ");
}
void dialougeEModeHit() {
    printf("Do you want to include \"Mode: 2\" and \"[HitObjects]\"? (y/n) ");
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
    if (arg.exporting || arg.logging) {
		int siz = 0;

		if (arg.exporting) {
			siz = siz + 2;
		}
		if (arg.logging) {
			siz = siz + 3;
		}

		if (siz > 1) {
			FunCallback *function = (FunCallback *)malloc(siz * sizeof (FunCallback));

			if (arg.exporting) {
				(function + 0)->function = dialougeENewCombo;
                (function + 0)->set = argExport.exportingNewCombo;

				(function + 1)->function = dialougeEModeHit;
                (function + 1)->set = argExport.exportingModeHit;
			}
			
			// TODO it works but maybe one day optimize this
			if (arg.logging) {
				if (arg.exporting) {
					(function + 2)->function = dialougeLEvery;
                    (function + 2)->set = argLog.loggingEvery;

					(function + 3)->function = dialougeLAllObjects;
                    (function + 3)->set = argLog.loggingAllObjects;

					(function + 4)->function = dialougeLDebug;
                    (function + 4)->set = argLog.loggingDebug;
				} else {
					(function + 0)->function = dialougeLEvery;
                    (function + 0)->set = argLog.loggingEvery;

					(function + 1)->function = dialougeLAllObjects;
                    (function + 1)->set = argLog.loggingAllObjects;

					(function + 2)->function = dialougeLDebug;
                    (function + 2)->set = argLog.loggingDebug;
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
                (function + i)->set = (function + i)->input == 'y' ? true : false;
			}
            free(function);
		}
	}
}