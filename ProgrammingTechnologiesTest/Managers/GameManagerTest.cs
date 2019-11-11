using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgrammingTechnologies.BLL.Managers;
using ProgrammingTechnologies.BO.Models;
using ProgrammingTechnologies.Helpers;
using ProgrammingTechnologiesTest.Helpers;

namespace ProgrammingTechnologiesTest.Managers
{
    [TestClass]
    public class GameManagerTest
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            DatabaseSeeder.SeedDatabase();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            DatabaseSeeder.CleanDatabase();
        }

        [TestMethod]
        public void TestGetGameOwner()
        {
            UserManager userManager = new UserManager(ServiceProvider.GetDatabaseDependentServices);
            GameManager gameManager = new GameManager(ServiceProvider.GetDatabaseDependentServices);

            User user = new User()
            {
                Name = "Tester",
                LastName = "Bestes",
                Email = "test@test.com",
                Password = "password"
            };

            userManager.CreateManagedObject(ref user);

            Game game = new Game()
            {
                Title = "Test Title",
                Description = "Test Description",
                Category = 2,
                UserId = user.Id
            };

            gameManager.CreateManagedObject(ref game);

            User result = gameManager.GetGameOwner(game);

            Assert.AreEqual(result.Id, user.Id);
        }
    }
}
