using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockCiphers
{
    public class PBlock
    {
        private static List<int> _encoderDictionaryPBox = new() { 5, 6, 7, 2, 0, 1, 3, 4 };

        
        private static string ToBinary(byte _byte)
        {
            var str = Convert.ToString(_byte, 2);

            while (str.Length != 8)
                str = '0' + str;

            return str;
        }

        public static List<string> Encoder(byte[] bytes)
        {
            var result = new List<string>();

            for (int i = 0; i < bytes.Length; i++)
                result.Add(ToBinary(bytes[i]));
            for (int i = 0; i < result.Count; i++)
            {
                string str = "";
                for (int j = 0; j < result[i].Length; j++)
                    str += result[i][_encoderDictionaryPBox[j]];
                result[i] = str;
            }
            return result;
        }

        public static List<string> Decoder(byte[] bytes)
        {
            var result = new List<string>();

            for (int i = 0; i < bytes.Length; i++)
                result.Add(ToBinary(bytes[i]));
            for (int i = 0; i < result.Count; i++)
            {
                char[] str = new char[8];
                for (int j = 0; j < result[i].Length; j++)
                    str[_encoderDictionaryPBox[j]] = result[i][j];
                result[i] = new string(str);
            }
            return result;
        }


    }
}
