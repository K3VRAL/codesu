#include "assignargs.h"
#include "codesuinfo.h"

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
		for (int i = stringstart; i < strlen(string); i++) {
			if (*(string + i) != *(str + j)) {
				return false;
			}
			j++;
		}
	}
	
	return found ? true : false;
}
 
void assignArgs(int argc, char **argv) {
    for (int i = 1; i < argc; i++) {
		if (*(*(argv + i)) + 0 == '-') {
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

		if (isOsuExtension(*(argv + i))) {
			FILE *fp;
			fp = fopen(*(argv + i), "r");
			if (fp == NULL) {
				exit(1);
			}
			fr.file = *(argv + i);

			char ch;
			char *stringbuild = (char *)malloc(sizeof (char));
			while ((ch = fgetc(fp)) != EOF) {
				if (ch != '\n') {
					*(stringbuild + strlen(stringbuild)) = ch;
					stringbuild = (char *)realloc(stringbuild, strlen(stringbuild));
				} else {
					char temp[4];
					strncpy(temp, stringbuild, 4);
					if (strcmp(temp, "Mode") == 0) {
						cinfo = *(stringbuild + 6) - '0';
						break;
					}
					memset(stringbuild, 0, strlen(stringbuild));
					stringbuild = (char *)realloc(stringbuild, 0);
				}
			}
			fclose(fp);
			free(stringbuild);
		}
	}
}