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
            string targetCompany = "069124"; //Milka
            string targetItem = "1253252";   //Oreo
            string codesFilePath = Path.GetFullPath(@"..\..\") + @"\Documents\tags.txt";

            ListItems(codesFilePath, targetCompany, targetItem);

            Console.WriteLine("Over!");
            Console.ReadLine();
        }

        private static void ListItems(string codesFilePath, string targetCompany, string targetItem)
        {

            List<string> validSN = new List<string>();
            List<string> invalidHex = new List<string>();
            string[] codeList = File.ReadAllLines(codesFilePath);

            foreach (var code in codeList)
            {
                SGTIN96Decoder sgtin96Decoder = new SGTIN96Decoder(code);
                if (sgtin96Decoder.CodeIsValid)
                {
                    if (sgtin96Decoder.CompanyCode == targetCompany && sgtin96Decoder.ItemCode == targetItem)
                        validSN.Add(sgtin96Decoder.SerialNumber);
                }
                else
                {
                    invalidHex.Add(code);
                }
            }

            PrintList("Invalid tags:", invalidHex);
            PrintList("Founded serials:", validSN);

        }

        private static void PrintList(string title, List<string> list)
        {
            Console.WriteLine(title);
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Total: {0}", list.Count);
            Console.WriteLine("");
        }
    }
}
