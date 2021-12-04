CC = gcc
CFLAGS = -Wall
LFLAGS = 
TARGET = codesu
BINFLR = bin/

all: CFLAGS += -g
all: $(TARGET)

Linux64: CFLAGS += -m64 -O3
Linux64: $(TARGET)
Linux64: cleano

Linux32: CFLAGS += -m32 -O3
Linux32: $(TARGET)
Linux32: cleano

Windows64: CC = x86_64-w64-mingw32-gcc
Windows64: CFLAGS += -O3
Windows64: $(TARGET)
Windows64: cleano

Windows32: CC = i686-w64-mingw32-gcc
Windows32: CFLAGS += -O3
Windows32: $(TARGET)
Windows32: cleano

%.o: %.c %.h | $(BINFLR)
	$(CC) $(CFLAGS) -o $(BINFLR)$(notdir $@) -c $<

$(TARGET): main.o src/programsu.o src/ctbrainfuck.o src/external.o src/mode.o src/lib/assignargs.o src/lib/args.o src/lib/codesuinfo.o src/lib/files.o
	$(CC) $(CFLAGS) -o $(BINFLR)$@ $(addprefix $(BINFLR), $(notdir $^)) $(LFLAGS)

$(BINFLR):
	if [ ! -d $(BINFLR) ]; then mkdir $(BINFLR); fi

cleano:
	rm $(BINFLR)*.o

clean:
	rm $(BINFLR)*.o $(BINFLR)$(TARGET)