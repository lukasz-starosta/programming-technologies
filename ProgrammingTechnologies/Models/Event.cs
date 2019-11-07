using System;

namespace ProgrammingTechnologies.Models
{
    public class Event : Model
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
    }
}
