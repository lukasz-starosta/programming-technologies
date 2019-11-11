using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgrammingTechnologies.BLL.Managers;
using ProgrammingTechnologies.BO.Models;
using ProgrammingTechnologies.Helpers;

namespace ProgrammingTechnologiesTest.Managers
{
    [TestClass]
    public class UserManagerTest
    {
        [TestMethod]
        public void TestManagerBaseFunctionalities()
        {
            UserManager userManager = new UserManager(ServiceProvider.GetDatabaseDependentServices);
            User user = new User()
            {
                Email = "mosquito",
                Name = "John",
                LastName = "Doe",
                Password = "password"
            };
            userManager.CreateManagedObject(ref user);
            user.LastName = "Pepper";
            userManager.UpdateManagedObject(ref user);
            userManager.DeleteManagedObject(user);
        }
    }
}
