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

            string testTag = "3074257BF7194E4000001A85";

            //Read file with codes
            string codesFilePath = Path.GetFullPath(@"..\..\") + @"\Documents\tags.txt";
            var codeList = sgtin96Decoder.GetCodeList(codesFilePath);

            //Test convert hex to binary string
            //string binaryString = sgtin96Decoder.HexStringToBinary(codeList[0]);
            string binaryString = sgtin96Decoder.HexStringToBinary("3074257BF7194E4000001A85");

            //test code is valid
            bool codeIsValid = sgtin96Decoder.IsCodeValid(testTag);
            Console.WriteLine(codeIsValid);

            //find invalid codes
            foreach (var code in codeList)
            {
               
                if (sgtin96Decoder.IsCodeValid(code))
                {
                    Console.WriteLine("Valid: {0}", code);
                }
                else
                {
                    Console.WriteLine("Invalid: {0}", code);
                }
            }


            Console.WriteLine("Over!");
            Console.ReadLine();
        }
    }
}
