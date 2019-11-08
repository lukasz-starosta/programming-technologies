using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgrammingTechnologies.Models;
using ProgrammingTechnologies.Services;
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
            UserService userService = new UserService();
            GameService gameService = new GameService();

            userService.CreateUser(new User()
            {
                Name = "EventServiceTest",
                LastName = "Test",
                Email = "test@tes.com",
                Password = "password",
            });

            user = userService.GetUserWhere("name = 'EventServiceTest'");

            gameService.CreateGame(new Game()
            {
                Title = "EventServiceTestGame",
                Description = "Test",
                Category = 1,
                UserId = user.Id
            });

            game = gameService.GetGameWhere("title = 'EventServiceTestGame'");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            UserService userService = new UserService();
            GameService gameService = new GameService();

            gameService.DeleteGame(game);
            userService.DeleteUser(user);
        }

        [TestMethod]
        public void TestCreateEvent()
        {
            Event _event = getNewEvent();

            EventService eventService = new EventService();

            eventService.CreateEvent(_event);
            eventService.DeleteEventWhere("title = 'EventForEventService'");
        }

        [TestMethod]
        public void TestUpdateEvent()
        {
            Event _event = getNewEvent();

            EventService eventService = new EventService();

            eventService.CreateEvent(_event);
            _event = eventService.GetEventWhere("title = 'EventForEventService'");

            _event.Description = "Changed description.";

            eventService.UpdateEvent(_event);
            _event = eventService.GetEventWhere($"id = {_event.Id}");

            Assert.AreEqual("Changed description.", _event.Description);

            eventService.DeleteEventWhere("title = 'EventForEventService'");
        }

        [TestMethod]
        public void TestGetEvent()
        {
            Event _event = getNewEvent();

            EventService eventService = new EventService();

            eventService.CreateEvent(_event);
            Event result = eventService.GetEventWhere("title = 'EventForEventService'");

            Assert.AreEqual(_event.Title, result.Title);

            eventService.DeleteEventWhere("title = 'EventForEventService'");
        }

        [TestMethod]
        public void TestGetAllEventsWhere()
        {
            EventService eventService = new EventService();

            for (int i = 0; i < 10; i++)
            {
                Event _event = getNewEvent();
                eventService.CreateEvent(_event);
            }

            List<Event> events = eventService.GetAllEventsWhere("title = 'EventForEventService'");

            Assert.AreEqual(10, events.Count);

            foreach (Event e in events)
            {
                Assert.AreEqual("EventForEventService", e.Title);
            }

            eventService.DeleteEventWhere("title = 'EventForEventService'");
        }

        [TestMethod]
        public void TestGetAll()
        {
            EventService eventService = new EventService();

            int eventsBefore = eventService.GetAllEvents().Count;

            for (int i = 0; i < 10; i++)
            {
                Event _event = getNewEvent();
                eventService.CreateEvent(_event);
            }

            int eventsAfter = eventService.GetAllEvents().Count;

            Assert.AreEqual(10, eventsAfter - eventsBefore);

            eventService.DeleteEventWhere("title = 'EventForEventService'");
        }

        [TestMethod]
        public void TestDeleteEvent()
        {
            Event _event = getNewEvent();

            EventService eventService = new EventService();

            eventService.CreateEvent(_event);
            _event = eventService.GetEventWhere("title = 'EventForEventService'");

            int eventsBefore = eventService.GetAllEvents().Count;

            eventService.DeleteEvent(_event);

            int eventsAfter = eventService.GetAllEvents().Count;

            Assert.AreEqual(1, eventsBefore - eventsAfter);
        }
    }
}
