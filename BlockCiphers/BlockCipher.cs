using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BlockCiphers
{
    public class BlockCipher
    {
        //public Dictionary<int, bool> DictionaryPBox;
        public List<int> EncoderDictionaryPBox;
        public List<int> DecoderDictionaryPBox;

        public List<byte> EncoderDictionarySBox;
        public List<byte> DecoderDictionarySBox;

        public BlockCipher()
        {
            EncoderDictionaryPBox = new() { 5, 6, 7, 2, 0, 1, 3, 4 };

            EncoderDictionarySBox = new() {
                15, 12, 13, 14,
                8,  10, 11, 9,
                5,   4,  6, 7,
                0,   2,  3, 1
            };
        }

        private byte[] ConvertListStringToByteArray(List<string> bits)
        {
            var result = new byte[bits.Count];
            for(int i = 0; i < bits.Count; i++)
                result[i] = Convert.ToByte(bits[i], 2);
            return result;
        }
         

        public string Encoder(string str)
        {
            var result = str;

            // блок P
            var bytes = Encoding.UTF8.GetBytes(result);
            var listBit = PBlock.Encoder(bytes);
            var SBytes = SBlock.Encoder(listBit);
            listBit = PBlock.Encoder(SBytes);


            result = Encoding.UTF8.GetString(ConvertListStringToByteArray(listBit));

            return result;
        }

        public string Decoder(string str)
        {
            var result = str;

            // блок P
            var bytes = Encoding.UTF8.GetBytes(result);
            var listBit = PBlock.Decoder(bytes);
            var SBytes = SBlock.Decoder(listBit);
            listBit = PBlock.Decoder(SBytes);


            result = Encoding.UTF8.GetString(ConvertListStringToByteArray(listBit));

            return result;
        }


    }
}
