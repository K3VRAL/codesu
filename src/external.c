#include "external.h"
#include "lib/args.h"
#include "lib/files.h"

void dataExternal(Mode mode) {
    // INIT
    char **all;
    int numAll = 0;
    if (arg.all || argLog.loggingAllObjects) {
        all = xrealloc(NULL, mode.getobject->numAho * sizeof (char *));
        for (int i = 0; i < mode.getobject->numAho; i++) {
            char *temp = mode.returnAll(i);
            *(all + i) = xrealloc(NULL, strlen(temp) * sizeof (char) + 1);
            strcpy(*(all + i), temp);
            numAll++;
            free(temp);
        }
    }

    // EXECUTE
    if (arg.all) {
        printf("[ALL]\n");
        for (int i = 0; i < numAll; i++) printf("%s\n", *(all + i));
    }

    if (arg.exporting) {
        FILE *terminal = stdout;
        char *file = xrealloc(NULL, (strlen(fr.file) + strlen(".export")) * sizeof (char) + 1);
        strcpy(file, fr.file);
        strcat(file, ".export");
        if (access(file, F_OK) == 0) remove(file);
        stdout = fopen(file, "a");
        if (argExport.exportingModeHit) printf("Mode: %d\n[HitObjects]\n", ocatch);
        for (int i = 0; i < mode.getobject->numAho; i++) {
            if (argExport.exportingNewCombo) {
                char curLine[strlen((mode.getobject->aho + i)->line) + 10];
                int numComma = 0;
                bool ncombo = false, finish = false;
                for (int j = 0, k = 0; j < strlen((mode.getobject->aho + i)->line); k++) {
                    if (numComma != 3) curLine[j++] = *((mode.getobject->aho + i)->line + k);
                    else if (!finish) {
                        if (!ncombo) {
                            switch ((mode.getobject->aho + i)->type) {
                                case circle:
                                    curLine[j++] = '5';
                                    break;
                                case slider:
                                    curLine[j++] = '6';
                                    break;
                                case spinner:
                                    curLine[j++] = '1';
                                    curLine[j++] = '2';
                                    break;
                            }
                            ncombo = true;
                        }
                        if (!finish && numComma == 3) {
                            finish = true;
                            curLine[j++] = ',';
                        }
                    }
                    if (*((mode.getobject->aho + i)->line + k) == ',') numComma++;
                }
                printf("%s\n", curLine);
            } else {
                printf("%s\n", (mode.getobject->aho + i)->line);
            }
        }
        fclose(stdout);
        stdout = terminal;
        free(file);
    }

    if (arg.logging) {
        if (argLog.loggingAllObjects) {
            FILE *terminal = stdout;
            char *file = xrealloc(NULL, (strlen(fr.file) + strlen(".all.log")) * sizeof (char) + 1);
            strcpy(file, fr.file);
            strcat(file, ".all.log");
            stdout = fopen(file, "a");
            for (int i = 0; i < numAll; i++) printf("%s\n", *(all + i));
            fclose(stdout);
            stdout = terminal;
            free(file);
        }
        // TODO
        // if (argLog.loggingDebug) {
        // }
        // if (argLog.loggingEvery) {
        // }
    }

    // FREE
    if (arg.all || argLog.loggingAllObjects) {
        for (int i = 0; i < numAll; i++) free(*(all + i));
        free(all);
    }

    argExport.exportingModeHit = false, argExport.exportingNewCombo = false;
    argLog.loggingAllObjects = false, argLog.loggingDebug = false, argLog.loggingEvery = false;
}

void dataDebug(char *input, ...) {
    if (arg.debug) {
        va_list vl;
        va_start(vl, input);
        vprintf(input, vl);
        va_end(vl);
    }
}

void dataStep(bool should) {
    if (arg.step && should && !(argLog.loggingDebug || argLog.loggingEvery)) {
        struct termios oldt, newt;
        tcgetattr(STDIN_FILENO, &oldt);
        newt = oldt;
        newt.c_lflag &= ~(ICANON | ECHO);
        tcsetattr(STDIN_FILENO, TCSANOW, &newt);
        getchar();
        tcsetattr(STDIN_FILENO, TCSANOW, &oldt);
    }
}