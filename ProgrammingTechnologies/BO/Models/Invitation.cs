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

    }
}
