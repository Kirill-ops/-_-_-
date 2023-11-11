
#include <iostream>
#include <string>
#include <vector>
#include <numeric>
#include <algorithm>
#include <map>

using namespace std;

int NOD(int a, int b)
{
    while (a > 0 && b > 0)
    {
        if (a > b)
            a %= b;
        else
            b %= a;
    }
    return a + b;
}

void encoder(int a, int b, string &str, map<char, int>& alph, map<int, char>& inverse_alph)
{
    for (int i = 0; i < str.length(); i++)
    {
        str[i] = inverse_alph[(a * alph[str[i]] + b) % alph.size()];
    }
}

void decoder(int inv_a, int b, string& str, map<char, int>& alph, map<int, char>& inverse_alph)
{
    for (int i = 0; i < str.length(); i++)
    {
        int z;
        if (alph[str[i]] - b < 0)
            z = alph.size() - (abs((alph[str[i]] - b) * inv_a) % alph.size());
        else
            z = ((alph[str[i]] - b) * inv_a) % alph.size();
        str[i] = inverse_alph[z];
    }
}

int inverse_a(int a, int n)
{
    int res = 1;
    while (true)
    {
        if ((a * res) % n == 1)
            return res;
        else
            res++; 
    }
}


int main()
{
    setlocale(LC_ALL, "rus");

    cout << "В данном проекте я хочу реализовать Аффинный шифр." << endl;
    cout << "Если вам интересно в чём его суть, то просто погуглите" << endl;
    cout << "Ну-с, начнём!" << endl << endl;

    cout << endl << "Вариант №8" << endl;
    cout << endl << "Аффинный шифр." << endl;

    string str = "NIKITA_I_KOT";
    //vector<char> alphabet {'A', 'O', 'I', 'N', 'T', 'K', '_' };
    map<char, int> alphabet;
    alphabet['A'] = 0;
    alphabet['O'] = 1;
    alphabet['I'] = 2;
    alphabet['N'] = 3;
    alphabet['T'] = 4;
    alphabet['K'] = 5;
    alphabet['_'] = 6;

    map<int, char> inverse_alphabet;
    inverse_alphabet[0] = 'A';
    inverse_alphabet[1] = 'O';
    inverse_alphabet[2] = 'I';
    inverse_alphabet[3] = 'N';
    inverse_alphabet[4] = 'T';
    inverse_alphabet[5] = 'K';
    inverse_alphabet[6] = '_';
    
    int n = alphabet.size();
    int a, b;
    string result;

    
    //cout << "Введите строку: ";  getline(cin, str);
    cout << endl << "Строка-оригинал: " << str << endl;  
    cout << "НОД(a, N) = 1 N = " << n << endl;
    while (true)
    {
        cout << "Введите a: "; cin >> a;
        if (NOD(a, n) == 1)
            break;
        else
            cout << "НОД(a, N) != 1" << endl;
    }
    cout << "НОД(b, N) = 1 N = " << n << endl;
    while (true)
    {
        cout << "Введите b: "; cin >> b;
        if (NOD(b, n) == 1)
            break;
        else
            cout << "НОД(b, N) != 1" << endl;
    }
    encoder(a, b, str, alphabet, inverse_alphabet);
    cout << "Зашифрованная строка: " << str << endl;

    int inv_a = inverse_a(a, n);
    decoder(inv_a, b, str, alphabet, inverse_alphabet);
    cout << "Расшифрованная строка: " << str << endl;




    

}

