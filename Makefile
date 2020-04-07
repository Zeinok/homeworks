.DEFAULT_GOAL = all
SRC = $(wildcard */*.cpp)
EXE = $(SRC:%.cpp=%.exe)
OUT = $(SRC:%.cpp=%.out)
SRCNAME = $(SRC:%.cpp=%)


all: $(OUT) $(EXE)


%.out: %.cpp
	g++ -static $< -o $@

%.exe: %.cpp
	i686-w64-mingw32-g++ -static $< -o $@

clean:
	rm $(EXE) $(OUT)

test:
	@echo $(SRC)
	@echo $(SRCNAME)
