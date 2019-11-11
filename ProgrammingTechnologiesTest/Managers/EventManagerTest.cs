using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgrammingTechnologies.BLL.Managers;
using ProgrammingTechnologies.BO.Models;
using ProgrammingTechnologies.Helpers;
using ProgrammingTechnologiesTest.Helpers;
using System.Collections.Generic;

namespace ProgrammingTechnologiesTest.Managers
{
    [TestClass]
    public class EventManagerTest
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
        public void TestGetEventGame()
        {
            EventManager eventManager = new EventManager(ServiceProvider.GetDatabaseDependentServices);

            Event _event = eventManager.GetAllManagedObjects()[2];

            Game game = eventManager.GetEventGame(_event);

            Assert.AreEqual(_event.GameId, game.Id);
        }

        [TestMethod]
        public void TestGetEventOwner()
        {
            EventManager eventManager = new EventManager(ServiceProvider.GetDatabaseDependentServices);

            Event _event = eventManager.GetAllManagedObjects()[2];

            User user = eventManager.GetEventOwner(_event);

            Assert.AreEqual(_event.UserId, user.Id);
        }

        [TestMethod]
        public void TestGetInvitedUsers()
        {
            UserManager userManager = new UserManager(ServiceProvider.GetDatabaseDependentServices);
            GameManager gameManager = new GameManager(ServiceProvider.GetDatabaseDependentServices);
            EventManager eventManager = new EventManager(ServiceProvider.GetDatabaseDependentServices);
            InvitationManager invitationManager = new InvitationManager(ServiceProvider.GetDatabaseDependentServices);


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

            Event _event = new Event()
            {
                Title = "EventForEventService",
                Description = "Test",
                Date = System.DateTime.Now,
                UserId = user.Id,
                GameId = game.Id
            };

            eventManager.CreateManagedObject(ref _event);

            Invitation invitation = new Invitation()
            {
                UserId = user.Id,
                EventId = _event.Id
            };

            invitationManager.CreateManagedObject(ref invitation);

            List<Invitation> invitations = invitationManager.GetAllManagedObjectsWhere($"event_id = {_event.Id}");

            List<User> users = eventManager.GetInvitedUsers(_event);

            users.ForEach(_user =>
            {
                Assert.IsTrue(invitations.Exists(delegate (Invitation invite)
                {
                    return invite.UserId == _user.Id;
                }));
            });
        }
    }
}
