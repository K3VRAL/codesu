#include "files.h"

FileRelated fr = { NULL, NULL, NULL, 0 };

// Thank you cboard.cprogramming.com - john.c
void *xrealloc(void *ptr, size_t size) {
    void *newptr = realloc(ptr, size);
    if (!newptr) { perror("xrealloc"); exit(EXIT_FAILURE); }
    return newptr;
}

// Thank you cboard.cprogramming.com - john.c
void readFileToMemory(char *file) {
    FILE *fp;
    fp = fopen(file, "r");
    if (!fp) { perror(file); exit(EXIT_FAILURE); }

    fr.file = xrealloc(NULL, strlen(file) * sizeof (char) + 1);
    strcpy(fr.file, file);
    fr.lines = xrealloc(NULL, sizeof (char *));

    fr.atLine = xrealloc(NULL, sizeof (int));
    int curLine = 0;

    char line[256];
    size_t len;
    bool more = false;

    while (fgets(line, sizeof (line), fp) != NULL) {
        if (line[0] != '\n') {
            len = strlen(line);
            bool still_more = false;
            if (line[len-1] == '\n') line[--len] = '\0';
            else still_more = true;

            if (!more) {
                fr.lines = xrealloc(fr.lines, (fr.numLines + 1) * sizeof (char *));
                *(fr.lines + fr.numLines) = xrealloc(NULL, len + 1);
                strcpy(*(fr.lines + fr.numLines), line);

                fr.atLine = xrealloc(fr.atLine, (fr.numLines + 1) * sizeof (int));
                *(fr.atLine + fr.numLines) = curLine + 1;

                fr.numLines++;
            } else {
                size_t n = fr.numLines - 1;
                size_t oldlen = strlen(*(fr.lines + n));
                *(fr.lines + n) = xrealloc(*(fr.lines + n), oldlen + len + 1);
                strcpy((*(fr.lines + n) + oldlen), line);
            }
            more = still_more;
        }
        
        curLine++;
    }
    fclose(fp);
}

void freeingMemory() {
    for (int i = 0; i < fr.numLines; i++) free(*(fr.lines + i));
    free(fr.atLine);
    free(fr.lines);
    free(fr.file);
}