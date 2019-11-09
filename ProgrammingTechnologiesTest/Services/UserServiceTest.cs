using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgrammingTechnologies.Models;
using ProgrammingTechnologies.Services;
using System;
using System.Collections.Generic;

namespace ProgrammingTechnologiesTest.Services
{
    [TestClass]
    public class UserServiceTest
    {
        private User GetNewUser()
        {
            return new User()
            {
                Email = "mosquito",
                Name = "John",
                LastName = "Doe",
                Password = "password"
            };
        }

        [TestCleanup]
        public void TestCleanup()
        {
            DatabaseService databaseService = new DatabaseService();
            databaseService.ExecuteInstruction("delete from Users");
        }

        [TestMethod]
        public void TestCreateUser()
        {
            User user = GetNewUser();
            UserService userService = new UserService(new DatabaseService());
            userService.CreateServicedObject(ref user);
            Assert.IsNotNull(user.Id);
        }

        [TestMethod]
        public void TestGetUser()
        {
            User user = GetNewUser();
            UserService userService = new UserService(new DatabaseService());
            userService.CreateServicedObject(ref user);
            user = userService.GetServicedObjectWhere("name = 'John'");
            Assert.AreEqual("John", user.Name);
        }

        [TestMethod]
        public void TestUpdateUser()
        {
            UserService userService= new UserService(new DatabaseService());
            User user = GetNewUser();
            userService.CreateServicedObject(ref user);
            user.LastName = "Dobrik";
            userService.UpdateServicedObject(ref user);
            User result = userService.GetServicedObjectWhere("name = 'John'");
            Assert.AreEqual("Dobrik", result.LastName);
        }

        [TestMethod]
        public void TestGetAllUsers()
        {
            UserService service = new UserService(new DatabaseService());
            List<User> users = service.GetAllServicedObjects();
            foreach (User user in users)
            {
                Assert.IsTrue(user.Id != 0);
            }
        }

        [TestMethod]
        public void TestGetAllUsersWhere()
        {
            UserService service = new UserService(new DatabaseService());
            List<User> users = service.GetAllServicedObjectsWhere("last_name = 'Dobrik'");
            foreach (User user in users)
            {
                Assert.AreEqual("Dobrik", user.LastName);
            }
        }

        [TestMethod]
        public void TestDeleteUser()
        {
            UserService service = new UserService(new DatabaseService());
            service.DeleteServicedObjectWhere("last_name = 'Dobrik'");
        }
    }
}
