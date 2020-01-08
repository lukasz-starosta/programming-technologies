using ProgrammingTechnologies.Helpers;
using ProgrammingTechnologies.BO.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;

namespace ProgrammingTechnologies.ViewModels
{
    abstract class ViewModel<T> : INotifyPropertyChanged
    {
        public User CurrentUser { get; set; }
        public string Name { get; set; }
        public ObservableCollection<T> Items { get; set; }
        private T _selectedItem;
        public T SelectedItem { get => _selectedItem; set { _selectedItem = value; OnPropertyChanged("SelectedItem"); } }


        public ICommand SubmitCommand { get; protected set; }
        public ICommand AddCommand { get; protected set; }
        public ICommand DeleteCommand { get; protected set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected abstract void AddItem();
        protected abstract void UpdateItem();
        protected abstract void DeleteItem();
        
        // Ensure at least 1 event is present
        protected bool CanDeleteItem() { return Items.Count > 1; }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
