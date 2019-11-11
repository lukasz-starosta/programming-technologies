using ProgrammingTechnologies.BO.Models;

namespace ProgrammingTechnologies.Helpers
{
    public static class SessionManager
    {
        public static int _currentUserId;

        public static void LogInUser(User user)
        {
            _currentUserId = user.Id;
        }

        public static void EndSession()
        {
            _currentUserId = 0;
        }
    }
}
