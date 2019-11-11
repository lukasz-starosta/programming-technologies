using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgrammingTechnologies.BLL.Managers;
using ProgrammingTechnologies.BO.Models;
using ProgrammingTechnologies.Helpers;
using ProgrammingTechnologiesTest.Helpers;
using System.Collections.Generic;

namespace ProgrammingTechnologiesTest.Managers
{
    [TestClass]
    public class UserManagerTest
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
            User result = userManager.GetManagedObjectWhere($"id = {user.Id}");
            List<User> users = userManager.GetAllManagedObjects();
            users = userManager.GetAllManagedObjectsWhere("email = 'mosquito'");
            userManager.DeleteManagedObject(user);
        }

        [TestMethod]
        public void TestGetGamesOfUser()
        {
            UserManager userManager = new UserManager(ServiceProvider.GetDatabaseDependentServices);
            User user = userManager.GetManagedObjectWhere("name = 'Tester'");
            List<Game> games = userManager.GetGamesOf(user);
            games.ForEach(game =>
            {
                Assert.AreEqual(game.UserId, user.Id);
            });
        }

        [TestMethod]
        public void TestGetInvitedEventsOfUser()
        {
            UserManager userManager = new UserManager(ServiceProvider.GetDatabaseDependentServices);
            User user = userManager.GetManagedObjectWhere("name = 'Tester'");
            List<Event> events = userManager.GetInvitedEventsOf(user);
            InvitationManager invitationManager = new InvitationManager(ServiceProvider.GetDatabaseDependentServices);
            List<Invitation> invitations;
            events.ForEach(_event =>
            {
                invitations = invitationManager.GetAllManagedObjectsWhere($"event_id = {_event.Id}");
                invitations.ForEach(invitation =>
                {
                    Assert.AreEqual(invitation.UserId, user.Id);
                });
            });
        }

        [TestMethod]
        public void TestGetEventsOrganizedBy()
        {
            UserManager userManager = new UserManager(ServiceProvider.GetDatabaseDependentServices);
            User user = userManager.GetManagedObjectWhere("name = 'Tester'");
            List<Event> events = userManager.GetEventsOrganizedBy(user);
            events.ForEach(_event =>
            {
                Assert.AreEqual(_event.UserId, user.Id);
            });
        }

        [TestMethod]
        public void TestInviteUserToEvent()
        {
            UserManager userManager = new UserManager(ServiceProvider.GetDatabaseDependentServices);
            EventManager eventManager = new EventManager(ServiceProvider.GetDatabaseDependentServices);
            GameManager gameManager = new GameManager(ServiceProvider.GetDatabaseDependentServices);

            User user = new User()
            {
                Name = "Invited",
                LastName = "Bestes",
                Email = "test@test.com",
                Password = "password"
            };

            userManager.CreateManagedObject(ref user);

            Event _event = new Event()
            {
                Title = "InvitationsEvent",
                Description = "Test",
                Date = System.DateTime.Now,
                UserId = user.Id,
                GameId = gameManager.GetAllManagedObjects()[0].Id
            };

            eventManager.CreateManagedObject(ref _event);

            userManager.InviteUserToEvent(user, _event);

            Event result = userManager.GetInvitedEventsOf(user).Find(delegate (Event e) { return e.Title.Equals("InvitationsEvent"); });

            Assert.AreEqual(result.Title, _event.Title);
        }
    }
}
