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
        


    }
}
