using ProgrammingTechnologies.BO.Models;
using ProgrammingTechnologies.DAL.Services;

namespace ProgrammingTechnologiesTest.Helpers
{
    public class DatabaseSeeder
    {
        public static void SeedDatabase()
        {
            DatabaseService databaseService = new DatabaseService();
            UserService userService = new UserService(databaseService);
            GameService gameService = new GameService(databaseService);
            EventService eventService = new EventService(databaseService);
            InvitationService invitationService = new InvitationService(databaseService);

            User user = new User()
            {
                Name = "Tester",
                LastName = "Bestes",
                Email = "test@test.com",
                Password = "password"
            };

            Game game = new Game()
            {
                Title = "Test Title",
                Description = "Test Description",
                Category = 2,
            };

            Event _event = new Event()
            {
                Title = "EventForEventService",
                Description = "Test",
                Date = System.DateTime.Now,
            };

            Invitation invitation = new Invitation();

            for (int i = 0; i < 10; i++)
            {
                userService.CreateServicedObject(ref user);
                game.UserId = user.Id;
                gameService.CreateServicedObject(ref game);
                _event.UserId = user.Id;
                _event.GameId = game.Id;
                eventService.CreateServicedObject(ref _event);
                if (i > 1)
                {
                    invitation.EventId = _event.Id;
                    invitation.UserId = user.Id;
                    invitationService.CreateServicedObject(ref invitation);
                }
            }

        }

        public static void CleanDatabase()
        {
            DatabaseService databaseService = new DatabaseService();

            databaseService.ExecuteInstruction("delete from Invitations");
            databaseService.ExecuteInstruction("delete from Events");
            databaseService.ExecuteInstruction("delete from Games");
            databaseService.ExecuteInstruction("delete from Users");
        }
    }
}
