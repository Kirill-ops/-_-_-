// Шифр_Цезаря.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>
#include <locale>
#include <Windows.h>
#include <string>

using namespace std;


string encoder_vigern_chipher(string str, string str_key) {
    string encoder_str = "";
    int str_key_length = str_key.length();
    int str_length = str.length();

    // Проходим по каждому символу открытого текста
    for (int i = 0; i < str_length; i++) {
        char str_character = str[i];
        if (str[i] >= 65 && str[i] <= 90)
        {
            char str_key_character = str_key[i % str_key_length];

            // Вычисляем новый символ шифротекста
            char encod_character = (str_character + str_key_character) % 26 + 'A';
            encoder_str += encod_character;
        }
        else
            encoder_str += str[i];
    }

    return encoder_str;
}

//Sanya is an old senile
//SanyaLokh

string decoder_vigern_chipher(string str, string str_key) {
    string decoder_str = "";
    int str_key_length = str_key.length();
    int encod_str_length = str.length();

    // Проходим по каждому символу шифрованного текста
    for (int i = 0; i < encod_str_length; i++) {
        char encod_str_character = str[i];
        if (str[i] >= 65 && str[i] <= 90)
        {
            char str_key_character = str_key[i % str_key_length];

            // Вычисляем новый символ открытого текста
            char decod_character = (encod_str_character - str_key_character + 26) % 26 + 'A';
            decoder_str += decod_character;
        }
        else
            decoder_str += str[i];
    }

    return decoder_str;
}

void to_upper(string& str)
{
    for (int i = 0; i < str.length(); i++)
        str[i] = toupper(str[i]);
}


int main()
{
    //setlocale(LC_ALL, "rus");
    SetConsoleCP(1251);
    SetConsoleOutputCP(1251);


    cout << "В данном проекте я хочу реализовать шифр Вижерна." << endl;
    cout << "Если вам интересно в чём его суть, то просто погуглите" << endl;
    cout << "Ну-с, начнём!" << endl << endl;

    string str;
    string result;
    string str_key;

    cout << endl << "Шифр Вижерна." << endl;
    cout << "Введите строку: ";  getline(cin, str);
    cout << "Введите строку ключ: ";  getline(cin, str_key);
    to_upper(str);
    to_upper(str_key);
    result = encoder_vigern_chipher(str, str_key);
    cout << "Закодированная строка: " << result << endl;
    cout << "Декодированная строка: " << decoder_vigern_chipher(result, str_key) << endl;

}



