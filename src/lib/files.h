#ifndef FILES_H
#define FILES_H

#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <string.h>

typedef struct {
	char *file;
    char *fileInMemory;
} FilesRelated;

void initFiles();

void setFilesFile(char *set);
void setFilesFIM(char *set);

char *getFilesFile();
char *getFilesFIM();

int indexAtLine(char *string);
int indexAtCharOfLine(int index);
char *lineAtIndex(int index);

#endif