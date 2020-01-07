using ProgrammingTechnologies.BLL.Managers;
using ProgrammingTechnologies.BO.Models;
using ProgrammingTechnologies.Helpers;
using System.ComponentModel;
using System.Windows.Input;

namespace ProgrammingTechnologies.ViewModels
{
    internal class UserViewModel : INotifyPropertyChanged
    {
        public string Name { get; set; }

        private UserManager UserManager;

        public event PropertyChangedEventHandler PropertyChanged;

        public User CurrentUser { get; set; }
        public UserViewModel()
        {
            Name = "User";
            UserManager = new UserManager(ServiceProvider.GetDatabaseDependentServices);
            CurrentUser = UserManager.GetManagedObjectWhere("id = 135");

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
            UserManager.UpdateManagedObject(ref user);
            CurrentUser = user;
            IsEditing = false;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}