#include "programsu.h"

void programsuComments() {
    // TODO remove pointer if line is comment
    for (int i = 0; i < fr.numLines; i++) {
        if (*(*(fr.lines + i) + 0) == '/' && *(*(fr.lines + i) + 1) == '/') {
            printf("%s\n", *(fr.lines + i));
        }
    }
}

void programsuRun() {
    // programsuComments();

    switch (cinfo) {
        case ocatch:
            runCatchTheBeat();
            break;
        case ostandard:
        case otaiko:
        case omania:
            printf("Sorry but the mode you've inputted: \'%s\' is currently under production.\n", cinfo == ostandard ?  "Standard" : cinfo == otaiko ? "Taiko" : "Mania");
            break;
        default:
            printf("Sorry but the mode you've inputted might not exist.\n");
            break;
    }
}