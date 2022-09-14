#include "args.h"

Argument args_argument = {
    .ignore = false,
    .debug = false,
    .step = false,
    .force_run = false,
    .show_objects = false,
    .export_map = {
        .with_newcombo = false,
        .with_modehitobjects = false
    },
    .export_output = {
        .running = false,
        .debug = false,
        .objects = false
    }
};

void args_assign(bool *keep_running, int argc, char **argv) {
    if (argc <= 1) {
        printf("Use `-h` or check out the documentation if you need help.\n");
    }
    for (int i = 1; i < argc; i++) {
        
    }

    if (help) {
        printf(""); // TODO
    }
}