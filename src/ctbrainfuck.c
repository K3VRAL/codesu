#include "ctbrainfuck.h"

yLocation yloc = { 0, 64, 128, 192, 256, 320, 384 };
objects obj = {NULL, 0};

void runSet() {
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
                //     (obj.aho + obj.numAho)->time = atoi(*(line + 2));
                //     (obj.aho + obj.numAho)->command = !(len == 7 && (ncombo == 8 || ncombo == 12) && rangeX == 256 && rangeY == 192) ? (rangeY /* do mathematics on this */) % 2 : ran;
                // } else { perror(*(fr.lines + i)); exit(EXIT_FAILURE); }
                if (len == 6            // Circle
                    && (ncombo == 1 || ncombo == 5)
                    && (rangeX >= 0 || rangeX <= 512)
                    && (rangeY >= 0 || rangeY <= 384)) {
                    (obj.aho + obj.numAho)->time = atoi(*(line + 2));
                    (obj.aho + obj.numAho)->command = 
                        rangeY >= yloc[0] && rangeY < yloc[1] ? inpDig :
                        rangeY >= yloc[1] && rangeY < yloc[2] ? loopStart :
                        rangeY >= yloc[2] && rangeY < yloc[3] ? left :
                        rangeY >= yloc[3] && rangeY < yloc[4] ? inc :
                        rangeY >= yloc[4] && rangeY < yloc[5] ? mulc :
                        rangeY >= yloc[5] && rangeY <= yloc[6] ? outDig :
                        isnull;
                } else if (len == 8     // Slider
                    && (ncombo == 2 || ncombo == 6)
                    && (rangeX >= 0 || rangeX <= 512)
                    && (rangeY >= 0 || rangeY <= 384)) {
                    (obj.aho + obj.numAho)->time = atoi(*(line + 2));
                    (obj.aho + obj.numAho)->command = 
                        rangeY >= yloc[0] && rangeY < yloc[1] ? inpAsc :
                        rangeY >= yloc[1] && rangeY < yloc[2] ? loopEnd :
                        rangeY >= yloc[2] && rangeY < yloc[3] ? right :
                        rangeY >= yloc[3] && rangeY < yloc[4] ? dec :
                        rangeY >= yloc[4] && rangeY < yloc[5] ? divc :
                        rangeY >= yloc[5] && rangeY <= yloc[6] ? outAsc :
                        isnull;
                } else if (len == 7     // Spinner
                    && (ncombo == 8 || ncombo == 12)
                    && rangeX == 256 && rangeY == 192) {
                    (obj.aho + obj.numAho)->time = atoi(*(line + 2));
                    (obj.aho + obj.numAho)->command =
                        rangeY == yloc[3] ? ran :
                        isnull;
                } else { perror(*(fr.lines + i)); exit(EXIT_FAILURE); }
                if ((obj.aho + obj.numAho)->command == isnull) { perror(*(fr.lines + i)); exit(EXIT_FAILURE); }

                (obj.aho + obj.numAho)->line = xrealloc(NULL, strlen(*(fr.lines + i)) + 1);
                strcpy((obj.aho + obj.numAho)->line, *(fr.lines + i));
                (obj.aho + obj.numAho)->fileline = *(fr.atLine + i);

                obj.numAho++;
            }

            for (size_t i = 0; i < len; i++) {
                free(*(line + i));
            }
            free(line);
        }
        free(copy);
    }
}

// TODO this
void runStart() {
    for (size_t i = 0; i < obj.numAho; i++) {
        printf("[%s]\t[%d]\t[%d]\t[%d]\n", (obj.aho + i)->line, (obj.aho + i)->fileline, (obj.aho + i)->time, (obj.aho + i)->command);
    }
}

Mode *ctbInit() {
    Mode *mode = xrealloc(NULL, 2 * sizeof (Mode));
    (mode + 0)->function = runSet;
    (mode + 1)->function = runStart;
    return mode;
}

void freeingCTB() {
    for (size_t i = 0; i < obj.numAho; i++) {
        free((obj.aho + i)->line);
    }
    free(obj.aho);
}