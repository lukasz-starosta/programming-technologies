﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgrammingTechnologies.Models;
using ProgrammingTechnologies.Services;

namespace ProgrammingTechnologiesTest.Services
{
    [TestClass]
    public class UserServiceTest
    {
        [TestMethod]
        public void TestCreateUser()
        {
            User user = new User()
            {
                Email = "mosquito",
                Name = "John",
                LastName = "Doe",
                Password = "password"
            };
            UserService service = new UserService();
            service.CreateUser(user);
        }

        [TestMethod]
        public void TestReadUser()
        {
            UserService service = new UserService();
            User user = service.GetUserWhere("name = 'John'");
            Assert.AreEqual("John", user.Name);
        }

        [TestMethod]
        public void TestUpdateUser()
        {
            UserService service = new UserService();
            User user = service.GetUserWhere("name = 'John'");
            user.LastName = "Dobrik";
            service.UpdateUser(user);
            User result = service.GetUserWhere("name = 'John'");
            Assert.AreEqual("Dobrik", result.LastName);
        }

        [TestMethod]
        public void TestDeleteUser()
        {
            UserService service = new UserService();
            service.DeleteUserWhere("last_name = 'Dobrik'");
        }
    }
}