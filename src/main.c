#include "../include/main.h"

int main(int argc, char **argv) {
	restore = stdout;
	assignArgs(argc, argv);

	if (fr.file == NULL || cinfo == -1) outputError("No file was inputted.", "");
	assignExportAndLog();

	programsuRun();

	freeingMemory();
	return EXIT_SUCCESS;
}
