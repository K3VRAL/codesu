#include "assignargs.h"

bool isOsuExtension(char *string) {
	bool found = false;
	int stringstart = 0;
	for (int i = strlen(string); i >= 0; i--) {
		if (*(string + i) == '.') {
			found = true;
			stringstart = i;
		}
	}

	if (found) {
		char *str = ".osu";
		int j = 0;
		for (int i = stringstart; i < strlen(string); i++) if (*(string + i) != *(str + j++)) return false;
	}
	
	return found ? true : false;
}
 
void assignArgs(int argc, char **argv) {
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
				}
			}
		}

		if (isOsuExtension(*(argv + i))) readFileToMemory(*(argv + i));
	}
}