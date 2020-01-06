using ProgrammingTechnologies.BLL.Managers;
using ProgrammingTechnologies.BO.Models;
using ProgrammingTechnologies.Helpers;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProgrammingTechnologies.ViewModels
{
    internal class EventViewModel : ViewModel
    {
        // TODO: get this from session manager
        public User CurrentUser { get; set; }
        private GameManager GameManager { get; set; }
        private EventManager EventManager { get; set; }
        public EventViewModel()
        {
            Name = "Events";
            GameManager = new GameManager(ServiceProvider.GetDatabaseDependentServices);
            EventManager = new EventManager(ServiceProvider.GetDatabaseDependentServices);

            CurrentUser = new User() { Id = 135, Name = "Lukasz", LastName = "Starosta" };
            Games = new ObservableCollection<Game>(GameManager.GetAllManagedObjectsWhere($"user_id = {CurrentUser.Id}"));

            Events = new ObservableCollection<Event>(EventManager.GetAllManagedObjects());
            SelectedEvent = Events[0];

            foreach (Event e in Events)
            {
                e.Game = GetEventGame(e);
            }

            SubmitEventCommand = new RelayCommand(() => Task.Run(() => UpdateEvent()), () => SelectedEvent.IsValid);
            AddEventCommand = new RelayCommand(() => Task.Run(() => AddEvent()));
            DeleteEventCommand = new RelayCommand(() => Task.Run(() => DeleteEvent()), CanDeleteEvent);
        }

        public Event SelectedEvent
        {
            get; set;
        }

        public ObservableCollection<Event> Events
        {
            get; set;

        }
        public ObservableCollection<Game> Games
        {
            get; set;

        }
        public Game SelectedGame { get; set; }

        public ObservableCollection<string> Categories
        {
            get; set;
        }

        private Game GetEventGame(Event e)
        {
            return Games.Where(Game => Game.Id == SelectedEvent.GameId).First();
        }

        public ICommand SubmitEventCommand { get; private set; }
        public ICommand AddEventCommand { get; private set; }
        public ICommand DeleteEventCommand { get; private set; }

        private void AddEvent()
        {
            Event newEvent = new Event()
            {
                Title = "New event",
                Description = "",
                Date = System.DateTime.Now,
                UserId = CurrentUser.Id,
                GameId = Games[0].Id
            };

            EventManager.CreateManagedObject(ref newEvent);

            // GUI can only be updated by using Dispatcher
            App.Current.Dispatcher.Invoke(() =>
            {
                Events.Add(newEvent);
                newEvent.Game = GetEventGame(newEvent);
                SelectedEvent = newEvent;
            });
        }
        private void UpdateEvent()
        {
            Event eventToUpdate = SelectedEvent;
            EventManager.UpdateManagedObject(ref eventToUpdate);
            SelectedEvent.Game = GetEventGame(eventToUpdate);
        }

        // Ensure at least 1 game is present
        private bool CanDeleteEvent() { return Events.Count > 1; }
        private void DeleteEvent()
        {
            int currentEventIndex = Events.IndexOf(SelectedEvent);
            // Get previous game (but game at 0 if there is no game left)
            int previousEventIndex = currentEventIndex > 0 ? currentEventIndex - 1 : 0;
            EventManager.DeleteManagedObject(SelectedEvent);

            // GUI can only be updated by using Dispatcher
            App.Current.Dispatcher.Invoke(() =>
            {
                Events.Remove(SelectedEvent);
                SelectedEvent = Events[previousEventIndex];
            });
        }
    }
}