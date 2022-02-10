#include "../include/args.h"

// Options that the user inputs
args arg = { false, false, false, false, false, false, false, false };
argsExport argExport = { false, false };
argsLog argLog = { false, false, false };

// All dialouge functions that user will see when using the `-e` flag
void dialougeEModeHit() {
    fprintf(restore, "[Exporting]\tDo you want to include \"Mode: 2\" and \"[HitObjects]\"? (y/n) ");
}
void dialougeENewCombo() {
    fprintf(restore, "[Exporting]\tDo you want all objects to have a New Combo attribute? (y/n) ");
}

// All dialouge functions that user will see when using the `-l` flag
void dialougeLAllObjects() {
    fprintf(restore, "[Logging]\tDo you want to log all objects listed? (y/n) ");
}
void dialougeLDebug() {
    fprintf(restore, "[Logging]\tDo you want to log everything debugged? (y/n) ");
}
void dialougeLEvery() {
    fprintf(restore, "[Logging]\tDo you want to log everything printed? (y/n) ");
}

// Assigning, executing and recording dialouge functions
void assignExportAndLog() {
    if (arg.exporting || arg.logging) {
		int siz = 0;

		if (arg.exporting) siz = siz + 2;
		if (arg.logging) siz = siz + 3;

		if (siz > 1) {
			FunCallback *function = malloc(siz * sizeof (FunCallback));

			if (arg.exporting) {
				(function + 0)->function = dialougeEModeHit;
                (function + 0)->set = &argExport.exportingModeHit;

				(function + 1)->function = dialougeENewCombo;
                (function + 1)->set = &argExport.exportingNewCombo;
			}
			
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

			char temp[256];
			for (int i = 0; i < siz; i++) {
				while (!((function + i)->input == 'y' || (function + i)->input == 'n')) {
					(*(function + i)->function)();
					scanf("%255s", temp);
					for (size_t i = 0; i < strlen(temp); i++) temp[i] = tolower(temp[i]);
					if (strcmp(temp, "yes") == 0
					 || strcmp(temp, "y") == 0
					 || strcmp(temp, "no") == 0
					 || strcmp(temp, "n") == 0) (function + i)->input = temp[0];
				}
			}

			for (int i = 0; i < siz; i++) *((function + i)->set) = (function + i)->input == 'y' ? true : false;
            free(function);
		}
	}
}