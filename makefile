CXX=g++
CXXFLAGS=-std=c++2a

LDLIBS:=
ifeq ($(OS),Windows_NT)
	LDLIBS+=-lmingw32 -lsfml-audio -lsfml-system
else
	LDLIBS+=-lsfml-audio -lsfml-system
endif

OBJS=obj/main.o

TARGET:=
ifeq ($(OS),Windows_NT)
	TARGET+=termvid.exe
else
	TARGET+=termvid
endif

OBJDIR=obj
BINDIR=bin

$(TARGET): $(OBJS)
	$(CXX) $^ -o $(BINDIR)/$@ $(CXXFLAGS) $(LDLIBS)

obj/main.o: main.cpp
	$(CXX) main.cpp -c -o $(OBJDIR)/main.o $(CXXFLAGS)

setup:
	mkdir -p $(OBJDIR) $(BINDIR)

clean:
	rm $(OBJS) $(BINDIR)/$(TARGET)
