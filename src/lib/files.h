#ifndef FILES_H
#define FILES_H

#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <string.h>

typedef struct {
    char *charInMem;
    int charCount;
} FilesRelated;

typedef struct {
	char *file;
    FilesRelated *linesInMem;
    int lineCount;
} FileRelated;

int indexAtLine(char *string);
int indexAtCharOfLine(int index);
char *lineAtIndex(int index);

extern FileRelated fr;

#endif