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



            //string companyCode = sgtin96Decoder.CompanyCode(testTag);
            //find invalid codes
            int milkaOreo = 0;
            int invalids = 0;
            foreach (var code in codeList)
            {
               
                if (sgtin96Decoder.IsCodeValid(code))
                {
                    string companyCode = sgtin96Decoder.CompanyCode(code);
                    string itemCode = sgtin96Decoder.ItemCode(code);
                    
                    if (companyCode == targetCompany && itemCode == targetItem)
                    {
                        Console.WriteLine("Milka!!");
                        milkaOreo++;
                    }
                }
                else
                {
                    invalids++;
                    Console.WriteLine("Invalid: {0}", code);
                }
            }


            Console.WriteLine("Milkas: {0}",milkaOreo);
            Console.WriteLine("Invalid: {0}", invalids);
            Console.WriteLine("Over!");
            Console.ReadLine();
        }
    }
}
