using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using PartsWarehouse;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void PasswordEncryptTest()
        {
            string password = "qq";
            string expected = "BED4EB698C6EEEA7F1DDF5397D480D3F2C0FB938";
            Assert.AreEqual(Encrypt.GetHash(password), expected);
        }
        [TestMethod]
        public void LoginTest()
        {
            string login = "Matrix";
            string password = "meme3";
            Assert.IsTrue(Functions.LoginCheck(login, password));
        }
        [TestMethod]
        public void IsValidLoginAndPassword()
        {
            Assert.IsTrue(Functions.IsValidLogAndPass("Matrix", "meme3"));
            Assert.IsTrue(Functions.IsValidLogAndPass("Imagine", "pizza"));
            Assert.IsTrue(Functions.IsValidLogAndPass("Login???", "p@ssw0rd"));
            Assert.IsFalse(Functions.IsValidLogAndPass("", ""));
            Assert.IsFalse(Functions.IsValidLogAndPass("", "SimplePass"));
            Assert.IsFalse(Functions.IsValidLogAndPass("SimpleLogin", ""));
        }
        [TestMethod]
        public void IsLoginAlreadyTaken()
        {
            Assert.IsTrue(Functions.IsLoginAlreadyTaken("Matrix"));
            Assert.IsTrue(Functions.IsLoginAlreadyTaken("Imagine"));
            Assert.IsFalse(Functions.IsLoginAlreadyTaken("SimpleLogin"));
            Assert.IsFalse(Functions.IsLoginAlreadyTaken("Login?"));
            Assert.IsFalse(Functions.IsLoginAlreadyTaken(""));
        }
        [TestMethod]
        public void IsLogEqualPass()
        {
            Assert.IsFalse(Functions.IsLogEqualPass("Matrix", "Matrix"));
            Assert.IsTrue(Functions.IsLogEqualPass("Matrix", "meme3"));
        }
        [TestMethod]
        public void IsValidLength()
        {
            Assert.IsTrue(Functions.IsValidLength("Matrix"));
            Assert.IsTrue(Functions.IsValidLength("Matrwerwewe"));
            Assert.IsFalse(Functions.IsValidLength("Ma"));
            Assert.IsFalse(Functions.IsValidLength(""));
        }
    }
}
