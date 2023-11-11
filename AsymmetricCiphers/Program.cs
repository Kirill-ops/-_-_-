using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection;
using System.Numerics;

namespace AsymmetricCiphers
{
    internal class Program
    {
        // НОД чисел a и b
        static int GCD(int a, int b)
        {
            while (b != 0)
            {
                var temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        // вычисление обратного к числу а по модулю n
        public static int InverseNumberModulo(int a, int n)
        {
            if (a % n == 0)
                throw new ArgumentException("Обратного к числу нет");
            else
            {
                int result = 1;
                while (true)
                {
                    if ((result * a) % n == 1)
                        return result;
                    result++;
                }
            }
        }

        // вычисление функции Эйлера
        public static int EulerFunction(int n)
        {
            int result = 0;
            
            for (int i = 0; i <= n; i++)
                if (GCD(i, n) == 1)
                    result++;

            return result;
        }

        // вычисление ключей
        public static Tuple<Tuple<int, int>, Tuple<int, int>> GenerateKeys(int p, int q)
        {
            int n = p * q;
            int ph = EulerFunction(n);

            var ex = Enumerable.Range(3, ph).Where(x => GCD(x, ph) == 1).ToList();
            var rand = new Random();
            int e = ex[rand.Next(ex.Count)];

            int d = InverseNumberModulo(e, ph);
            var publicKey = new Tuple<int, int>(e, n);
            var secretKey = new Tuple<int, int>(d, n);
            return new Tuple<Tuple<int, int>, Tuple<int, int>>(publicKey, secretKey);

        }

        public static BigInteger Power(int num, int power)
        {
            var result = BigInteger.One;

            for (int i = 1; i <= power; i++)
                result *= num;

            return result;
        }

        // шифрование
        public static int Chifering(int m, Tuple<Tuple<int, int>, Tuple<int, int>> keys)
        {
            var publicKey = keys.Item1;
            var e = publicKey.Item1;
            var n = publicKey.Item2;

            return (int)(Power(m, e) % n);
        }

        // расшифровка
        public static int  Dechifering(int c, Tuple<Tuple<int, int>, Tuple<int, int>> keys)
        {

            var publicKey = keys.Item1;
            var secretKey = keys.Item2;

            var n = publicKey.Item2;
            var d = secretKey.Item1;

            return (int)(Power(c, d) % n);
        }

        public static string ConvertListIntToString(List<int> list)
        {
            string result = "";
            foreach (int i in list)
                result += (char)i;
            return result;
        }


        static void Main(string[] args)
        {
            Console.WriteLine("В данном проекте я хочу реализовать Асимметричный шифр.");
            Console.WriteLine("Если вам интересно в чём его суть, то просто погуглите");
            Console.WriteLine("Ну-с, начнём!\n");

            var keys = GenerateKeys(13, 17);
            Console.WriteLine($"Ключи: {keys}");

            var message = "The text to be encrypted";
            Console.WriteLine($"Изначально: {message}");

            var list = new List<int>();
            foreach (var _char in message)
                list.Add(_char);

            var listEncoder = new List<int>();
            for (int i = 0; i < list.Count; i++)
                listEncoder.Add(Chifering(list[i], keys));
            var encoderStr = ConvertListIntToString(listEncoder);
            Console.WriteLine($"Зашифрованное сообщение: {encoderStr}");


            var listDecoder = new List<int>();
            for (int i = 0; i < list.Count; i++)
                listDecoder.Add(Dechifering(listEncoder[i], keys));
            var decoderStr = ConvertListIntToString(listDecoder);
            Console.WriteLine($"Расшифрованное сообщение: {decoderStr}");


        }
    }
}