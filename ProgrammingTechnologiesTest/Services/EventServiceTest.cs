using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgrammingTechnologies.BO.Models;
using ProgrammingTechnologies.DAL.Services;
using System.Collections.Generic;

namespace ProgrammingTechnologiesTest.Services
{
    [TestClass]
    public class EventServiceTest
    {
        private User user;
        private Game game;

        private Event getNewEvent()
        {
            return new Event()
            {
                Title = "EventForEventService",
                Description = "Test",
                Date = System.DateTime.Now,
                UserId = user.Id,
                GameId = game.Id
            };
        }

        [TestInitialize]
        public void TestInitialize()
        {
            DatabaseService databaseService = new DatabaseService();
            UserService userService = new UserService(databaseService);
            GameService gameService = new GameService(databaseService);

            user = new User()
            {
                Name = "EventServiceTest",
                LastName = "Test",
                Email = "test@tes.com",
                Password = "password",
            };
            userService.CreateServicedObject(ref user);

            game = new Game()
            {
                Title = "EventServiceTestGame",
                Description = "Test",
                Category = 1,
                UserId = user.Id
            };
            gameService.CreateServicedObject(ref game);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            DatabaseService databaseService = new DatabaseService();
            UserService userService = new UserService(databaseService);
            GameService gameService = new GameService(databaseService);

            databaseService.ExecuteInstruction("delete from Events");
            gameService.DeleteServicedObject(game);
            userService.DeleteServicedObject(user);
        }

        [TestMethod]
        public void TestCreateEvent()
        {
            Event _event = getNewEvent();

            EventService eventService = new EventService(new DatabaseService());

            eventService.CreateServicedObject(ref _event);
            Assert.IsNotNull(_event.Id);
        }

        [TestMethod]
        public void TestUpdateEvent()
        {
            Event _event = getNewEvent();

            EventService eventService = new EventService(new DatabaseService());

            eventService.CreateServicedObject(ref _event);

            _event.Description = "Changed description.";

            eventService.UpdateServicedObject(ref _event);

            Assert.AreEqual("Changed description.", _event.Description);
        }

        [TestMethod]
        public void TestGetEvent()
        {
            Event _event = getNewEvent();

            EventService eventService = new EventService(new DatabaseService());

            eventService.CreateServicedObject(ref _event);
            Event result = eventService.GetServicedObjectWhere("title = 'EventForEventService'");

            Assert.AreEqual(_event.Title, result.Title);
        }

        [TestMethod]
        public void TestGetAllEventsWhere()
        {
            EventService eventService = new EventService(new DatabaseService());

            for (int i = 0; i < 10; i++)
            {
                Event _event = getNewEvent();
                eventService.CreateServicedObject(ref _event);
            }

            List<Event> events = eventService.GetAllServicedObjectsWhere("title = 'EventForEventService'");

            Assert.AreEqual(10, events.Count);

            foreach (Event e in events)
            {
                Assert.AreEqual("EventForEventService", e.Title);
            }
        }

        [TestMethod]
        public void TestGetAll()
        {
            EventService eventService = new EventService(new DatabaseService());

            int eventsBefore = eventService.GetAllServicedObjects().Count;

            for (int i = 0; i < 10; i++)
            {
                Event _event = getNewEvent();
                eventService.CreateServicedObject(ref _event);
            }

            int eventsAfter = eventService.GetAllServicedObjects().Count;

            Assert.AreEqual(10, eventsAfter - eventsBefore);
        }

        [TestMethod]
        public void TestDeleteEvent()
        {
            Event _event = getNewEvent();

            EventService eventService = new EventService(new DatabaseService());

            eventService.CreateServicedObject(ref _event);

            int eventsBefore = eventService.GetAllServicedObjects().Count;

            eventService.DeleteServicedObject(_event);

            int eventsAfter = eventService.GetAllServicedObjects().Count;

            Assert.AreEqual(1, eventsBefore - eventsAfter);
        }
    }
}
