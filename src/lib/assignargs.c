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
						setIgnore(true);
						break;

					case 's':
						setStep(true);
						break;

					case 'r':
						setRun(true);
						break;

					case 'd':
						setDebug(true);
						break;

					case 'a':
						setAll(true);
						break;

					case 'e':
						setExporting(true);
						break;

					case 'l':
						setLogging(true);
						break;
				}
			}
		}

		if (isOsuExtension(*(argv + i))) {
			setFilesFile(*(argv + i));

			FILE *fp;
			fp = fopen(*(argv + i), "r");
			if (fp == NULL) {
				exit(1);
			}

			char ch;
			char *stringbuild = (char *)malloc(sizeof (char));
			memset(stringbuild, 0, strlen(stringbuild));
			while ((ch = fgetc(fp)) != EOF) {
				if (ch != '\n') {
					*(stringbuild + strlen(stringbuild)) = ch;
					stringbuild = (char *)realloc(stringbuild, strlen(stringbuild) + 1);
				} else {
					char temp[5];
					strncpy(temp, stringbuild, 4);
					if (strcmp(temp, "Mode") == 0 && getCodesuMode() == -1) {
						setCodesuMode(*(stringbuild + 6) - '0');
						break;
					}
					memset(stringbuild, 0, strlen(stringbuild));
				}
			}
			fclose(fp);
			free(stringbuild);
		}
	}
}