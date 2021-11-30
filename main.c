#include "main.h"

int main(int argc, char **argv) {
	assignArgs(argc, argv);

	if (fr.file == NULL || cinfo == -1) { perror("main"); exit(EXIT_FAILURE); }
	assignExportAndLog();

	bool run = programsuRun();

	if (run) {
		freeingProgramsu();
		freeingMemory();
	} else { perror("main"); exit(EXIT_FAILURE); }
	return EXIT_SUCCESS;
}