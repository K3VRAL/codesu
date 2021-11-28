#include "programsu.h"

bool programsuRun() {
    FunCallbackMode *function;

    Mode ctb = ctbInit();

    function = xrealloc(NULL, 1 * sizeof (FunCallbackMode));
    (function + 0)->target = ctb;

    int i = -1;
    switch (cinfo) {
        case ocatch:
            i = 0;
            break;
        case ostandard:
        case otaiko:
        case omania:
            printf("Sorry but the mode you've inputted: \'%s\' is currently under production.\n", cinfo == ostandard ?  "Standard" : cinfo == otaiko ? "Taiko" : "Mania");
            return false;
        default:
            printf("Sorry but the mode you've inputted might not exist.\n");
            return false;
    }

    (function + i)->target.runSet();
    if (arg.all || arg.exporting || arg.logging) {
        dataExternal((function + i)->target);
        if (!arg.run) {
            return true;
        }
    }
    (function + i)->target.runStart();

    free(function);
    return true;
}

void freeingProgramsu() {
    switch (cinfo) {
        case ocatch:
            freeingCTB();
            break;
        case ostandard:
        case otaiko:
        case omania:
            break;
    }
}