using System;
using System.Net.WebSockets;
using System.Text;

namespace TheHammingCode 
{
    internal class Program
    {

        public static string ToBinary(byte _byte)
        {
            var str = Convert.ToString(_byte, 2);

            while (str.Length != 8)
                str = '0' + str;

            return str;
        }


        public static char GetControlBit(string str, int numberK)
        {

            int sum = 0;
            for (int i = numberK - 1; i < str.Length; i += numberK + numberK)
            {
                int length = i + numberK < str.Length ? i + numberK : str.Length;
                int a = 0;
                for (int j = i; j < length; j++)
                {
                    if (str[j] == '1')
                        sum++;
                }
            }
            if (sum % 2 == 0)
                return '0';
            else
                return '1';
        }

        public static string GetCodeHamming(string str)
        {
            

            int i = 0;
            while (Math.Pow(2, i) < str.Length) 
            {
                str = str.Insert((int)Math.Pow(2, i) - 1, "0");
                i++;
            }

            
            var s = str;
            for (int j = 0; j < i; j++)
            {
                str = str.Remove((int)Math.Pow(2, j) - 1, 1).Insert((int)Math.Pow(2, j) - 1, GetControlBit(s, (int)Math.Pow(2, j)).ToString());
            }


            return str;
        }

        public static string GetCodeHammingX(string str)
        {


            int i = 0;
            while (Math.Pow(2, i) < str.Length)
            {
                str = str.Insert((int)Math.Pow(2, i) - 1, "X");
                i++;
            }

            return str;
        }

        public static string RecalculateCodeHamming(string str)
        {
            int i = 0;
            while (Math.Pow(2, i) < str.Length)
            {
                str = str.Remove((int)Math.Pow(2, i) - 1, 1).Insert((int)Math.Pow(2, i) - 1, "0");
                i++;
            }

            var s = str;
            for (int j = 0; j < i; j++)
            {
                str = str.Remove((int)Math.Pow(2, j) - 1, 1).Insert((int)Math.Pow(2, j) - 1, GetControlBit(s, (int)Math.Pow(2, j)).ToString());
            }


            return str;
        }


        public static List<string> EncoderHamming(byte[] bytes)
        {

            var bits = new List<string>();
            for (int i = 0; i < bytes.Length; i++)
            {
                bits.Add(ToBinary(bytes[i]));
            }

            
            


            var list = new List<string>();
            for (int i = 0; i < bits.Count; i ++)
            {
                list.Add(GetCodeHamming(bits[i]));
            
            }
            

            return list;

        }

        public static List<string> DecoderHamming(List<string> encoderList)
        {

            var list = new List<string>();
            
            for (int i = 0; i < encoderList.Count; i++)
            {
                list.Add(RecalculateCodeHamming(encoderList[i]));
                int sum = -1;
                for (int j = 0; j < encoderList[i].Length; j++)
                {
                    if (list[i][j] != encoderList[i][j])
                        sum = sum == -1 ? j : sum + j;
                }
                if (sum != -1)
                {
                    if (encoderList[i][sum] == '0')
                        encoderList[i] = encoderList[i].Remove(sum, 1).Insert(sum, "1");
                    else
                        encoderList[i] = encoderList[i].Remove(sum, 1).Insert(sum, "0");
                }
                
            }

            return encoderList;
        }

        private static byte[] ConvertListStringBitsToByteArray(List<string> bits)
        {
            var result = new byte[bits.Count];
            for (int i = 0; i < bits.Count; i++)
                result[i] = Convert.ToByte(bits[i], 2);
            return result;
        }

        public static string GetStrFromCodeHamming(List<string> list)
        {

            var str = "";
            var bytes = new List<string>();

            for (int i = 0; i < list.Count; i++)
            {
                int power = 0;
                for (int j = 0; j < list[i].Length; j++)
                {
                    if (j != Math.Pow(2, power) - 1)
                    {

                        str += list[i][j];

                        if (str.Length == 8)
                        {
                            bytes.Add(str);
                            str = "";
                        }
                    }
                    else
                        power++;
                    
                }
            }

            return Encoding.ASCII.GetString(ConvertListStringBitsToByteArray(bytes)); 
        }

        static void Main(string[] args)
        {
            Console.WriteLine("В данном проекте я хочу реализовать простейший код Хемминга.");
            Console.WriteLine("Если вам интересно в чём его суть, то просто погуглите");
            Console.WriteLine("Ну-с, начнём!\n");

            var str = "habr";
            Console.WriteLine($"Изначальная строка: {str}");
            var bytes = Encoding.ASCII.GetBytes(str);
            foreach (var b in bytes)
                Console.Write(b + " ");
            Console.WriteLine();

            var encoderList = EncoderHamming(bytes);
            foreach (var encoder in encoderList)
                Console.Write($"{encoder} ");
            Console.WriteLine();

            encoderList[0] = encoderList[0].Remove(2, 1).Insert(1, "0");
            encoderList[1] = encoderList[1].Remove(2, 1).Insert(1, "0");
            foreach (var encoder in encoderList)
                Console.Write($"{encoder} ");
            Console.WriteLine();

            Console.WriteLine(GetStrFromCodeHamming(encoderList));

            var decoderList = DecoderHamming(encoderList);
            foreach (var decoder in decoderList)
                Console.Write($"{decoder} ");
            Console.WriteLine();

            Console.WriteLine(GetStrFromCodeHamming(decoderList));

        }
    }
}