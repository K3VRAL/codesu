#include "../include/ctbrainfuck.h"

objects obj = { NULL, 0 };

// Setting the code up
void runSet(void) {
	int xloc[] = { 0, 85, 171, 256, 341, 427, 512 };

	obj.aho = malloc(sizeof (allHO));
	char *delim = ",";
	for (int i = 0; i < fr.numLines; i++) {
		size_t len = 0;
		char *copy = strdup(*(fr.lines + i));
		char *token = strtok(copy, delim);
		if (token) {
			char **line = malloc(sizeof (char *));
			while (token != NULL) {
				line = realloc(line, (len + 1) * sizeof (char *));
				*(line + len) = strdup(token);
				token = strtok(NULL, delim);
				len++;
			}
			if (len == 6 || len == 8 || len == 11 || len == 7) {
				obj.aho = realloc(obj.aho, (obj.numAho + 1) * sizeof (allHO));

				int rangeX = atoi(*(line + 0));
				int rangeY = atoi(*(line + 1));
				int ncombo = atoi(*(line + 3));
				if (len == 6                        // Circle
					&& (ncombo == 1 || ncombo == 5)
					&& (rangeX >= 0 || rangeX <= 512)
					&& (rangeY >= 0 || rangeY <= 384)) {
					(obj.aho + obj.numAho)->command = 
						rangeY >= xloc[0] && rangeY < xloc[1] ? inpDig :
						rangeY >= xloc[1] && rangeY < xloc[2] ? jmpStr :
						rangeY >= xloc[2] && rangeY < xloc[3] ? pntLft :
						rangeY >= xloc[3] && rangeY < xloc[4] ? inc :
						rangeY >= xloc[4] && rangeY < xloc[5] ? mulc :
						rangeY >= xloc[5] && rangeY <= xloc[6] ? outDig :
						isnull;
					(obj.aho + obj.numAho)->y = rangeY;
					(obj.aho + obj.numAho)->type = circle;
				} else if ((len == 8 || len == 11)  // Slider
					&& (ncombo == 2 || ncombo == 6)
					&& (rangeX >= 0 || rangeX <= 512)
					&& (rangeY >= 0 || rangeY <= 384)) {
					(obj.aho + obj.numAho)->command = 
						rangeY >= xloc[0] && rangeY < xloc[1] ? inpAsc :
						rangeY >= xloc[1] && rangeY < xloc[2] ? jmpEnd :
						rangeY >= xloc[2] && rangeY < xloc[3] ? pntRgt :
						rangeY >= xloc[3] && rangeY < xloc[4] ? dec :
						rangeY >= xloc[4] && rangeY < xloc[5] ? divc :
						rangeY >= xloc[5] && rangeY <= xloc[6] ? outAsc :
						isnull;
					(obj.aho + obj.numAho)->y = rangeY;
					(obj.aho + obj.numAho)->type = slider;
				} else if (len == 7                 // Spinner
					&& (ncombo == 8 || ncombo == 12)
					&& rangeX == 256 && rangeY == 192) {
					(obj.aho + obj.numAho)->command =
						rangeY == xloc[3] ? ran :
						isnull;
					(obj.aho + obj.numAho)->y = rangeY;
					(obj.aho + obj.numAho)->type = spinner;
				} else outputError("There is something wrong with this line:", *(fr.lines + i));
				if ((obj.aho + obj.numAho)->command == isnull) outputError("Command is null because something is wrong with this line:", *(fr.lines + i));
				(obj.aho + obj.numAho)->line = strdup(*(fr.lines + i));
				(obj.aho + obj.numAho)->fileline = *(fr.atLine + i);

				obj.numAho++;
			}

			for (size_t i = 0; i < len; i++) free(*(line + i));
			free(line);
		}
		free(copy);
	}

	int numBracket = 0;
	for (int i = 0; i < obj.numAho; i++) {
		if ((obj.aho + i)->y >= xloc[1] && (obj.aho + i)->y < xloc[2]) {
			switch ((obj.aho + i)->type) {
				case circle:
					numBracket++;
					break;
				case slider:
					numBracket--;
					break;
				case spinner:
					break;
			}
		}
	}
	if (numBracket != 0) outputError("Number of brackets are inconsistent/do not match.", "");
}

