#include "main.h"

int main(int argc, char **argv) {
	assignArgs(argc, argv);

	if (fr.file == NULL || cinfo == -1) { perror("main"); exit(EXIT_FAILURE); }
	assignExportAndLog();

	restore = stdout;
	programsuRun();

	freeingMemory();
	return EXIT_SUCCESS;
}