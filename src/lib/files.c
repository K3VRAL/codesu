#include "files.h"

FilesRelated fr;
void initFiles() {
    fr.file = "";
    fr.fileInMemory = "";
}

void setFilesFile(char *set) {
    fr.file = set;
}
void setFilesFIM(char *set) {
    fr.fileInMemory = set;
}

char *getFilesFile() {
    return fr.file;
}
char *getFilesFIM() {
    return fr.fileInMemory;
}

int indexAtLine(char *string) {
    char *fim = getFilesFIM();

    char ch;
    char *stringbuild = (char *)malloc(sizeof (char));
    memset(stringbuild, 0, strlen(stringbuild));
    int line = 0;
    bool found = false;
    for (int i = 0; i < strlen(fim); i++) {
        ch = *(fim + i);
        if (ch != '\n') {
            *(stringbuild + strlen(stringbuild)) = ch;
        } else {
            if (strcmp(stringbuild, string) == 0) {
                found = true;
                break;
            }
            memset(stringbuild, 0, strlen(stringbuild));
            line++;
        }
    }
    free(stringbuild);

    return found ? line : -1;
}

int indexAtCharOfLine(int index) {
    char *fim = getFilesFIM();

    char ch;
    int count = 0, amount = 0;
    bool found = false;
    for (int i = 0; i < strlen(fim); i++) {
        ch = *(fim + i);
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

    return found ? amount : -1;
}

char *lineAtIndex(int index) {
    char *fim = getFilesFIM();

    char ch;
    int count = 0, foundi = 0;
    bool found = false;
    for (int i = 0; i < strlen(fim); i++) {
        ch = *(fim + i);
        if (ch == '\n') {
            count++;
        }
        if (count == index) {
            found = true;
            foundi = i+1;
            break;
        }
    }

    char *stringbuild = (char *)malloc(sizeof (char));
    memset(stringbuild, 0, strlen(stringbuild));
    if (found) {
        for (int i = foundi; i < strlen(fim); i++) {
            ch = *(fim + i);
            if (ch != '\n') {
                *(stringbuild + strlen(stringbuild)) = ch;
                stringbuild = (char *)realloc(stringbuild, strlen(stringbuild) + 1);
            } else {
                break;
            }
        }
    }

    return found ? stringbuild : "";
}