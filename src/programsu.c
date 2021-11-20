#include "programsu.h"
#include <stdio.h>
#include <stdlib.h>

void programsuComments() {
    FILE *fp;
    fp = fopen(fr.file, "r");
    if (fp == NULL) {
        exit(1);
    }

    char ch;
    fr.linesInMem = (FilesRelated *)malloc(sizeof (FilesRelated));
    fr.lineCount = 1;
    char stringbuild[500];
    while ((ch = fgetc(fp)) != EOF) {
        if (ch != '\n') {
            stringbuild[strlen(stringbuild)] = ch;
        } else {
            char temp[3];
            strncpy(temp, stringbuild, 2);
            if (strcmp(temp, "//") == 0) {
                while (!(ch == EOF || ch == '\n' || ch == '\0')) {
                    ch = fgetc(fp);
                }
            } else if (!(stringbuild[0] == '\n')) {
                fr.lineCount++;
                fr.linesInMem = (FilesRelated *)realloc(fr.linesInMem, fr.lineCount + sizeof (FilesRelated));
                (fr.linesInMem + fr.lineCount)->charInMem = (char *)malloc(strlen(stringbuild) * sizeof (char));
                (fr.linesInMem + fr.lineCount)->charInMem = stringbuild;
                (fr.linesInMem + fr.lineCount)->charCount = strlen(stringbuild);
                printf("INMEM: %s\n", (fr.linesInMem + fr.lineCount)->charInMem);
            }
            memset(stringbuild, 0, strlen(stringbuild));
        }
    }
    // TODO Figure out whats wrong
    fclose(fp);
}

void programsuRun() {
    programsuComments();

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