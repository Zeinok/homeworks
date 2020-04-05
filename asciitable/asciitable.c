#include <stdio.h>

const char ctrlChars[][4] = {"NUL", "SOH", "STX", "ETX", "EOT", "ENQ", "ACK", "BEL", "BS", "HT", "LF", "VT", "FF", "CR", "SO", "SI", "DLE", "DC1", "DC2", "DC3", "DC4", "NAK", "SYN", "ETB", "CAN", "EM", "SUB", "ESC", "FS", "GS", "RS", "US", "DEL"};

int main(){
    int i;
    for(i=0;i<128/4;++i){
        char row1[4];
	char* row1Ptr = row1;
	row1[0] = i;
	row1[1] = NULL;
	if(i<32) row1Ptr = ctrlChars[i];
	
	char row2[4];
	char* row2Ptr = row2;
	row2[0] = 64+i;
	row2[1] = NULL;
	if(64+i==127) row2Ptr = ctrlChars[32];

	int row3 = 128+i;
	if(row3==129||row3==141||row3==143||row3==144||row3==157||row3==160) row3 = '?'; // character doesn't exist

        printf("| %3d %3x %3s | %3d %3x %3s | %3d %3x %3c | %3d %3x %3c |\n", i, i, row1Ptr, 64+i, 64+i, row2Ptr, 128+i, 128+i, row3, 192+i, 192+i, 192+i);
    }
    return 0;
}
