using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgrammingTechnologies.Models;
using ProgrammingTechnologies.Services;
using System;
using System.Collections.Generic;

namespace ProgrammingTechnologiesTest.Services
{
    [TestClass]
    public class InvitationServiceTest
    {
        private User user;
        private Game game;
        private Event _event;

        private Invitation GetNewInvitation()
        {
            return new Invitation()
            {
                UserId = user.Id,
                EventId = _event.Id
            };
        }

        [TestInitialize]
        public void TestInitialize()
        {
            DatabaseService databaseService = new DatabaseService();
            UserService userService = new UserService(databaseService);
            GameService gameService = new GameService(databaseService);
            EventService eventService = new EventService(databaseService);

            user = new User()
            {
                Name = "EventServiceTest",
                LastName = "Test",
                Email = "test@tes.com",
                Password = "password"
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

            _event = new Event()
            {
                Title = "EventForEventService",
                Description = "Test",
                Date = DateTime.Now,
                UserId = user.Id,
                GameId = game.Id
            };
            eventService.CreateServicedObject(ref _event);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            DatabaseService databaseService = new DatabaseService();
            UserService userService = new UserService(databaseService);
            GameService gameService = new GameService(databaseService);
            EventService eventService = new EventService(databaseService);

            databaseService.ExecuteInstruction("delete from Invitations");
            eventService.DeleteServicedObject(_event);
            gameService.DeleteServicedObject(game);
            userService.DeleteServicedObject(user);
        }

        [TestMethod]
        public void TestCreateEvent()
        {
            Invitation invitation = GetNewInvitation();

            InvitationService invitationService = new InvitationService(new DatabaseService());
            invitationService.CreateServicedObject(ref invitation);

            Assert.IsNotNull(invitation.Id);
        }

        [TestMethod]
        public void TestUpdateInvitation()
        {
            Invitation invitation = GetNewInvitation();

            DatabaseService databaseService = new DatabaseService();
            InvitationService invitationService = new InvitationService(databaseService);

            invitationService.CreateServicedObject(ref invitation);

            UserService userService = new UserService(databaseService);
            User replacement = new User()
            {
                Name = "Replacement",
                LastName = "Test",
                Email = "test@test.com",
                Password = "password"
            };
            userService.CreateServicedObject(ref replacement);
            replacement = userService.GetServicedObjectWhere("name = 'Replacement'");

            Assert.AreEqual(user.Id, invitation.UserId);

            invitation.UserId = replacement.Id;

            invitationService.UpdateServicedObject(ref invitation);

            Assert.AreEqual(replacement.Id, invitation.UserId);

            invitationService.DeleteServicedObjectWhere($"user_id = {replacement.Id} and event_id = {_event.Id}");
            userService.DeleteServicedObject(replacement);
        }

        [TestMethod]
        public void TestGetInvitation()
        {
            Invitation invitation = GetNewInvitation();

            InvitationService invitationService = new InvitationService(new DatabaseService());
            invitationService.CreateServicedObject(ref invitation);

            Invitation result = invitationService.GetServicedObjectWhere($"user_id = {user.Id} and event_id = {_event.Id}");
            Assert.AreEqual(invitation.UserId, result.UserId);
            Assert.AreEqual(invitation.EventId, result.EventId);

            invitationService.DeleteServicedObject(result);
        }

        [TestMethod]
        public void TestGetAllInvitationsWhere()
        {
            InvitationService invitationService = new InvitationService(new DatabaseService());

            for (int i = 0; i < 10; i++)
            {
                Invitation invitation = GetNewInvitation();
                invitationService.CreateServicedObject(ref invitation);
            }

            List<Invitation> invitations = invitationService.GetAllServicedObjectsWhere($"user_id = {user.Id}");

            Assert.AreEqual(10, invitations.Count);

            foreach (Invitation i in invitations)
            {
                Assert.AreEqual(user.Id, i.UserId);
                Assert.AreEqual(_event.Id, i.EventId);
            }

            invitationService.DeleteServicedObjectWhere($"user_id = {user.Id}");
        }

        [TestMethod]
        public void TestGetAllInvitations()
        {
            InvitationService invitationService = new InvitationService(new DatabaseService());

            for (int i = 0; i < 10; i++)
            {
                Invitation invitation = GetNewInvitation();
                invitationService.CreateServicedObject(ref invitation);
            }

            List<Invitation> invitations = invitationService.GetAllServicedObjectsWhere($"user_id = {user.Id}");

            Assert.AreEqual(10, invitations.Count);

            invitationService.DeleteServicedObjectWhere($"user_id = {user.Id}");
        }

        [TestMethod]
        public void TestDeleteInvitationWhere()
        {
            Invitation invitation = GetNewInvitation();

            InvitationService invitationService = new InvitationService(new DatabaseService());

            invitationService.CreateServicedObject(ref invitation);

            int invitationsBefore = invitationService.GetAllServicedObjects().Count;

            invitationService.DeleteServicedObject(invitation);

            int invitationsAfter = invitationService.GetAllServicedObjects().Count;

            Assert.AreEqual(1, invitationsBefore - invitationsAfter);
        }
    }
}
