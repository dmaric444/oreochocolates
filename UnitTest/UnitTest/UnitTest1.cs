using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGTIN96;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        SGTIN96Decoder sgtin96Decoder = new SGTIN96Decoder("3074257BF7194E4000001A85");

        [TestMethod]
        public void TestCompanyCode()
        {
            string expected = "0614141";
            string actual = sgtin96Decoder.CompanyCode;
            Assert.AreEqual(expected, actual, "Company code invalid!");
        }
        [TestMethod]
        public void TestItemCode()
        {
            string expected = "812345";
            string actual = sgtin96Decoder.ItemCode;
            Assert.AreEqual(expected, actual, "Item code is invalid!");
        }

        [TestMethod]
        public void TestSerialNumber()
        {
            string expected = "6789";
            string actual = sgtin96Decoder.SerialNumber;
            Assert.AreEqual(expected, actual, "Serial number is invalid!");
        }
    }
}
