using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgrammingTechnologies.Models;

namespace ProgrammingTechnologiesTest.Models
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void TestObjectIniitalizer()
        {
            User user = new User()
            {
                Email = "mosquito@komar.enterprise.com",
                Name = "John",
                LastName = "Doe"
            };

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
            User user = new User()
            {
                Email = "mosquito@komar.enterprise.com",
                Name = "John",
                LastName = "Doe"
            };

            user.Password = "password";

            Assert.IsTrue(user.IsPasswordCorrect("password"));
            Assert.IsFalse(user.IsPasswordCorrect("passwordooo"));
        }
    }
}
