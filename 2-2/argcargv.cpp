#include <iostream>

int main(int argc, const char** argv){
    std::cout << "Arguments:" << '\n';
    for(int i=0;i<argc;++i){
        std::cout << "Argument " << i << ": " << argv[i] << '\n';
    }
    return 0;
}
