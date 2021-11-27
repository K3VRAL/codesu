#include "programsu.h"

FunCallbackMode *function;
Mode *ctb;

bool programsuRun() {
    ctb = ctbInit();

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

    (((function + i)->target + 0)->function)();   // runSet()
    if (!arg.logging) {
        if (arg.all || arg.exporting || arg.logging) {
            dataExternal();
            if (!arg.run) {
                return true;
            }
        }
    }
    (((function + i)->target + 1)->function)();   // runStart()
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

    free(ctb);
    free(function);
}