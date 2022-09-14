#include "main.h"

int main(int argc, char **argv) {
    args_assign(argc, argv);

    of_beatmap_free(&beatmap);
    return 0;
}