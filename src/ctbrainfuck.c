#include "ctbrainfuck.h"

objects obj = { NULL, 0 };

void runSet() {
    int yloc[] = { 0, 64, 128, 192, 256, 320, 384 };

    obj.aho = xrealloc(NULL, sizeof (allHO));
    char *delim = ",";
    for (int i = 0; i < fr.numLines; i++) {
        char *copy = xrealloc(NULL, strlen(*(fr.lines + i)) + 1);
        size_t len = 0;
        strcpy(copy, *(fr.lines + i));
        char *token = strtok(copy, delim);
        if (token) {
            char **line = xrealloc(NULL, sizeof (char *));
            while (token != NULL) {
                line = xrealloc(line, (len + 1) * sizeof (char *));
                *(line + len) = xrealloc(NULL, strlen(token) + 1);
                strcpy(*(line + len), token);
                token = strtok(NULL, delim);
                len++;
            }
            if (len == 6 || len == 8 || len == 7) {
                obj.aho = xrealloc(obj.aho, (obj.numAho + 1) * sizeof (allHO));

                int rangeX = atoi(*line);
                int rangeY = atoi(*(line + 1));
                int ncombo = atoi(*(line + 3));
                // TODO fix then use this as it is more optimized and readable
                // if ((len == 6 && (ncombo == 1 || ncombo == 5) && (rangeX >= 0 || rangeX <= 512) && (rangeY >= 0 || rangeY <= 384))  // Circle
                // ||  (len == 8 && (ncombo == 2 || ncombo == 6) && (rangeX >= 0 || rangeX <= 512) && (rangeY >= 0 || rangeY <= 384))  // Slider
                // ||  (len == 7 && (ncombo == 8 || ncombo == 12) && rangeX == 256 && rangeY == 192))                                  // Spinner
                // {
                //     (obj.aho + obj.numAho)->command = !(len == 7 && (ncombo == 8 || ncombo == 12) && rangeX == 256 && rangeY == 192) ? (rangeY /* do mathematics on this */) % 2 : ran;
                // } else { perror(*(fr.lines + i)); exit(EXIT_FAILURE); }
                if (len == 6            // Circle
                    && (ncombo == 1 || ncombo == 5)
                    && (rangeX >= 0 || rangeX <= 512)
                    && (rangeY >= 0 || rangeY <= 384)) {
                    (obj.aho + obj.numAho)->command = 
                        rangeY >= yloc[0] && rangeY < yloc[1] ? inpDig :
                        rangeY >= yloc[1] && rangeY < yloc[2] ? jmpStr :
                        rangeY >= yloc[2] && rangeY < yloc[3] ? pntLft :
                        rangeY >= yloc[3] && rangeY < yloc[4] ? inc :
                        rangeY >= yloc[4] && rangeY < yloc[5] ? mulc :
                        rangeY >= yloc[5] && rangeY <= yloc[6] ? outDig :
                        isnull;
                    (obj.aho + obj.numAho)->y = rangeY;
                    (obj.aho + obj.numAho)->type = circle;
                } else if (len == 8     // Slider
                    && (ncombo == 2 || ncombo == 6)
                    && (rangeX >= 0 || rangeX <= 512)
                    && (rangeY >= 0 || rangeY <= 384)) {
                    (obj.aho + obj.numAho)->command = 
                        rangeY >= yloc[0] && rangeY < yloc[1] ? inpAsc :
                        rangeY >= yloc[1] && rangeY < yloc[2] ? jmpEnd :
                        rangeY >= yloc[2] && rangeY < yloc[3] ? pntRgt :
                        rangeY >= yloc[3] && rangeY < yloc[4] ? dec :
                        rangeY >= yloc[4] && rangeY < yloc[5] ? divc :
                        rangeY >= yloc[5] && rangeY <= yloc[6] ? outAsc :
                        isnull;
                    (obj.aho + obj.numAho)->y = rangeY;
                    (obj.aho + obj.numAho)->type = slider;
                } else if (len == 7     // Spinner
                    && (ncombo == 8 || ncombo == 12)
                    && rangeX == 256 && rangeY == 192) {
                    (obj.aho + obj.numAho)->command =
                        rangeY == yloc[3] ? ran :
                        isnull;
                    (obj.aho + obj.numAho)->y = rangeY;
                    (obj.aho + obj.numAho)->type = spinner;
                } else { perror(*(fr.lines + i)); exit(EXIT_FAILURE); }
                if ((obj.aho + obj.numAho)->command == isnull) { perror(*(fr.lines + i)); exit(EXIT_FAILURE); }

                (obj.aho + obj.numAho)->line = xrealloc(NULL, strlen(*(fr.lines + i)) + 1);
                strcpy((obj.aho + obj.numAho)->line, *(fr.lines + i));
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
        if ((obj.aho + i)->y >= yloc[1] && (obj.aho + i)->y < yloc[2]) {
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
    if (numBracket != 0) { perror("numBracket"); exit(EXIT_FAILURE); }
}

void runStart() {
    int tick = 1, curline = 0;
    size_t *memory = xrealloc(NULL, USHRT_MAX * sizeof (size_t));
    for (size_t i = 0; i < USHRT_MAX; i++) *(memory + i) = 0;
    size_t memorypos = 0;

    while (curline < obj.numAho) {
        switch ((obj.aho + curline)->command) {
            case inpDig:
                printf("\nInput (Digit): ");
                char inpD[256];
                scanf("%255s", inpD);
                for (int i = 0; i < strlen(inpD); i++) if (!(inpD[i] >= '0' && inpD[i] <= '9')) { perror("inpDig"); exit(EXIT_FAILURE); }
                *(memory + memorypos) = atoi(inpD);
                break;

            case inpAsc:
                printf("Input (ASCII): \n");
                char inpA[256];
                scanf("%255s", inpA);
                for (int i = 0; i < strlen(inpA); i++) *(memory + memorypos) += inpA[i];
                break;

            case jmpStr:
                for (int i = curline + 1, numBracketR = 1; i < obj.numAho; i++) {
                    if ((obj.aho + i)->command == jmpStr) numBracketR++;
                    else if ((obj.aho + i)->command == jmpEnd) numBracketR--;
                    if (numBracketR == 0 && *(memory + memorypos) == 0) curline = i - 2;
                }
                break;

            case jmpEnd:
                for (int i = curline - 1, numBracketL = 1; i >= 0; i--) {
                    if ((obj.aho + i)->command == jmpStr) numBracketL--;
                    else if ((obj.aho + i)->command == jmpEnd) numBracketL++;
                    if (numBracketL == 0 && *(memory + memorypos) != 0) curline = i + 2;
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
                printf("%lu", (size_t)*(memory + memorypos));
                break;

            case outAsc:
                printf("%c", (char)*(memory + memorypos));
                break;
                
            case isnull:
            default:
                perror("curline"); exit(EXIT_FAILURE);
                break;
        }
        curline++;
        tick++;

        if (tick == 1000) { 
            return;
        }
    }
    free(memory);
}

char *allObjects(int i) {
    char *str = "Line: %s\tFileLine: %s\tY: %s\tType: %s\tCommand: %s";
    
    char *strLine = (obj.aho + i)->line;
    char strFL[10];
    sprintf(strFL, "%d", (obj.aho + i)->fileline);
    char strY[10];
    sprintf(strY, "%d", (obj.aho + i)->y);
    char *strType = etsType((obj.aho + i)->type);
    char *strCommand = etsCommand((obj.aho + i)->command);

    char *format = xrealloc(NULL, (strlen(str) - (2 * 5) + strlen(strLine) + strlen(strFL) + strlen(strY) + strlen(strType) + strlen(strCommand)) * sizeof (char) + 1);
    sprintf(format, str, strLine, strFL, strY, strType, strCommand);
    return format;
}

Mode ctbInit() {
    Mode mode = { &obj, runSet, runStart, allObjects };
    return mode;
}

void freeingCTB() {
    for (size_t i = 0; i < obj.numAho; i++) free((obj.aho + i)->line);
    free(obj.aho);
}