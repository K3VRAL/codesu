#include "codesuinfo.h"

enum_mode cinfo = -1;

void setCodesuMode(int set) {
    switch (set) {
        case ostandard:
            cinfo = ostandard;
            break;
        case otaiko:
            cinfo = otaiko;
            break;
        case ocatch:
            cinfo = ocatch;
            break;
        case omania:
            cinfo = omania;
            break;
        default:
            exit(1);
            break;
    }
}

enum_mode getCodesuMode() {
    return cinfo;
}