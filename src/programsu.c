#include "programsu.h"

void programsuComments() {
    FILE *fp;
    fp = fopen(getFilesFile(), "r");
    if (fp == NULL) {
        exit(1);
    }

    // char ch;
    // char *file = (char *)malloc(sizeof (char));
    // *(file + 0) = '\0';
    // char *stringbuild = (char *)malloc(sizeof (char)); // TODO error in this line
    // memset(stringbuild, 0, strlen(stringbuild));
    // while ((ch = fgetc(fp)) != EOF) {
    //     if (ch != '\n') {
    //         *(stringbuild + strlen(stringbuild)) = ch;
    //         stringbuild = (char *)realloc(stringbuild, strlen(stringbuild) + 1);
    //     } else {
    //         char temp[3];
    //         strncpy(temp, stringbuild, 2);
    //         if (strcmp(temp, "//") == 0) {
    //             while (!(ch == EOF || ch == '\n' || ch == '\0')) {
    //                 ch = fgetc(fp);
    //             }
    //         } else if (!(*(stringbuild) == '\0' || *(stringbuild) == '\n')) {
    //             *(stringbuild + strlen(stringbuild)) = '\n';
    //             file = (char *)realloc(file, strlen(file) + strlen(stringbuild) + 1);
    //             strcat(file, stringbuild);
    //         }
    //         memset(stringbuild, 0, strlen(stringbuild));
    //     }
    // }
    // setFilesFIM(file);

    // fclose(fp);
    // free(stringbuild);
    // free(file);

    char ch;
    char *file = (char *)malloc(sizeof (char));
    *(file + 0) = '\0';
    char stringbuild[500];
    memset(stringbuild, 0, sizeof(stringbuild));
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
            } else if (!(stringbuild[0] == '\0' || stringbuild[0] == '\n')) {
                stringbuild[strlen(stringbuild)] = '\n';
                file = (char *)realloc(file, strlen(file) + strlen(stringbuild) + 1);
                strcat(file, stringbuild);
            }
            memset(stringbuild, 0, sizeof(stringbuild));
        }
    }
    setFilesFIM(file);

    free(file);
    fclose(fp);
}

void programsuRun() {
    programsuComments();

    switch (getCodesuMode()) {
        case ocatch:
            runCatchTheBeat();
            break;
        case ostandard:
        case otaiko:
        case omania:
            printf("Sorry but the mode you've inputted: \'%s\' is currently under production.\n", getCodesuMode() == ostandard ?  "Standard" : getCodesuMode() == otaiko ? "Taiko" : "Mania");
            break;
        default:
            printf("Sorry but the mode you've inputted might not exist.\n");
            break;
    }
}