#include "args.h"

args arg = { false, false, false, false, false, false, false };
argsExport argExport = { false, false };
argsLog argLog = { false, false, false };

void dialougeEModeHit() {
    printf("[Exporting]\tDo you want to include \"Mode: 2\" and \"[HitObjects]\"? (y/n) ");
}
void dialougeENewCombo() {
    printf("[Exporting]\tDo you want all objects to have a New Combo attribute? (y/n) ");
}

void dialougeLAllObjects() {
    printf("[Logging]\tDo you want to log everything debugged? (y/n) ");
}
void dialougeLDebug() {
    printf("[Logging]\tDo you want to log all objects listed? (y/n) ");
}
void dialougeLEvery() {
    printf("[Logging]\tDo you want to log everything printed? (y/n) ");
}

void assignExportAndLog() {
    if (arg.exporting || arg.logging) {
		int siz = 0;

		if (arg.exporting) siz = siz + 2;
		if (arg.logging) siz = siz + 3;

		if (siz > 1) {
			FunCallback *function = xrealloc(NULL, siz * sizeof (FunCallback));

			if (arg.exporting) {
				(function + 0)->function = dialougeEModeHit;
                (function + 0)->set = &argExport.exportingModeHit;

				(function + 1)->function = dialougeENewCombo;
                (function + 1)->set = &argExport.exportingNewCombo;
			}
			
			// TODO it works but maybe one day optimize this
			if (arg.logging) {
				if (arg.exporting) {
					(function + 2)->function = dialougeLAllObjects;
                    (function + 2)->set = &argLog.loggingAllObjects;

					(function + 3)->function = dialougeLDebug;
                    (function + 3)->set = &argLog.loggingDebug;

					(function + 4)->function = dialougeLEvery;
                    (function + 4)->set = &argLog.loggingEvery;
				} else {
					(function + 0)->function = dialougeLAllObjects;
                    (function + 0)->set = &argLog.loggingAllObjects;

					(function + 1)->function = dialougeLDebug;
                    (function + 1)->set = &argLog.loggingDebug;

					(function + 2)->function = dialougeLEvery;
                    (function + 2)->set = &argLog.loggingEvery;
				}
			}

			char *temp = xrealloc(NULL, sizeof (char));
			for (int i = 0; i < siz; i++) {
				while (!((function + i)->input == 'y' || (function + i)->input == 'n')) {
					(*(function + i)->function)();
					scanf("%s", temp);
					for (size_t i = 0; i < strlen(temp); i++) *(temp + i) = tolower(*(temp + i));
					if (strcmp(temp, "yes") == 0
					 || strcmp(temp, "y") == 0
					 || strcmp(temp, "no") == 0
					 || strcmp(temp, "n") == 0) (function + i)->input = *(temp);
				}
			}
			free(temp);

			for (int i = 0; i < siz; i++) *((function + i)->set) = (function + i)->input == 'y' ? true : false;
            free(function);
		}
	}
}