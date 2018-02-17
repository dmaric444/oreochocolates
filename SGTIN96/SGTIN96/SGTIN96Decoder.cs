using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SGTIN96
{

    public class SGTIN96Decoder
    {
        public string[] GetCodeList(string filePath)
        {
            string[] codes = File.ReadAllLines(filePath);
            return codes;
        }

        public string HexStringToBinary(string hex)
        {
            string binarystring = String.Join(String.Empty,
               hex.Select(
                 c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
               )
             );
            return binarystring;
        }

        public bool IsCodeValid(string hex)
        {
            if (!StringIsHex(hex))
                return false;

            if (!IsHeaderSGTIN96(hex))
                return false;

            string binary = HexStringToBinary(hex);
            if (!LengthIsValid(binary))
                return false;

            return true;
        }

        public bool IsHeaderSGTIN96(string hex)
        {
            if (hex.Substring(0, 2) == "30")
                return true;
            else
                return false;
        }

        public bool LengthIsValid(string binaryString)
        {
            if (binaryString.Length == 96)
                return true;
            else
                return false;

        }

        public bool StringIsHex(string hexString)
        {
            return Regex.IsMatch(hexString, @"\A\b[0-9a-fA-F]+\b\Z");

        }
    }
}
