using ProgrammingTechnologies.Helpers;
using System.ComponentModel;

namespace ProgrammingTechnologies.BO.Models
{
    public class User : Model, IDataErrorInfo
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
                if (_password == null)
                {
                    _password = value;
                } else 
                {
                _password = SecurePasswordHasher.Hash(value);
                }
                OnPropertyChanged("Password");
            }
        }

        private string _readablePassword;
        public string ReadablePassword
        {
            get { return _readablePassword != null ? _readablePassword : "********" ; }
            set
            {
                _readablePassword = value;
                Password = value;
                OnPropertyChanged("ReadablePassword");
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

        public string FullName { get { return $"{Name} {LastName}"; } }

        #region IDataErrorInfoMembers

        string IDataErrorInfo.Error
        {
            get { return null; }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                return GetValidationError(propertyName);
            }
        }

        #endregion

        #region Validation
        private static readonly string[] ValidatedProperties =
        {
            "Name",
            "LastName",
            "Email",
            "ReadablePassword"
        };

        public override bool isValid()
        {
            foreach (string property in ValidatedProperties)
            {
                if (GetValidationError(property) != null)
                {
                    return false;
                }
            }
            return true;
        }

        public string GetValidationError(string propertyName)
        {
            string error = null;

            switch (propertyName)
            {
                case "Name":
                    error = ValidateName();
                    break;
                case "LastName":
                    error = ValidateLastName();
                    break;
                case "Email":
                    error = ValidateEmail();
                    break;
                case "ReadablePassword":
                    error = ValidatePassword();
                    break;
            }

            return error;
        }

        private string ValidateName()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                return "Name cannot be empty.";
            }
            if (Name.Length > 20)
            {
                return "Name cannot be longer than 20 characters.";
            }
            return null;
        }

        private string ValidateLastName()
        {
            if (string.IsNullOrWhiteSpace(LastName))
            {
                return "Last name cannot be empty.";
            }
            if (LastName.Length > 40)
            {
                return "Last name cannot be longer than 40 characters.";
            }
            return null;
        }
        private string ValidateEmail()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                return "Email cannot be empty.";
            }
            if (Email.Length > 50)
            {
                return "Email cannot be longer than 50 characters.";
            }
            return null;
        }
        private string ValidatePassword()
        {
            if (ReadablePassword.Length < 4)
            {
                return "Password cannot be shorter than 4 characters.";
            }
            if (ReadablePassword.Length > 10)
            {
                return "Password cannot be longer than 10 characters.";
            }
            return null;
        }

        #endregion
    }
}
