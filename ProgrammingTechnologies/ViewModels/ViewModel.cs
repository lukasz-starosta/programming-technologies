using ProgrammingTechnologies.Helpers;
using ProgrammingTechnologies.BO.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProgrammingTechnologies.ViewModels
{
    abstract class ViewModel<T>
    {
        public string Name { get; set; }
        public ObservableCollection<T> Items { get; set; }
        public T SelectedItem { get; set; }


        public ICommand SubmitCommand { get; protected set; }
        public ICommand AddCommand { get; protected set; }
        public ICommand DeleteCommand { get; protected set; }

        protected abstract void AddItem();
        protected abstract void UpdateItem();
        protected abstract void DeleteItem();
        
        // Ensure at least 1 event is present
        protected bool CanDeleteItem() { return Items.Count > 1; }
    }
}
