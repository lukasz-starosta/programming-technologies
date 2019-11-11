using ProgrammingTechnologies.Helpers;
using ProgrammingTechnologies.BO.Models;
using System.Windows.Input;

namespace ProgrammingTechnologies.ViewModels
{
    public class SessionViewModel
    {
        public User User { get; }

        public SessionViewModel()
        {
            User = new User()
            {
                Email = string.Empty,
                Name = string.Empty,
                LastName = string.Empty
            };

            RegisterUserCommand = new RelayCommand(
                () =>
                {
                    // Save user to database if applicable.
                    // Save user's id to retrieve logged in user form everywhere in application.
                    //DatabaseService.CurrentUserId = User.Id;
                },
                () =>
                {
                    if (User == null) { return false; }
                    return !string.IsNullOrEmpty(User.Name) && !string.IsNullOrEmpty(User.LastName) && !string.IsNullOrEmpty(User.Password);
                });
        }

        public ICommand RegisterUserCommand
        {
            get;
            private set;
        }
    }
}