// Running the code
void runStart(void) {
	int tick = 1, curline = 0;
	size_t *memory = malloc(USHRT_MAX * sizeof (size_t));
	for (size_t i = 0; i < USHRT_MAX; i++) *(memory + i) = 0;
	size_t memorypos = 0;

	while (curline < obj.numAho) {
		switch ((obj.aho + curline)->command) {
			case inpDig:
				dataPrint("Input (Digit): ");
				char inpD[256];
				scanf("%255s", inpD);
				for (int i = 0; i < strlen(inpD); i++) if (!(inpD[i] >= '0' && inpD[i] <= '9')) outputError("inputDig is not between 0 and 9", (obj.aho + curline)->line);
				*(memory + memorypos) = atoi(inpD);
				break;

			case inpAsc:
				dataPrint("Input (ASCII): ");
				char inpA[256];
				scanf("%255s", inpA);
				for (int i = 0; i < strlen(inpA); i++) *(memory + memorypos) += inpA[i];
				break;

			case jmpStr:
				for (int i = curline + 1, numBracketR = 1; i < obj.numAho; i++) {
					if ((obj.aho + i)->command == jmpStr) numBracketR++;
					else if ((obj.aho + i)->command == jmpEnd) numBracketR--;
					if (numBracketR == 0) {
						if (*(memory + memorypos) == 0) curline = i;
						break;
					}
				}
				break;

			case jmpEnd:
				for (int i = curline - 1, numBracketL = 1; i >= 0; i--) {
					if ((obj.aho + i)->command == jmpStr) numBracketL--;
					else if ((obj.aho + i)->command == jmpEnd) numBracketL++;
					if (numBracketL == 0) {
						if (*(memory + memorypos) != 0) curline = i;
						break;
					}
				}
				break;

			case pntLft:
				if (memorypos == 0) memorypos = USHRT_MAX - 1;
				else memorypos--;
				break;

			case pntRgt:
				if (memorypos == USHRT_MAX) memorypos = 0;
				else memorypos++;
				break;

			case inc:
				if (*(memory + memorypos) == USHRT_MAX) *(memory + memorypos) = 0;
				else *(memory + memorypos) += 1;
				break;

			case dec:
				if (*(memory + memorypos) == 0) *(memory + memorypos) = USHRT_MAX;
				else *(memory + memorypos) -= 1;
				break;

			case ran:
				*(memory + memorypos) = (size_t)(rand() % USHRT_MAX);
				break;

			case mulc:
				*(memory + memorypos) *= 2;
				break;

			case divc:
				*(memory + memorypos) /= 2;
				break;

			case outDig:
				dataDebug("out. [");
				dataPrint("%zu", (size_t)*(memory + memorypos));
				dataDebug("]\n");
				dataStep(!arg.debug);
				break;

			case outAsc:
				dataDebug("out: [");
				dataPrint("%c", (char)*(memory + memorypos));
				dataDebug("]\n");
				dataStep(!arg.debug);
				break;
				
			case isnull:
			default:
				outputError("inputDig is not between 0 and 9", (obj.aho + curline)->line);
				break;
		}
		dataDebug("Tick: %d\tCommand: %s\tMemPos: %d\tMemCell: %d\tFileLine: %d\tLine: %s\n", tick, etsCommand((obj.aho + curline)->command), memorypos, *(memory + memorypos), (obj.aho + curline)->fileline, (obj.aho + curline)->line);
		dataStep(arg.debug);

		curline++;
		tick++;
	}
	free(memory);
}

// If the user uses the `-a` flag or logging all
char *allMode(int i) {
	char *str = "Command: %s\tX: %s\tType: %s\tFileLine: %s\tLine: %s";
	
	char *strCommand = etsCommand((obj.aho + i)->command);
	char strY[10];
	sprintf(strY, "%d", (obj.aho + i)->y);
	char *strType = etsType((obj.aho + i)->type);
	char strFL[10];
	sprintf(strFL, "%d", (obj.aho + i)->fileline);
	char *strLine = (obj.aho + i)->line;

	char *format = malloc((strlen(str) - (2 * 5) + strlen(strFL) + strlen(strY) + strlen(strType) + strlen(strCommand) + strlen(strLine)) * sizeof (char) + 1);
	sprintf(format, str, strCommand, strY, strType, strFL, strLine);
	return format;
}

// Freeing all the data
void freeingMode(void) {
	for (size_t i = 0; i < obj.numAho; i++) free((obj.aho + i)->line);
	free(obj.aho);
}

// Initialising data
Mode ctbInit(void) {
	Mode mode = { &obj, runSet, runStart, allMode, freeingMode };
	return mode;
}