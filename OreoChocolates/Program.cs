using SGTIN96;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OreoChocolates
{
    class Program
    {
        static void Main(string[] args)
        {
            SGTIN96Decoder sgtin96Decoder = new SGTIN96Decoder();

            //Read file with codes
            string codesFilePath = Path.GetFullPath(@"..\..\") + @"\Documents\tags.txt";
            var codeList = sgtin96Decoder.GetCodeList(codesFilePath);

            //Test convert hex to binary string
            string binaryString = sgtin96Decoder.HexStringToBinary(codeList[0]);
            string binaryString1 = sgtin96Decoder.HexStringToBinary("3074257BF7194E4000001A85");
        }
    }
}
