#include "Header.h"

int main()
{
	LU L = LU();
	L.ShowMainMatrix();
	L.Calculate();
	L.OutputResults();
	system("pause");
}