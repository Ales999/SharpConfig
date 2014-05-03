using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpConfig;

namespace UnitTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void ValueTypes()
        {
            var standardConfig = new Config("SharpConfigTest",true);

            int valueInteger = 5000;
            standardConfig["testInteger"] = valueInteger;

            DateTime valueDate = DateTime.Now;
            standardConfig["testDate"] = valueDate;

            char valueChar = 'x';
            standardConfig["testChar"] = valueChar;

            Assert.AreEqual(valueDate, standardConfig["testDate"]);
            Assert.AreEqual(valueInteger, standardConfig["testInteger"]);
            Assert.AreEqual(valueChar, standardConfig["testChar"]);
        }

        [TestMethod]
        public void ReferenceTypes()
        {
            var standardConfig = new Config("SharpConfigTest", true);

            string referenceString = "referenced string";
            string[] referenceArray = "referenced string".Split(' ');

            standardConfig["testString"] = referenceString;
            standardConfig["testArray"] = referenceArray;

            Assert.AreEqual(referenceString, standardConfig["testString"]);
            Assert.AreEqual(referenceArray, standardConfig["testArray"]);
        }

    }
}
