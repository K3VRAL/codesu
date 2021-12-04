#ifndef FILES_H
#define FILES_H

#include <assert.h>
#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <string.h>

#include "codesuinfo.h"

typedef struct {
	char *file;
    char **lines;
    int *atLine;
    int numLines;
} FileRelated;

void *xrealloc(void *ptr, size_t size);

void readFileToMemory(char *file);
void freeingMemory();

// int indexAtLine(char *string);
// int indexAtCharOfLine(int index);
// char *lineAtIndex(int index);

extern FileRelated fr;

#endif