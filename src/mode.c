#include "../include/mode.h"

// Prints enum to string

char *etsCommand(instruction com) {
    switch (com) {
        case inpDig:
            return "inp,";
        case inpAsc:
            return "inp;";
        case jmpStr:
            return "jmp[";
        case jmpEnd:
            return "jmp]";
        case pntLft:
            return "pnt<";
        case pntRgt:
            return "pnt>";
        case inc:
            return "inc+";
        case dec:
            return "dec-";
        case ran:
            return "rnd~";
        case mulc:
            return "mul*";
        case divc:
            return "div/";
        case outDig:
            return "out.";
        case outAsc:
            return "out:";
        case isnull:
        default:
            return "isnull";
    }
}

char *etsType(types type) {
    switch (type) {
        case circle:
            return "circle";
        case slider:
            return "slider";
        case spinner:
            return "spinner";
        default:
            return "isnull";
    }
}