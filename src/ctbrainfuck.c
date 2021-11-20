#include "ctbrainfuck.h"

yLocation yloc = { 0, 64, 128, 192, 256, 320, 384 };
allHO *aho;

void runInit() {
    char *temp = lineAtIndex(indexAtLine("[HitObjects]"));
    printf("%s\n", temp);
    free(temp);
}

void runStart() {
    unsigned int tick = 0;
    unsigned int objct = 0;
    short memory[USHRT_MAX];
    for (unsigned short i = 0; i < USHRT_MAX; i++) {
        memory[i] = 0;
    }
    unsigned int memorypos = 0;

    // while (objct < ) {}
}

void runCatchTheBeat() {
    runInit();
    runStart();
}