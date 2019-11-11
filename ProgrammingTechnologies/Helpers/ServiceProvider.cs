using ProgrammingTechnologies.BO.Models;
using ProgrammingTechnologies.DAL.Services;

namespace ProgrammingTechnologies.Helpers
{
    public static class ServiceProvider
    {
        public delegate void OutServices(out IService<User> userService, out IService<Game> gameService, out IService<Event> eventService, out IService<Invitation> invitationService);

        public static void GetDatabaseDependentServices(out IService<User> userService, out IService<Game> gameService, out IService<Event> eventService, out IService<Invitation> invitationService)
        {
            DatabaseService databaseService = new DatabaseService();
            userService = new UserService(databaseService);
            gameService = new GameService(databaseService);
            eventService = new EventService(databaseService);
            invitationService = new InvitationService(databaseService);
        }
    }
}
