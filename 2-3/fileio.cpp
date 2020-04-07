#include <iostream>
#include <fstream>

using namespace std;

int main(int argc, const char** argv){
    if(argc!=2) {
        cout << "usage: " << argv[0] << " filename\n";
	return -1;
    }
    ifstream fs;
    fs.open(argv[1], fstream::in);
    char c;
    while(!fs.eof()){
        fs.get(c);
        cout << c;
    }
    return 0;
}
