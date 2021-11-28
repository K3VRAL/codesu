#include "external.h"
#include "lib/args.h"

// TODO
void dataExternal(Mode run) {
    // INIT
    // char **all;
    // int numAll = 0;
    // if (arg.all || argLog.loggingAllObjects) {
    //     all = xrealloc(NULL, (numAll + 1) * sizeof (char *));
    //     if (argLog.loggingAllObjects) {
    //         for (int i = 0; i < ; i++) {
    //             *(all + i) = xrealloc(NULL, strlen( ) * sizeof (char) + 1);
    //             strcpy(*(all + i), );
    //             numAll++;
    //         }
    //     }
    // }

    // EXECUTE
    // if (arg.all) {
    //     printf("[ALL]\n");
    //     for (int i = 0; i < numAll; i++) {
    //         printf("%s\n", *(all + i));
    //     }
    //     printf("\n");
    // }

    // if (arg.exporting) {
    //     for (int i = 0; i < ; i++) {
    //         if (argExport.exportingNewCombo) {
    //         }
    //         if (argExport.exportingModeHit) {
    //         }
    //     }
    // }

    // if (arg.logging) {
    //     if (argLog.loggingAllObjects) {
    //         FILE *terminal = stdout;
    //         char *file;
    //         strcat(file, ".all.log");
    //         stdout = fopen(file, "a");
    //         for (int i = 0; i < numAll; i++) {
    //             printf("%s\n", *(all + i));
    //         }
    //         fclose(stdout);
    //         stdout = terminal;
    //     }
    //     if (argLog.loggingEvery) {
    //     }
    //     if (argLog.loggingDebug) {
    //     }
    // }

    // FREE
}

void dataPrint(char *input, ...) {
    if (arg.debug) {
        va_list vl;
        va_start(vl, input);
        vprintf(input, vl);
        va_end(vl);
        dataStep();
    }
}

void dataStep() {
    if (arg.step) {
        int ch;
        while ((ch = getchar()) != 10) { }
    }
}