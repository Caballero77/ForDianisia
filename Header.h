#include <iostream>
#include <iomanip>
#include <fstream>
class LU
{
	float** A;
	float* X,* B;
	int Size;
	float V = 0;
	std::string Adres = "c:\\Users\\�����\\Documents\\Visual Studio 2013\\Projects\\Pr\\LU-r\\Matrix\\"; // ��� ����� ����� � ��������� + Matrix\\ � ���� ����� � ���������� (����'������,������ ��������� �� ����!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!)
	std::string AdresV = Adres + "Vector\\";
public:
	LU();
	~LU();
	void Calculate();
	void ShowMainMatrix();
	void OutputResults();
};