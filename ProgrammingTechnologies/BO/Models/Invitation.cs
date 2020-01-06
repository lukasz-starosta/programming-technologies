namespace ProgrammingTechnologies.BO.Models
{
    public class Invitation : Model
    {
        private int _userId;
        private int _eventId;

        public int UserId
        {
            get { return _userId; }
            set
            {
                _userId = value;
                OnPropertyChanged("UserId");
            }
        }

        public int EventId
        {
            get { return _eventId; }
            set
            {
                _eventId = value;
                OnPropertyChanged("EventId");
            }
        }

        private Event _event;
        public Event Event { get => _event; set { _event = value; EventId = value.Id; } }

        private User _user;
        public User User { get => _user; set { _user = value; UserId = value.Id; } }

        private string _title;
        public string Title { get { return _title;  } set { _title = value; OnPropertyChanged("Title"); } }
        public string CreateTitle() {
           return $"{User.FullName} to {Event.Title}";
        }

        public override bool isValid()
        {
            return true;
        }
    }
}
