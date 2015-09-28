#include "Header.h"

LU::LU()
{
	std::cout << "Input name of file with matrix :" << std::endl;
	char c[20];
	std::cin.get(c, 20, '\n');
	for (char* C = c; *C != '\0'; C++)
	{
		Adres += *C;
		AdresV += *C;
	}
	std::ifstream File;
	File.open(Adres);
	Size = 0;
	int buf;
	while (!File.eof())
	{
		File >> buf;
		Size++;
	}
	File.seekg(0);
	Size = int(sqrt(Size));
	A = new float*[Size];
	for (int i = 0; i < Size; i++)
	{
		A[i] = new float[Size];
	}
	for (int i = 0; i < Size; i++)
	{
		for (int j = 0; j < Size; j++)
		{
			File >> A[i][j];
		}
	}
	File.close();
	B = new float[Size];
	X = new float[Size];
	File.open(AdresV);
	for (int i = 0; i < Size; i++)
	{
		File >> B[i];
	}
}

void LU::ShowMainMatrix(){
	for (int i = 0; i < Size; i++){
		for (int j = 0; j < Size; j++)
			std::cout << std::setw(3) << std::right << std::setprecision(2) << A[i][j] << " ";
		std::cout << std::setw(6) << std::right << B[i] << std::endl;
	}
}

LU::~LU()
{
	for (int j = 0; j < Size; j++)
	{
		delete[]A[j];
	}
	delete[]A;
	delete[]X;
	delete[]B;
}

void LU::Calculate()
{
	float ** M;
	M = new float*[Size];
	for (int i = 0; i < Size; i++)
	{
		M[i] = new float[Size];
	}
	for (int k = 0; k < Size - 1; k++){
		float max = 0;
		int m;
		for (int i = k; i < Size; i++){
			if (max < abs(A[i][k])) { m = i; max = abs(A[i][k]); }
		}
		if (max == 0) { std::cout << "WTF !? " << k << std::endl; system("Pause"); }
		else {
			if (k != m) V++;
			float c;
			c = B[k];
			B[k] = B[m];
			B[m] = c;
			for (int j = k; j < Size; j++){
				c = A[k][j];
				A[k][j] = A[m][j];
				A[m][j] = c;
			}
		}
		for (int i = k + 1; i < Size; i++){
			M[i][k] = -(A[i][k] / A[k][k]);
			B[i] = B[i] + M[i][k] * B[k];
			for (int j = k + 1; j < Size; j++)
				A[i][j] = A[i][j] + M[i][k] * A[k][j];
		}
	}
	X[Size - 1] = B[Size - 1] / A[Size - 1][Size - 1];
	for (int k = Size - 2; k >= 0; k--){
		float m = 0;
		for (int j = k + 1; j < Size; j++) m += A[k][j] * X[j];
		X[k] = (B[k] - m) / A[k][k];
	}
	V = pow(-1, V);
	for (int i = 0; i < Size; i++) V *= A[i][i];
	for (int i = 0; i < Size; i++) delete[Size]M[i];
	delete []M;
}

void LU::OutputResults()
{
	std::cout << "Result == ";
	for (int i = 0; i < Size; i++)
		std::cout << std::setw(4) << std::right << std::setprecision(3) << X[i] << " ";
	std::cout << std::endl << std::setw(4) << std::right << std::setprecision(3) << "Key matrix = " << V << std::endl;
}