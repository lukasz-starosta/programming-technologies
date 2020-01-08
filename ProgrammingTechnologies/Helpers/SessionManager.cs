using ProgrammingTechnologies.BO.Models;

namespace ProgrammingTechnologies.Helpers
{
    public static class SessionManager
    {
        public static User _currentUser;

        public static void LogInUser(User user)
        {
            _currentUser = user;
        }

        public static User GetCurrentUser() { return _currentUser; }

        public static void EndSession()
        {
            _currentUser = null;
        }
    }
}
