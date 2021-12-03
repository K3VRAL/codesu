#include "programsu.h"

void programsuRun() {
    if (!arg.ignore) {
        bool found = false;
        for (int i = 0; i < fr.numLines; i++) {
            if (strlen(*(fr.lines + i)) == 0 || (*(*(fr.lines + i) + 0) == '/' && *(*(fr.lines + i) + 1) == '/')) {
                printf("Found illegal line at %d:\t%s\n", *(fr.atLine + i), *(fr.lines + i));
                found = true;
            }
        }
        if (found) {
            printf("Make sure you remove these later on or you can export it using the `-e` flag\n");
        }
    }

    FunCallbackMode *funcMode;

    Mode ctb = ctbInit();

    funcMode = xrealloc(NULL, 1 * sizeof (FunCallbackMode));
    (funcMode + 0)->target = ctb;

    int i = -1;
    switch (cinfo) {
        case ocatch:
            i = 0;
            break;
        case ostandard:
        case otaiko:
        case omania:
            printf("Sorry but the mode you've inputted: \'%s\' is currently under production.\n", cinfo == ostandard ?  "Standard" : cinfo == otaiko ? "Taiko" : "Mania");
            return;
        default:
            printf("Sorry but the mode you've inputted might not exist.\n");
            return;
    }

    (funcMode + i)->target.runSet();
    if (arg.all || arg.exporting || arg.logging) {
        dataExternal((funcMode + i)->target);
        if (!arg.run) return;
    }
    (funcMode + i)->target.runStart();

    (funcMode + i)->target.freeMode();
    free(funcMode);

    return;
}