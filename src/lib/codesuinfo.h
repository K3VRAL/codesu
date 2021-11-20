#ifndef CODESUINFO_H
#define CODESUINFO_H

#include <stdlib.h>

typedef enum {
    ostandard = 0,
    otaiko = 1,
    ocatch = 2,
    omania = 3
} enum_mode;

void initCodesu();

void setCodesuMode(int set);

enum_mode getCodesuMode();

#endif