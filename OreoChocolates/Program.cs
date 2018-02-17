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
            string targetCompany = "069124";
            string targetItem = "1253252";

            //Read file with codes
            string codesFilePath = Path.GetFullPath(@"..\..\") + @"\Documents\tags.txt";
            var codeList = sgtin96Decoder.GetCodeList(codesFilePath);

            //Test convert hex to binary string
            //string binaryString = sgtin96Decoder.HexStringToBinary(codeList[0]);
            string binaryString = sgtin96Decoder.HexStringToBinary("3074257BF7194E4000001A85");

            //test code is valid
            bool codeIsValid = sgtin96Decoder.IsCodeValid(testTag);
            Console.WriteLine(codeIsValid);



            //string companyCode = sgtin96Decoder.CompanyCode(testTag);
            //find invalid codes
            foreach (var code in codeList)
            {
               
                if (sgtin96Decoder.IsCodeValid(code))
                {
                    string companyCode = sgtin96Decoder.CompanyCode(code);
                    Console.WriteLine(companyCode);
                    if (companyCode == targetCompany)
                    {
                        Console.WriteLine("Milka!!");
                    }
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
