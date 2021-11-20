#include "main.h"

int main(int argc, char **argv) {
	initArgs();
	assignArgs(argc, argv);

	if (fr.file == NULL || cinfo == -1) {
		exit(1);
	}
	assignExportAndLog();

	programsuRun();

	return 0;
}