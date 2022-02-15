CC		= gcc
CFLAGS	= -Wall
LFLAGS	= 
TARGET	= codesu
BINFLR	= bin/
RELFLR	= binrel/
ENV		=
VER		=
EXEC	=

all:		CFLAGS += -g
all:		$(TARGET)

Linux64:	CC = gcc
Linux64:	CFLAGS += -m64 -O3
Linux64:	ENV = linux
Linux64:	VER = 64
Linux64:	EXEC =
Linux64:	$(TARGET)
Linux64:	rel

Linux32:	CC = gcc
Linux32:	CFLAGS += -m32 -O3
Linux32:	ENV = linux
Linux32:	VER = 32
Linux32:	EXEC =
Linux32:	$(TARGET)
Linux32:	rel

ifeq ($(OS), Windows_NT)
Windows64:	CC = gcc
Windows64:	CFLAGS += -O3 -static
else
Windows64:	CC = x86_64-w64-mingw32-gcc
Windows64:	CFLAGS += -O3
endif
Windows64:	ENV = windows
Windows64:	VER = 64
Windows64:	EXEC = .exe
Windows64:	$(TARGET)
Windows64:	rel

ifeq ($(OS), Windows_NT)
Windows32:	CC = gcc
Windows32:	CFLAGS += -O3 -static
else
Windows32:	CC = i686-w64-mingw32-gcc
Windows32:	CFLAGS += -O3
endif
Windows32:	ENV = windows
Windows32:	VER = 32
Windows32:	EXEC = .exe
Windows32:	$(TARGET)
Windows32:	rel

%.o: src/%.c include/%.h | $(BINFLR)
	$(CC) $(CFLAGS) -o $(BINFLR)$(notdir $@) -c $<

$(TARGET): $(addsuffix .o, $(basename $(notdir $(wildcard src/*.c))))
	$(CC) $(CFLAGS) -o $(BINFLR)$@ $(addprefix $(BINFLR), $(notdir $^)) $(LFLAGS)

$(BINFLR):
	[ -d $(BINFLR) ] || mkdir -p $(BINFLR)

clean:
	rm $(BINFLR)$(TARGET)* $(BINFLR)*.o

rel:
	[ -d $(RELFLR) ] || mkdir -p $(RELFLR)
	7z a $(RELFLR)$(ENV)_$(TARGET)_$(VER).7z $(BINFLR)$(TARGET)$(EXEC)

cleanrel:
	rm $(RELFLR)*.7z
