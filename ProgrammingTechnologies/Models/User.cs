using ProgrammingTechnologies.Helpers;
using System;
using System.ComponentModel;

namespace ProgrammingTechnologies.Models
{
    public class User : INotifyPropertyChanged
    {
        // Private fields holding values for public properties
        private string _name;
        private string _lastName;
        private string _email;
        private string _password;
        private int _id;

        // Constructor for use with object initializer
        public User() { }

        // Public properties for safe exposition of data fields
        public int Id
        {
            get { return _id;  }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = SecurePasswordHasher.Hash(value);
                OnPropertyChanged("Password");
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }

        public bool IsPasswordCorrect(string password)
        {
            return SecurePasswordHasher.Verify(password, Password);
        }

        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies about property change by raising PropertyChanged event.
        /// </summary>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
