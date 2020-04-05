SRC = $(wildcard */*.cpp)
SRCNAME = $(SRC:%.cpp=%)

$(SRCNAME):
	g++ -static $@.cpp -o $@.out
	i686-w64-mingw32-g++ -static $@.cpp -o $@.exe

all: $(SRCNAME)

test:
	@echo $(SRC)
	@echo $(SRCNAME)
