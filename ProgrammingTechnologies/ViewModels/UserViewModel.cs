using ProgrammingTechnologies.BLL.Managers;
using ProgrammingTechnologies.BO.Models;
using ProgrammingTechnologies.Helpers;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace ProgrammingTechnologies.ViewModels
{
    internal class UserViewModel : INotifyPropertyChanged
    {
        public string Name { get; set; }

        private UserManager UserManager;

        public event PropertyChangedEventHandler PropertyChanged;

        public User CurrentUser { get; set; }
        public ObservableCollection<User> Users { get; set; }
        public UserViewModel(ref User currentUser, ref ObservableCollection<User> users)
        {
            Name = "User";
            CurrentUser = currentUser;
            Users = users;

            UserManager = new UserManager(ServiceProvider.GetDatabaseDependentServices);

            StartEditing = new RelayCommand(Edit, () => !IsEditing);
            SubmitEditing = new RelayCommand(Submit, () => IsEditing && CurrentUser.isValid());
        }

        private bool _isEditing;
        public bool IsEditing { get => _isEditing; set { _isEditing = value; OnPropertyChanged("IsEditing"); } }

        public ICommand StartEditing { get; private set; }
        public ICommand SubmitEditing { get; private set; }

        private void Edit()
        {
            IsEditing = true;
        }

        private void Submit()
        {
            User user = CurrentUser;

            // GUI can only be updated by using Dispatcher
            App.Current.Dispatcher.Invoke(() =>
            {
                User userToUpdate = Users.First(User => User.Id == CurrentUser.Id);

                UserManager.UpdateManagedObject(ref user);
                CurrentUser = user;
                userToUpdate.Name = CurrentUser.Name;
                userToUpdate.LastName = CurrentUser.LastName;
                userToUpdate.Email = CurrentUser.Email;
                userToUpdate.Password = CurrentUser.Password;

                OnPropertyChanged("CurrentUser");
            });


            IsEditing = false;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}