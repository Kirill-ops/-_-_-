// Шифр_Цезаря.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>
#include <locale>
#include <Windows.h>
#include <string>

using namespace std;

string encoder(string& str, int offset)
{
    string encod_str = str;

    int number0 = 48, number9 = 57;
    int letter_a = 97, letter_z = 122;
    int letter_A = 65, letter_Z = 90;

    for (int i = 0; i < encod_str.length(); i++)
    {
        // смещение цифр 0...9
        if (number0 <= encod_str[i] && encod_str[i] <= number9)
        {
            if (abs(offset) > 10)
                offset = offset % 10;

            int div = encod_str[i] + offset;

            if (div > number9)
                div -= 10;
            else if (div < number0)
                div += 10;
            encod_str[i] = div;
        }
        // смещение A...Z
        else if (letter_A <= encod_str[i] && encod_str[i] <= letter_Z)
        {
            if (abs(offset) > 26)
                offset = offset % 26;

            int div = encod_str[i] + offset;

            if (div > letter_Z)
                div -= 26;
            else if (div < letter_A)
                div += 26;
            encod_str[i] = div;
        }
        // смещение a...z
        else if (letter_a <= encod_str[i] && encod_str[i] <= letter_z)
        {
            if (abs(offset) > 26)
                offset = offset % 26;

            int div = encod_str[i] + offset;

            if (div > letter_z)
                div -= 26;
            else if (div < letter_a)
                div += 26;
            encod_str[i] = div;
        }

    }

    return encod_str;
}


int main()
{
    //setlocale(LC_ALL, "rus");
    SetConsoleCP(1251);
    SetConsoleOutputCP(1251);


    cout << "В данном проекте я хочу реализовать шифр Цезаря." << endl;
    cout << "Если вам интересно в чём его суть, то просто погуглите" << endl;
    cout << "Ну-с, начнём!" << endl << endl;

    // Русские буквы: А...Я [-64; -33], а...я [-32; -1]
    // Английские буквы: A...Z [65; 90], a...z [97; 122]
    // 0...9 [48; 57]

    string str;
    int offset;

    cout << "Введите строку: ";  getline(cin, str);
    cout << "Введите offset: "; cin >> offset;
    string result = encoder(str, offset);
    cout << "Закодированная строка: " << result << endl;
    for (int i = 1; i <= 26; i++)
        cout << i << ": " << encoder(result, i) << endl;
    
}



