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

        public BlockCipher() { }

        private byte[] ConvertListStringBitsToByteArray(List<string> bits)
        {
            var result = new byte[bits.Count];
            for(int i = 0; i < bits.Count; i++)
                result[i] = Convert.ToByte(bits[i], 2);
            return result;
        }
         

        public string Encoder(string str)
        {
            //var bytes = Encoding.UTF8.GetBytes(str);
            var bytes = Encoding.Unicode.GetBytes(str);

            var listBit = PBlock.Encoder(bytes);    // блок P
            var SBytes = SBlock.Encoder(listBit);   // блок S
            listBit = PBlock.Encoder(SBytes);       // блок P

            //return Encoding.UTF8.GetString(ConvertListStringBitsToByteArray(listBit));
            return Encoding.Unicode.GetString(ConvertListStringBitsToByteArray(listBit));

        }

        public string Decoder(string str)
        {
            //var bytes = Encoding.UTF8.GetBytes(str);
            var bytes = Encoding.Unicode.GetBytes(str);

            var listBit = PBlock.Decoder(bytes);    // блок P
            var SBytes = SBlock.Decoder(listBit);   // блок S
            listBit = PBlock.Decoder(SBytes);       // блок P

            //return Encoding.UTF8.GetString(ConvertListStringBitsToByteArray(listBit));
            return Encoding.Unicode.GetString(ConvertListStringBitsToByteArray(listBit));
        }


    }
}
