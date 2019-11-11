using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgrammingTechnologies.BO.Models;

namespace ProgrammingTechnologiesTest.Models
{
    [TestClass]
    public class UserTest
    {
        #region Helpers

        private User CreateUser()
        {
            return new User()
            {
                Email = "mosquito@komar.enterprise.com",
                Name = "John",
                LastName = "Doe"
            };
        }

        #endregion

        [TestMethod]
        public void TestObjectIniitalizer()
        {
            User user = CreateUser(); 

            Assert.AreEqual("mosquito@komar.enterprise.com", user.Email);
            Assert.AreEqual("John", user.Name);
            Assert.AreEqual("Doe", user.LastName);
        }

        [TestMethod]
        public void TestProperties()
        {
            User user = new User();

            user.Email = "mosquito@komar.enterprise.com";
            user.Name = "John";
            user.LastName = "Doe";

            Assert.AreEqual("mosquito@komar.enterprise.com", user.Email);
            Assert.AreEqual("John", user.Name);
            Assert.AreEqual("Doe", user.LastName);
        }

        [TestMethod]
        public void TestPasswordHashing()
        {
            User user = CreateUser();

            user.Password = "password";

            Assert.IsTrue(user.IsPasswordCorrect("password"));
            Assert.IsFalse(user.IsPasswordCorrect("passwordooo"));
        }
    }
}
