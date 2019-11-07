using System.ComponentModel;

namespace ProgrammingTechnologies.Models
{
    public class Game : INotifyPropertyChanged
    {
        private int _id;
        private string _title;
        private string _description;
        private int _userId;
        private int _category;

        public int Id
        {
            get { return _id;  }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

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

        public int Category
        {
            get { return _category; }
            set
            {
                _category = value;
                OnPropertyChanged("Category");
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
