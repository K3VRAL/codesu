#include "main.h"

int main(int argc, char **argv) {
	initArgs();
	initFiles();
	assignArgs(argc, argv);

	if (strlen(getFilesFile()) == 0 || getCodesuMode() == -1) {
		exit(1);
	}
	assignExportAndLog();

	programsuRun();

	return 0;
}