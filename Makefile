CC = gcc
CFLAGS = -Wall
LFLAGS = 
TARGET = codesu
BINFLR = bin/

all: CFLAGS += -g
all: $(TARGET)

Linux64: CFLAGS += -m64 -O3
Linux64: $(TARGET)

Linux32: CFLAGS += -m32 -O3
Linux32: $(TARGET)

%.o: %.c %.h | $(BINFLR)
	$(CC) $(CFLAGS) -o $(BINFLR)$(notdir $@) -c $<

$(TARGET): main.o src/programsu.o src/ctbrainfuck.o src/external.o src/mode.o src/lib/assignargs.o src/lib/args.o src/lib/codesuinfo.o src/lib/files.o
	$(CC) $(CFLAGS) -o $(BINFLR)$@ $(addprefix $(BINFLR), $(notdir $^)) $(LFLAGS)

$(BINFLR):
	if [ ! -d $(BINFLR) ]; then mkdir $(BINFLR); fi

clean:
	rm $(BINFLR)*.o $(BINFLR)$(TARGET)