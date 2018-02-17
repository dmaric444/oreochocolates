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

        private string _hex;
        private string _binary;
        private Partition _partition;

        private bool _codeIsValid;

        public bool CodeIsValid
        {
            get { return _codeIsValid; }
        }

        private string _companyCode;

        public string CompanyCode
        {
            get { return _companyCode; }
        }

        private string _serialNumber;

        public string SerialNumber
        {
            get { return _serialNumber; }
            
        }

        private string _itemCode;

        public string ItemCode
        {
            get { return _itemCode; }
        }

        public SGTIN96Decoder(string hex)
        {
            _hex = hex;
            _codeIsValid = IsCodeValid(hex);
            if (_codeIsValid)
            {
                _binary = HexStringToBinary();
                _partition = GetPartitionData();
                _companyCode = GetCompanyCode();
                _serialNumber = GetSerialNumber();
                _itemCode = GetItemCode();
            }
        }

     

        public string HexStringToBinary()
        {
            string binarystring = String.Join(String.Empty,
               _hex.Select(
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

            return true;
        }

        public bool IsHeaderSGTIN96(string hex)
        {
            if (hex.Substring(0, 2) == HEX_HEADER_VALUE)
                return true;
            else
                return false;
        }

        public bool StringIsHex(string hex)
        {
            return Regex.IsMatch(hex, @"\A\b[0-9a-fA-F]+\b\Z");
        }

        public Partition GetPartitionData()
        {
            int partitionValue = Convert.ToInt32(_binary.Substring(PARTITION_START_POSITION, 3), 2);
            return _partitions.Where(p => p.value == partitionValue).FirstOrDefault();
        }

        public string GetCompanyCode()
        {
            string companyData = _binary.Substring(COMPANY_START_POSITION, _partition.companyBitsCount);
            return Convert.ToInt64(companyData, 2).ToString().PadLeft(_partition.companyDigits, '0');
        }
        public string GetItemCode()
        {
            string itemData = _binary.Substring(COMPANY_START_POSITION + _partition.companyBitsCount, _partition.itemBitsCount);
            return Convert.ToInt64(itemData, 2).ToString().PadLeft(_partition.itemDigits, '0');
        }
        public string GetSerialNumber()
        {
            string serialNumberData = _binary.Substring(SERIAL_NUMBER_START_POSITION, SERIAL_NUMBER_LENGTH);
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
