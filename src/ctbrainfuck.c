#include "ctbrainfuck.h"
#include "lib/files.h"
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

yLocation yloc = { 0, 64, 128, 192, 256, 320, 384 };
allHO *aho;

void runSet() {
    char *delim = ",";
    for (int i = 0; i < fr.numLines; i++) {
        char *token = strtok(*(fr.lines + i), delim);
        if (token) {
            // char **line = xrealloc(NULL, sizeof (char *));
            // size_t len = 0;
            while (token != NULL) { // TODO
                printf("[%s] ", token);
                // *(line + len) = xrealloc(NULL, strlen(token) + 1);
                // strcpy(*(line + len), token);
                token = strtok(NULL, delim);
                // len++;
            }
            printf("\n");
        }
    }
}

void runStart() {
}

void runCatchTheBeat() {
    if (!arg.logging) {
        runSet();
        if (arg.all || arg.exporting || arg.logging) {
            if (!arg.run) {
                return;
            }
        }
        runStart();
    }
}