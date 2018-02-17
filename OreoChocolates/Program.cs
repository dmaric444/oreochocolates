using SGTIN96;
using System;
using System.Collections.Generic;
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
            var codeList = sgtin96Decoder.GetCodeList(System.IO.Path.GetFullPath(@"..\..\") + @"\Documents\tags.txt");

        }
    }
}
