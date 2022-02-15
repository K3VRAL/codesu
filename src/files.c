#include "../include/files.h"

FileRelated fr = { NULL, NULL, NULL, 0 };

// Thank you cboard.cprogramming.com - john.c
// Reads file and stores it in memory to be processed
void readFileToMemory(char *file) {
    FILE *fp;
    fp = fopen(file, "r");
    if (!fp) { perror(file); exit(EXIT_FAILURE); }

    fr.file = strdup(file);
    fr.lines = malloc(sizeof (char *));

    fr.atLine = malloc(sizeof (int));
    int curLine = 0;

    char line[256];
    size_t len;
    bool more = false;

    bool afterMode = false;
    bool afterHO = false;
    while (fgets(line, sizeof (line), fp) != NULL) {
        if (!afterMode && !strstr(line, "Mode: ")) {
            cinfo = line[6] - '0';
            afterMode = true;
        }
        if (!afterHO && !strcmp(line, "[HitObjects]\n")) {
            afterHO = true;
            continue;
        }
        if (line[0] != '\n' && afterHO) {
            len = strlen(line);
            bool still_more = false;
            if (line[len-1] == '\n') line[--len] = '\0';
            else still_more = true;

            if (!more) {
                fr.lines = realloc(fr.lines, (fr.numLines + 1) * sizeof (char *));
                *(fr.lines + fr.numLines) = strdup(line);

                fr.atLine = realloc(fr.atLine, (fr.numLines + 1) * sizeof (int));
                *(fr.atLine + fr.numLines) = curLine + 1;

                fr.numLines++;
            } else {
                size_t n = fr.numLines - 1;
                size_t oldlen = strlen(*(fr.lines + n));
                *(fr.lines + n) = realloc(*(fr.lines + n), oldlen + len + 1);
                strcat(*(fr.lines + n), line);
            }
            more = still_more;
        }
        
        curLine++;
    }
    fclose(fp);
}

// Frees all data stored in memory
void freeingMemory(void) {
    for (int i = 0; i < fr.numLines; i++) free(*(fr.lines + i));
    free(fr.atLine);
    free(fr.lines);
    free(fr.file);
}