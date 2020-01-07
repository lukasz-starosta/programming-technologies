using ProgrammingTechnologies.BLL.Managers;
using ProgrammingTechnologies.BO.Models;
using ProgrammingTechnologies.Helpers;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace ProgrammingTechnologies.ViewModels
{
    public class SessionViewModel : INotifyPropertyChanged
    {
        public User User { get; }

        private UserManager UserManager;

        public SessionViewModel()
        {
            User = new User()
            {
                Name = "Name",
                LastName = "Last name",
                Email = string.Empty,
                ReadablePassword = string.Empty
            };

            UserManager = new UserManager(ServiceProvider.GetDatabaseDependentServices);

            SignUpCommand = new RelayCommand(
                () =>
                {
                    if (UserManager.UserExists(User.Email))
                    {
                        ErrorLabel = "User with this email already exists.";
                    }
                    else
                    {
                        User user = User;
                        UserManager.CreateManagedObject(ref user);
                        SessionManager.LogInUser(user);

                        new MainWindow().Show();

                        // close the login/signup window
                        Application.Current.MainWindow.Close();
                    }
                }, User.isValid);

            LoginCommand = new RelayCommand(() =>
            {
                if (UserManager.VerifyUserLogin(User.Email, User.ReadablePassword))
                {
                    SessionManager.LogInUser(UserManager.GetManagedObjectWhere($"email = '{User.Email}'"));
                    new MainWindow().Show();

                    // close the login/signup window
                    Application.Current.MainWindow.Close();
                }
                else
                {
                    ErrorLabel = "Invalid username or password.";
                }
            }, User.isValid);
        }

        private string _errorLabel;

        public event PropertyChangedEventHandler PropertyChanged;

        public string ErrorLabel
        {
            get => _errorLabel; set
            {
                _errorLabel = value;
                OnPropertyChanged("ErrorLabel");
            }
        }

        public ICommand SignUpCommand
        {
            get;
            private set;
        }

        public ICommand LoginCommand { get; private set; }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
