#ifndef ARGS_H
#define ARGS_H

#include <stdio.h>
#include <stdbool.h>
#include <string.h>

#include <osu.h>

typedef struct ExportMap {
    bool with_newcombo;
    bool with_modehitobjects;
} ExportMap;

typedef struct ExportOutput {
    bool running;
    bool debug;
    bool objects;
} ExportOutput;

typedef struct Argument {
    bool ignore;
    bool debug;
    bool step;
    bool force_run;
    bool show_objects;
    ExportMap export_map;
    ExportOutput export_output;
} Argument;

extern Argument args_argument;

/*
    Handling the arguments given from the input of the terminal
    
    return
        void
    args
        int
        char **
*/
void args_assign(int, char **);

#endif