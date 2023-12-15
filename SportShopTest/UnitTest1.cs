using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SportShop;
using SportShop.Views;
using System.Globalization;

namespace SportShopTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCorrectLogin()
        {
            string password = "asd";
            string login = "asd";
            bool expected = true;

            bool actual = TestValid.ValidateUser(login, password);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestCorrectLogin1()
        {
            string password = "user";
            string login = "user";
            bool expected = false;

            bool actual = TestValid.ValidateUser(login, password);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestLogin()
        {
            string password = "password";
            string login = "asd";
            bool expected = false;

            bool actual = TestValid.ValidateUser(login, password);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]        
        public void ValidateTest2()
        {
            Assert.AreEqual(true, TestValid.ZayavkaTest(1, 3));
        }
        [TestMethod]
        public void ValidateTest()
        {
            string value = "asd";
            Assert.AreEqual(false, TestValid.getUser(value)); ;
        }
        [TestMethod]
        public void addUserTest()
        {
            Assert.AreEqual(true, TestValid.addUser("op", "op", 1)); ;
        }
    }
}
