CC = gcc
CFLAGS = -g -Wall
LFLAGS = 
TARGET = codesu
BINFLR = bin/

all: $(TARGET)

%.o: %.c %.h
	$(CC) $(CFLAGS) -o $(BINFLR)$(notdir $@) -c $<

$(TARGET): main.o src/programsu.o src/ctbrainfuck.o src/external.o src/mode.o src/lib/assignargs.o src/lib/args.o src/lib/codesuinfo.o src/lib/files.o
	$(CC) $(CFLAGS) -o $(BINFLR)$@ $(addprefix $(BINFLR), $(notdir $^)) $(LFLAGS)

clean:
	rm $(BINFLR)*.o $(BINFLR)$(TARGET)