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

        [TestMethod]
        public void SavingLoading()
        {
            int value = 1024;
            {
                var standardConfig = new Config("SharpConfigTest");

                standardConfig["test"] = value;

                standardConfig.Save();
            }

            {
                var standardConfig = new Config("SharpConfigTest");

                Assert.AreEqual(value,standardConfig["test"]);
            }

        }

        [TestMethod]
        public void LocalSavingLoading()
        {
            int value = 1024;
            {
                var standardConfig = new Config("SharpConfigTest",true);

                standardConfig["test"] = value;

                standardConfig.Save();
            }

            {
                var standardConfig = new Config("SharpConfigTest", true);

                Assert.AreEqual(value, standardConfig["test"]);
            }

        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void Delete()
        {
            var standardConfig = new Config("SharpConfigTest", true);

            standardConfig["test"] = 1024;


            standardConfig.Delete("test");

            if (standardConfig["test"] == 1024)
            {
                Assert.Fail("Deleted key still exists.");
            }

        }

    }
}
