#include "../include/programsu.h"

// Finds illegal data and prints it if `-i` flag is active
void findIllegal(void) {
    if (!arg.ignore) {
        bool found = false;
        for (int i = 0; i < fr.numLines; i++) {
            if (strlen(*(fr.lines + i)) == 0 || (*(*(fr.lines + i) + 0) == '/' && *(*(fr.lines + i) + 1) == '/')) {
                fprintf(stdout, "- Found illegal line at %d:\t%s\n", *(fr.atLine + i) + 1, *(fr.lines + i));
                found = true;
            }
        }
        if (found) {
            fprintf(stdout, "- Make sure you remove these later on or you can export it using the `-e` flag\n");
        }
    }
}

// Runs program
void programsuRun(void) {
    findIllegal();

    Mode funcMode;
    switch (cinfo) {
        case ocatch:
            funcMode = ctbInit();
            break;
        case ostandard:
        case otaiko:
        case omania:
            fprintf(stdout, "Sorry but the mode you've inputted: \'%s\' is currently under production.\n", cinfo == ostandard ?  "Standard" : cinfo == otaiko ? "Taiko" : "Mania");
            return;
        default:
            fprintf(stdout, "Sorry but the mode you've inputted might not exist.\n");
            return;
    }
    funcMode.runSet();
    if (arg.all || arg.exporting || arg.logging) {
        dataExternal(funcMode);
        if (!arg.run) return;
    }
    funcMode.runStart();
    funcMode.freeMode();
}