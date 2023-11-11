using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockCiphers
{
    public class SBlock
    {
        public static List<byte> EncoderDictionarySBox = new()
        {
                15, 12, 13, 14,
                8,  10, 11, 9,
                5,   4,  6, 7,
                0,   2,  3, 1
        };

        private static string ToBinary(byte byteOne, byte byteTwo)
        {
            var str1 = Convert.ToString(byteOne, 2);
            var str2 = Convert.ToString(byteTwo, 2);

            while (str1.Length != 4)
                str1 = '0' + str1;
            while (str2.Length != 4)
                str2 = '0' + str2;

            return str1 + str2;
        }


        // шифратор
        public static byte[]  Encoder(List<string> listBit)
        {
            // to do
            var numbers = new List<byte>();
            for (int i = 0; i < listBit.Count; i++)
            {
                string _string = listBit[i].Substring(0, 4);     // выделяем 4 бита
                numbers.Add(EncoderDictionarySBox[Convert.ToByte(_string, 2)]); // переводим в 10 СС, и сразу меняем
                _string = listBit[i].Substring(4, 4);            // выделяем 4 бита
                numbers.Add(EncoderDictionarySBox[Convert.ToByte(_string, 2)]); // переводим в 10 СС, и сразу меняем
            }

            var listResultBit = new List<string>();
            for (int i = 0; i < numbers.Count; i += 2)
            {
                var str = ToBinary(numbers[i], numbers[i + 1]);
                listResultBit.Add(str);
            }

            var bytesResult = new byte[listResultBit.Count];
            for (int i = 0; i < bytesResult.Length; i++)
                bytesResult[i] = Convert.ToByte(listResultBit[i], 2);
            return bytesResult;
        }

        // дешифратор
        public static byte[] Decoder(List<string> listBit)
        {
            // to do
            var numbers = new List<byte>();
            for (int i = 0; i < listBit.Count; i++)
            {
                string _string = listBit[i].Substring(0, 4);     // выделяем 4 бита
                numbers.Add((byte)EncoderDictionarySBox.IndexOf(Convert.ToByte(_string, 2))); // переводим в 10 СС, и сразу меняем
                _string = listBit[i].Substring(4, 4);            // выделяем 4 бита
                numbers.Add((byte)EncoderDictionarySBox.IndexOf(Convert.ToByte(_string, 2))); // переводим в 10 СС, и сразу меняем
            }
            var listResultBit = new List<string>();
            for (int i = 0; i < numbers.Count; i += 2)
                listResultBit.Add(ToBinary(numbers[i], numbers[i + 1]));

            var bytesResult = new byte[listResultBit.Count];
            for (int i = 0; i < bytesResult.Length; i++)
                bytesResult[i] = Convert.ToByte(listResultBit[i], 2);
            return bytesResult;
        }

    }
}
