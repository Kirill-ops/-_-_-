using System.Collections;
using System.Text;

namespace BlockCiphers
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("В данном проекте я хочу реализовать Аффинный шифр.");
            Console.WriteLine("Если вам интересно в чём его суть, то просто погуглите");
            Console.WriteLine("Ну-с, начнём!\n");
            Console.WriteLine("Вариант №8");
            Console.WriteLine("Блочные шифры.");

            string str = "Albukerke_Sanya_LOH";
            Console.WriteLine($"Изначальная строка: {str}"); 

            var blockCipher = new BlockCipher();
            var res = blockCipher.Encoder(str);
            Console.WriteLine($"Зашифрованная строка: {res}");
            Console.WriteLine($"Расшифрованная строка: {blockCipher.Decoder(res)}");
            
        }

    }
}