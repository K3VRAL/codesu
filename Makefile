CC = gcc
CFLAGS = -Wall
LFLAGS = 
TARGET = codesu
BINFLR = bin/

ENV =
VER =
EXEC =

all: CFLAGS += -g
all: $(TARGET)

Linux64: CFLAGS += -m64 -O3
Linux64: $(TARGET)
Linux64: cleano
Linux64: ENV = linux
Linux64: VER = 64
Linux64: EXEC = codesu
Linux64: add7z
Linux64: cleanx

Linux32: CFLAGS += -m32 -O3
Linux32: $(TARGET)
Linux32: cleano
Linux32: ENV = linux
Linux32: VER = 32
Linux32: EXEC = codesu
Linux32: add7z
Linux32: cleanx

Windows64: CC = x86_64-w64-mingw32-gcc
Windows64: CFLAGS += -O3
Windows64: $(TARGET)
Windows64: cleano
Windows64: ENV = windows
Windows64: VER = 64
Windows64: EXEC = codesu.exe
Windows64: add7z
Windows64: cleanx

Windows32: CC = i686-w64-mingw32-gcc
Windows32: CFLAGS += -O3
Windows32: $(TARGET)
Windows32: cleano
Windows32: ENV = windows
Windows32: VER = 32
Windows32: EXEC = codesu.exe
Windows32: add7z
Windows32: cleanx

%.o: %.c %.h | $(BINFLR)
	$(CC) $(CFLAGS) -o $(BINFLR)$(notdir $@) -c $<

$(TARGET): main.o src/programsu.o src/ctbrainfuck.o src/external.o src/mode.o src/lib/assignargs.o src/lib/args.o src/lib/codesuinfo.o src/lib/files.o
	$(CC) $(CFLAGS) -o $(BINFLR)$@ $(addprefix $(BINFLR), $(notdir $^)) $(LFLAGS)

$(BINFLR):
	if [ ! -d $(BINFLR) ]; then mkdir $(BINFLR); fi

cleano:
	rm $(BINFLR)*.o

cleanx:
	rm $(BINFLR)$(EXEC)

add7z:
	7z a $(BINFLR)$(ENV)_codesu_$(VER).7z $(BINFLR)$(EXEC)

clean:
	rm $(BINFLR)*.o $(BINFLR)$(TARGET) $(BINFLR)$(TARGET).exe