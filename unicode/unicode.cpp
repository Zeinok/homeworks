#include <cstdio>
#include <string>

using namespace std;
int main(){
    std::u16string wstr = u"林奕丞";
    for(uint16_t c : wstr){
        printf("\\u{%x}", c);
    }
    putchar('\n');
    return 0;
}