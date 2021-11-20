#include "files.h"

FileRelated fr;

int indexAtLine(char *string) {
    char ch;
    char *stringbuild = (char *)malloc(sizeof (char));
    int line = 0;
    bool found = false;
    for (int f = 0; f < fr.lineCount; f++) {
        for (int i = 0; i < (fr.linesInMem + f)->charCount; i++) {
            ch = *((fr.linesInMem + f)->charInMem + i);
            if (ch != '\n') {
                *(stringbuild + strlen(stringbuild)) = ch;
                stringbuild = (char *)realloc(stringbuild, strlen(stringbuild));
            } else {
                if (strcmp(stringbuild, string) == 0) {
                    found = true;
                    break;
                }
                memset(stringbuild, 0, strlen(stringbuild));
                stringbuild = (char *)realloc(stringbuild, 0);
                line++;
            }
        }
        if (found) {
            break;
        }
    }
    free(stringbuild);

    return found ? line : -1;
}

int indexAtCharOfLine(int index) {
    char ch;
    int count = 0, amount = 0;
    bool found = false;
    for (int f = 0; f < fr.lineCount; f++) {
        for (int i = 0; i < (fr.linesInMem + f)->charCount; i++) {
            ch = *((fr.linesInMem + f)->charInMem + i);
            if (ch == '\n') {
                count++;
            }
            if (count == index) {
                found = true;
                amount++;
                break;
            }
            amount++;
        }
        if (found) {
            break;
        }
    }

    return found ? amount : -1;
}

char *lineAtIndex(int index) {
    char ch;
    int count = 0, foundi = 0;
    bool found = false;
    for (int f = 0; f < fr.lineCount; f++) {
        for (int i = 0; i < (fr.linesInMem + f)->charCount; i++) {
            ch = *((fr.linesInMem + f)->charInMem + i);
            if (ch == '\n') {
                count++;
            }
            if (count == index) {
                found = true;
                foundi = i+1;
                break;
            }
        }
        if (found) {
            break;
        }
    }

    char *stringbuild = (char *)malloc(sizeof (char));
    bool getout = false;
    if (found) {
        for (int f = 0; f < fr.lineCount; f++) {
            for (int i = foundi; i < (fr.linesInMem + f)->charCount; i++) {
                ch = *((fr.linesInMem + f)->charInMem + i);
                if (ch != '\n') {
                    *(stringbuild + strlen(stringbuild)) = ch;
                    stringbuild = (char *)realloc(stringbuild, strlen(stringbuild));
                } else {
                    getout = true;
                    break;
                }
            }
            if (getout) {
                break;
            }
        }
    }

    return found ? stringbuild : "";
}