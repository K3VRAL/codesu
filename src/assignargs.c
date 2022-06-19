#include "../include/assignargs.h"

// User inputs data to be processed
void assignArgs(int argc, char **argv) {
	bool found = false;
	for (int i = 1; i < argc; i++) {
		if (*(*(argv + i)) == '-') {
			for (int j = 1; j < strlen(*(argv + i)); j++) {
				switch (*(*(argv + i) + j)) {
					case 'i':
						arg.ignore = true;
						break;

					case 's':
						arg.step = true;
						break;

					case 'r':
						arg.run = true;
						break;

					case 'd':
						arg.debug = true;
						break;

					case 'a':
						arg.all = true;
						break;

					case 'e':
						arg.exporting = true;
						break;

					case 'l':
						arg.logging = true;
						break;

					case 'h':
						arg.help = true;
						break;
				}
			}
		}

		if (arg.help) {
			fprintf(stdout, "Help Menu:\n"
				"\tRequires an input of an .osu file with relevant objects; circle, slider, spinner\n"
				"\t-i\t\tIgnores all warnings\n"
				"\t-s\t\tTakes a step for each execution printed to the console\n"
				"\t-r\t\tRuns regardless of arguments used: -e -a -l\n"
				"\t-d\t\tShows debugging information while executing\n"
				"\t-a\t\tShows all objects and their information; stop program immediately\n"
				"\t-e\t\tExports targetted file by removing unnecessary data without having to modify the original\n"
				"\t-l\t\tLogs information after executing the file\n"
				"\t-h\t\tShows the help menu\n"
			);
		}

		char *str = strrchr(*(argv + i), '.');
		if (str != NULL && !strcmp(str, ".osu")) {
			if (!found) {
				found = true;
				readFileToMemory(*(argv + i));
			} else {
				fprintf(stdout, "Already made an input. Not going to interpret `%s`\n", *(argv + i));
			}
		}
	}
	if (!found) {
		fprintf(stdout, "No input file found!\n");
	}
}