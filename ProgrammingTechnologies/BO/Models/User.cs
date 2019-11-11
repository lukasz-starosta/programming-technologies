using ProgrammingTechnologies.Helpers;

namespace ProgrammingTechnologies.BO.Models
{
    public class User : Model
    {
        // Private fields holding values for public properties
        private string _name;
        private string _lastName;
        private string _email;
        private string _password;

        // Constructor for use with object initializer
        public User() { }

        // Public properties for safe exposition of data fields
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
    }
}
