#include "../include/external.h"

// If uses `-a`, `-l` and/or `-e`
void dataExternal(Mode mode) {
	// INIT
	char **all = NULL;
	int numAll = 0;
	if (arg.all || argLog.loggingAllObjects) {
		all = malloc(mode.getobject->numAho * sizeof (char *));
		for (int i = 0; i < mode.getobject->numAho; i++) {
			char *temp = mode.allMode(i);
			*(all + i) = strdup(temp);
			numAll++;
			free(temp);
		}
	}

	// EXECUTE
	// `-a`
	if (arg.all) {
		restore = stdout;
		fprintf(restore, "[ALL] - \n");
		for (int i = 0; i < numAll; i++) fprintf(restore, "%s\n", *(all + i));
		fprintf(restore, "[DONE]\n");
	}

	// `-e`
	if (arg.exporting) {
		restore = stdout;
		fprintf(restore, "[EXPORTING] - ");
		char *file = malloc((strlen(fr.file) + strlen(".export")) * sizeof (char) + 1);
		strcpy(file, fr.file);
		strcat(file, ".export");
		if (access(file, F_OK) == 0) remove(file);
		restore = fopen(file, "a");
		if (argExport.exportingModeHit) fprintf(restore, "Mode: %d\n[HitObjects]\n", ocatch);
		for (int i = 0; i < mode.getobject->numAho; i++) {
			if (argExport.exportingNewCombo) {
				int len = strlen((mode.getobject->aho + i)->line);
				char *curLine = calloc((len + 2 + 1), sizeof(char));
				int numComma = 0;
				for (int j = 0; j < len; j++) {
					if (numComma != 3) strncat(curLine, &*((mode.getobject->aho + i)->line + j), 1);
					else {
						switch ((mode.getobject->aho + i)->type) {
							case circle:
								strcat(curLine, "5");
								break;
							case slider:
								strcat(curLine, "6");
								break;
							case spinner:
								strcat(curLine, "12");
								break;
						}
						while (*((mode.getobject->aho + i)->line + j) != ',') j++;
						strcat(curLine, ",");
					}
					if (*((mode.getobject->aho + i)->line + j) == ',') numComma++;
				}
				fprintf(restore, "%s\n", curLine);
				free(curLine);
			} else {
				fprintf(restore, "%s\n", (mode.getobject->aho + i)->line);
			}
		}
		fclose(restore);
		free(file);
		restore = stdout;
		fprintf(restore, "[DONE]\n");
	}

	// `-l`
	if (arg.logging) {
		if (argLog.loggingAllObjects) {
			restore = stdout;
			fprintf(restore, "[LOGGING] | [ALLOBJECTS] - ");
			char *file = malloc((strlen(fr.file) + strlen(".all.log")) * sizeof (char) + 1);
			strcpy(file, fr.file);
			strcat(file, ".all.log");
			if (access(file, F_OK) == 0) remove(file);
			restore = fopen(file, "a");
			for (int i = 0; i < numAll; i++) fprintf(restore, "%s\n", *(all + i));
			fclose(restore);
			free(file);
			restore = stdout;
			fprintf(restore, "[DONE]\n");
		}
		if (argLog.loggingDebug) {
			restore = stdout;
			fprintf(restore, "[LOGGING] | [DEBUG] - ");
			char *file = malloc((strlen(fr.file) + strlen(".debug.log")) * sizeof (char) + 1);
			strcpy(file, fr.file);
			strcat(file, ".debug.log");
			if (access(file, F_OK) == 0) remove(file);
			restore = fopen(file, "a");
			bool b4debug = arg.debug;
			bool b4step = arg.step;
			arg.debug = true;
			arg.step = false;
			mode.runStart();
			arg.debug = b4debug;
			arg.step = b4step;
			fclose(restore);
			free(file);
			restore = stdout;
			fprintf(restore, "[DONE]\n");
		}
		if (argLog.loggingEvery) {
			fprintf(restore, "[LOGGING] | [EVERY] - ");
			char *file = malloc((strlen(fr.file) + strlen(".every.log")) * sizeof (char) + 1);
			strcpy(file, fr.file);
			strcat(file, ".every.log");
			if (access(file, F_OK) == 0) remove(file);
			restore = fopen(file, "a");
			bool b4debug = arg.debug;
			bool b4step = arg.step;
			arg.debug = false;
			arg.step = false;
			mode.runStart();
			arg.debug = b4debug;
			arg.step = b4step;
			fclose(restore);
			free(file);
			restore = stdout;
			fprintf(restore, "[DONE]\n");
		}
	}

	// FREE
	if ((arg.all || argLog.loggingAllObjects) && all) {
		for (int i = 0; i < numAll; i++) free(*(all + i));
		free(all);
	}

	argExport.exportingModeHit = false, argExport.exportingNewCombo = false;
	argLog.loggingAllObjects = false, argLog.loggingDebug = false, argLog.loggingEvery = false;
}

// Prints data
void dataPrint(char *input, ...) {
	va_list vl;
	va_start(vl, input);
	char format[256];
	vsprintf(format, input, vl);
	fprintf(restore, "%s", format);
	va_end(vl);
}

// Prints for debugging purposes
void dataDebug(char *input, ...) {
	if (arg.debug) {
		va_list vl;
		va_start(vl, input);
		char format[256];
		vsprintf(format, input, vl);
		fprintf(restore, "%s", format);
		va_end(vl);
	}
}

// Prints then waits for step
void dataStep(bool should) {
	if (arg.step && should && !(argLog.loggingDebug || argLog.loggingEvery)) {
#ifdef __linux__
		struct termios oldt, newt;
		tcgetattr(STDIN_FILENO, &oldt);
		newt = oldt;
		newt.c_lflag &= ~(ICANON | ECHO);
		tcsetattr(STDIN_FILENO, TCSANOW, &newt);
		getchar();
		tcsetattr(STDIN_FILENO, TCSANOW, &oldt);
#elif _WIN32
		getchar();
#endif
	}
}