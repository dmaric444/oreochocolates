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
        private const int BINARY_STRING_LENGTH = 96;
        private const string HEX_HEADER_VALUE = "30";
        private const int PARTITION_START_POSITION = 11;
        private const int COMPANY_START_POSITION = 14;
        private const int SERIAL_NUMBER_START_POSITION = 58;
        private const int SERIAL_NUMBER_LENGTH= 38;

        private List<Partition> _partitions = new List<Partition>(){
              new Partition(){ value = 0 , companyBitsCount = 40, companyDigits = 12, itemBitsCount = 4, itemDigits = 1},
              new Partition(){ value = 1 , companyBitsCount = 37, companyDigits = 11,  itemBitsCount = 7, itemDigits = 2},
              new Partition(){ value = 2 , companyBitsCount = 34, companyDigits = 10,  itemBitsCount = 10, itemDigits = 3},
              new Partition(){ value = 3 , companyBitsCount = 30, companyDigits = 9,  itemBitsCount = 14, itemDigits = 4},
              new Partition(){ value = 4 , companyBitsCount = 27, companyDigits = 8,  itemBitsCount = 17, itemDigits = 5},
              new Partition(){ value = 5 , companyBitsCount = 24, companyDigits = 7,  itemBitsCount = 20, itemDigits = 6},
              new Partition(){ value = 6 , companyBitsCount = 20, companyDigits = 6,  itemBitsCount = 24, itemDigits = 7}
        };

        

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
            if (hex.Substring(0, 2) == HEX_HEADER_VALUE)
                return true;
            else
                return false;
        }

        public bool LengthIsValid(string binaryString)
        {
            if (binaryString.Length == BINARY_STRING_LENGTH)
                return true;
            else
                return false;

        }

        public bool StringIsHex(string hex)
        {
            return Regex.IsMatch(hex, @"\A\b[0-9a-fA-F]+\b\Z");

        }

        public Partition PartitionData(string hex)
        {
            string binary = HexStringToBinary(hex);
            int partitionValue = Convert.ToInt32(binary.Substring(PARTITION_START_POSITION, 3), 2);
            return _partitions.Where(p => p.value == partitionValue).FirstOrDefault();

        }

        public string CompanyCode(string hex)
        {
            Partition partition = PartitionData(hex);
            string binary = HexStringToBinary(hex);
            string companyData = binary.Substring(COMPANY_START_POSITION, partition.companyBitsCount);
            return Convert.ToInt64(companyData, 2).ToString().PadLeft(partition.companyDigits, '0');

        }
        public string ItemCode(string hex)
        {
            Partition partition = PartitionData(hex);
            string binary = HexStringToBinary(hex);
            string itemData = binary.Substring(COMPANY_START_POSITION + partition.companyBitsCount, partition.itemBitsCount);
            return Convert.ToInt64(itemData, 2).ToString().PadLeft(partition.itemDigits, '0');
        }
        public string SerialNumber(string hex)
        {
            Partition partition = PartitionData(hex);
            string binary = HexStringToBinary(hex);
            string serialNumberData = binary.Substring(SERIAL_NUMBER_START_POSITION, SERIAL_NUMBER_LENGTH);
            return Convert.ToInt64(serialNumberData, 2).ToString();
        }


    }

    public class Partition
    {
        public int value { get; set; }
        public int companyBitsCount { get; set; }
        public int companyDigits { get; set; }
        public int itemBitsCount { get; set; }
        public int itemDigits { get; set; }

    }
}
