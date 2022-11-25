using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnitTestExample.Controllers;

namespace UnitTestExample.Test
{
    public class AccountControllerTestFixture
    {
        [
            Test,
            TestCase("abcd1234", false),
            TestCase("irf@uni-corvinus", false),
            TestCase("irf.uni-corvinus.hu", false),
            TestCase("irf@uni-corvinus.hu", true)
        ]

       
        public void TestValidateEmail(string email, bool expectedResult)
        {
            
            var accountController = new AccountController();

            
            var actualResult = accountController.ValidateEmail(email);

            
            Assert.AreEqual(expectedResult, actualResult);
        }

        [
            Test,
            TestCase("ABCD1234", false),
            TestCase("Ab1234", false),
            TestCase("Abcd1234", true),
            TestCase("abcd1234", false),
            TestCase("abcdABCD1234", false)
        ]

        public void TestValidatePassword(string password, bool expectedResult)
        {
            var accountController = new AccountController();

            //var actualResult = accountController.ValidatePassword(password);
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");

            var actualResult = hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password) && hasMinimum8Chars.IsMatch(password);

            Assert.AreEqual(expectedResult, actualResult);

            

            
        }
    }
}
