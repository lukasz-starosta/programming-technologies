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

            userService.CreateUser(new User()
            {
                Name = "EventServiceTest",
                LastName = "Test",
                Email = "test@tes.com",
                Password = "password"
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

            eventService.CreateEvent(new Event()
            {
                Title = "EventForEventService",
                Description = "Test",
                Date = System.DateTime.Now,
                UserId = user.Id,
                GameId = game.Id
            });

            _event = eventService.GetEventWhere("title = 'EventForEventService'");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            DatabaseService databaseService = new DatabaseService();
            UserService userService = new UserService(databaseService);
            GameService gameService = new GameService(databaseService);
            EventService eventService = new EventService(databaseService);

            eventService.DeleteEvent(_event);
            gameService.DeleteGame(game);
            userService.DeleteUser(user);
        }

        [TestMethod]
        public void TestCreateEvent()
        {
            Invitation invitation = GetNewInvitation();

            InvitationService invitationService = new InvitationService(new DatabaseService());

            invitationService.CreateInvitation(invitation);
            invitationService.DeleteInvitationWhere($"user_id = {user.Id} and event_id = {_event.Id}");
        }

        [TestMethod]
        public void TestUpdateInvitation()
        {
            Invitation invitation = GetNewInvitation();

            DatabaseService databaseService = new DatabaseService();
            InvitationService invitationService = new InvitationService(databaseService);

            invitationService.CreateInvitation(invitation);
            invitation = invitationService.GetInvitationWhere($"user_id = {user.Id} and event_id = {_event.Id}");

            UserService userService = new UserService(databaseService);
            userService.CreateUser(new User()
            {
                Name = "Replacement",
                LastName = "Test",
                Email = "test@test.com",
                Password = "password"
            });
            User replacement = userService.GetUserWhere("name = 'Replacement'");
            Console.WriteLine(replacement.Id);

            Assert.AreEqual(user.Id, invitation.UserId);

            invitation.UserId = replacement.Id;

            invitationService.UpdateInvitation(invitation);
            invitation = invitationService.GetInvitationWhere($"event_id = {_event.Id}");

            Assert.AreEqual(replacement.Id, invitation.UserId);

            invitationService.DeleteInvitationWhere($"user_id = {replacement.Id} and event_id = {_event.Id}");
            userService.DeleteUser(replacement);
        }

        [TestMethod]
        public void TestGetInvitation()
        {
            Invitation invitation = GetNewInvitation();

            InvitationService invitationService = new InvitationService(new DatabaseService());
            invitationService.CreateInvitation(invitation);

            Invitation result = invitationService.GetInvitationWhere($"user_id = {user.Id} and event_id = {_event.Id}");
            Assert.AreEqual(invitation.UserId, result.UserId);
            Assert.AreEqual(invitation.EventId, result.EventId);

            invitationService.DeleteInvitation(result);
        }

        [TestMethod]
        public void TestGetAllInvitationsWhere()
        {
            InvitationService invitationService = new InvitationService(new DatabaseService());

            for (int i = 0; i < 10; i++)
            {
                Invitation invitation= GetNewInvitation();
                invitationService.CreateInvitation(invitation);
            }

            List<Invitation> invitations = invitationService.GetAllinvitationsWhere($"user_id = {user.Id}");

            Assert.AreEqual(10, invitations.Count);

            foreach (Invitation i in invitations)
            {
                Assert.AreEqual(user.Id, i.UserId);
                Assert.AreEqual(_event.Id, i.EventId);
            }

            invitationService.DeleteInvitationWhere($"user_id = {user.Id}");
        }

        [TestMethod]
        public void TestGetAllInvitations()
        {
            InvitationService invitationService = new InvitationService(new DatabaseService());

            for (int i = 0; i < 10; i++)
            {
                Invitation invitation = GetNewInvitation();
                invitationService.CreateInvitation(invitation);
            }

            List<Invitation> invitations = invitationService.GetAllinvitationsWhere($"user_id = {user.Id}");

            Assert.AreEqual(10, invitations.Count);

            invitationService.DeleteInvitationWhere($"user_id = {user.Id}");
        }

        [TestMethod]
        public void TestDeleteInvitationWhere()
        {
            Invitation invitation = GetNewInvitation();

            InvitationService invitationService = new InvitationService(new DatabaseService());

            invitationService.CreateInvitation(invitation);
            invitation = invitationService.GetInvitationWhere($"user_id = {user.Id}");

            int invitationsBefore = invitationService.GetAllInvitations().Count;

            invitationService.DeleteInvitation(invitation);

            int invitationsAfter = invitationService.GetAllInvitations().Count;

            Assert.AreEqual(1, invitationsBefore - invitationsAfter);
        }
    }
}
