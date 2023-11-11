using System;
using System.Collections.Generic;
using System.Text;

namespace StreamCiphers
{
    internal class Program
    {


        private static Random _random = new Random();
        private static int _maxNumberInByte = 256;

        // генерация случайной перестановки списка list
        public static List<int> GetRandomPermutation(List<int> list)
        {
            var resultList = new List<int>(list);

            int seed = 2 * list.Count;

            for (int n = 0; n < seed; n++)
            {
                int i = _random.Next(list.Count);
                int j = _random.Next(list.Count);
                if (i != j && resultList[i] != resultList[j])
                    (resultList[i], resultList[j]) = (resultList[j], resultList[i]);
            }
            return resultList;
        }

        // печать длинного списка с пояснением (показываются первые count элементов)
        public static void PrintLongList<T>(List<T> list, string msg = " ", int count = 10)
        {
            Console.Write($"{msg}: [");
            for (int i = 0; i < count; i++)
                Console.Write($"{list[i]}, ");
            Console.WriteLine("...]");
        }



        // инициализация генератора случайных чисел
        public static List<int> InitializationGenerateRabdomNuber(List<int> list, List<int> key)
        {
            var resultList = new List<int>(list);
            int j = 0;
            for (int i = 0; i < _maxNumberInByte; i++)
            {
                j = (j + resultList[i] + key[i]) % _maxNumberInByte;
                (resultList[i], resultList[j]) = (resultList[j], resultList[i]);
            }
            
            return resultList;
        }

        // преобразование символа в последовательность нулей и единиц
        public static string CharToBits(char _char, int count = 16) => Convert.ToString(_char, 2).PadLeft(count, '0'); 

        // преобразование числа в последовательность нулей и единиц
        public static string IntToBits(int _int, int count = 8) => Convert.ToString(_int, 2).PadLeft(count, '0');

        // преобразование последовательности нулей и единиц в число
        public static int FromBitsToNumber(string str) => Convert.ToInt32(str, 2);

        // получение списка разбитых кодов юникода на две части
        public static List<int> FromUnicodeToAscii(string str)
        {
            var list = new List<string>();

            foreach(var _char in str)
            {
                var s = CharToBits(_char);
                list.Add(s.Substring(0, 8));
                list.Add(s.Substring(8));
            }

            var resultList = new List<int>();
            foreach (var item in list)
                resultList.Add(FromBitsToNumber(item));

            return resultList;
        }

        // получение гаммы
        public static List<int> MakeGamma(List<int> gen, List<int> message)
        {
            var gamma = new List<int>();
            var genTemp = new List<int>(gen);

            int i = 0, j = 0;

            for (int k = 0; k < message.Count; k++)
            {
                i = (i + 1) % _maxNumberInByte;
                j = (j + genTemp[j]) % _maxNumberInByte;
                (genTemp[i], genTemp[j]) = (genTemp[j], genTemp[i]);
                gamma.Add(genTemp[(genTemp[i] + genTemp[j]) % _maxNumberInByte]);
            }

            return gamma;
        }

        // шифрование списка байт-чисел
        public static List<int> Chifering(List<int> message, List<int> gamma)
        {
            var resultList = new List<int>();

            for (int i = 0; i < message.Count; i++)
                resultList.Add(message[i] ^ gamma[i]); // ^ - XOR

            return resultList;
        }

        // показ сообщения в символьном виде
        public static string Symbolise(List<int> list)
        {
            string result = "";

            for (int i = 0; i < list.Count; i+=2)
                result += (char)FromBitsToNumber(IntToBits(list[i]) + IntToBits(list[i + 1]));

            return result;
        }


        static void Main(string[] args)
        {
            //Console.OutputEncoding = Encoding.Unicode;
            //Console.InputEncoding = Encoding.Unicode;

            Console.WriteLine("В данном проекте я хочу реализовать Потоковый шифр.");
            Console.WriteLine("Если вам интересно в чём его суть, то просто погуглите");
            Console.WriteLine("Ну-с, начнём!\n");

            
            // список от 0 до 255
            var list = new List<int>();
            for (int i = 0; i < _maxNumberInByte; i++)
                list.Add(i);

            // получение начального ключа
            var key = GetRandomPermutation(list);
            PrintLongList(key, "Ключ");

            // инициализация генератора случайных чисел
            var gen = new List<int>(key);
            gen = InitializationGenerateRabdomNuber(gen, key);
            PrintLongList(gen, "Инициализированный генератор случайных чисел");

            // сообщение
            var message = "The_text_to_be_encrypted";
            //var message = "Hello, world!";
            Console.WriteLine("Сообщение: " + message);

            // коды юникода символов сообщения
            // to do

            // список чисел, соответствующих байтам символов юникода
            var splittedMessage = FromUnicodeToAscii(message);
            PrintLongList(splittedMessage, "Пары байтов сообщения");

            // гамма
            var gamma = MakeGamma(gen, splittedMessage);
            PrintLongList(gamma, "Гамма");

            // шифрование сообщения
            var chiferingMessage = Chifering(splittedMessage, gamma);
            PrintLongList(chiferingMessage, "Зашифрованное сообщение");

            // показ сообщения в символьном виде
            var symlolisedMessage = Symbolise(chiferingMessage);
            Console.WriteLine("Шифрованное сообщение (символы юникода): " + symlolisedMessage);

            // расшифрование сообщения
            var desplittedMessage = FromUnicodeToAscii(symlolisedMessage);
            var dechifredMessage = Chifering(desplittedMessage, gamma);
            PrintLongList(dechifredMessage, "Расшифрованное сообщение (список)");
            Console.WriteLine("Расшифрованное сообщение (символы юникода):" + Symbolise(dechifredMessage));

        }
    }
}