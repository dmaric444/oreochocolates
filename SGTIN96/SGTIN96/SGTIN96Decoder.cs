using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

    }
}
