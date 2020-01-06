using System;
using System.ComponentModel;

namespace ProgrammingTechnologies.BO.Models
{
    public class Event : Model, IDataErrorInfo
    {
        private string _title;
        private string _description;
        private DateTime _date;
        private int _userId;
        private int _gameId;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged("Date");
            }
        }

        public int UserId
        {
            get { return _userId; }
            set
            {
                _userId = value;
                OnPropertyChanged("UserId");
            }
        }

        public int GameId
        {
            get { return _gameId; }
            set
            {
                _gameId = value;
                OnPropertyChanged("GameId");
            }
        }

        private Game _game;
        public Game Game { get { return _game; } set { _game = value; GameId = _game.Id; } }

        private User _gameOwner;
        public User GameOwner { get { return _gameOwner; } set { _gameOwner = value; UserId = _gameOwner.Id; } }

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
        protected static readonly string[] ValidatedProperties =
        {
            "Title",
            "Description",
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

        public string GetValidationError(String propertyName)
        {
            string error = null;

            switch (propertyName)
            {
                case "Title":
                    error = ValidateTitle();
                    break;
                case "Description":
                    error = ValidateDescription();
                    break;
            }

            return error;
        }

        private string ValidateTitle()
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                return "Title cannot be empty.";
            }
            if (Title.Length > 50)
            {
                return "Title cannot be longer than 50 characters.";
            }
            return null;
        }

        private string ValidateDescription()
        {
            if (Description.Length > 1000)
            {
                return "Description cannot be longer than 1000 characters.";
            }
            return null;
        }

        #endregion
    }
}
