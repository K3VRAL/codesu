#include "main.h"
#include <stdio.h>
#include <stdlib.h>

int main(int argc, char **argv) {
	initArgs();
	assignArgs(argc, argv);

	if (fr.file == NULL || cinfo == -1) { perror("main"); exit(EXIT_FAILURE); }
	assignExportAndLog();

	programsuRun();

	freeingMemory();
	return EXIT_SUCCESS;
}