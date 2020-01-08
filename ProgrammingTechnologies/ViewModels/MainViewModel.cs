using ProgrammingTechnologies.BLL.Managers;
using ProgrammingTechnologies.BO.Models;
using ProgrammingTechnologies.Helpers;
using System.Collections.ObjectModel;

namespace ProgrammingTechnologies.ViewModels
{
    internal class MainViewModel
    {
        public GameViewModel GameViewModel { get; set; }
        public EventViewModel EventViewModel { get; set; }
        public InvitationViewModel InvitationViewModel { get; set; }
        public UserViewModel UserViewModel { get; set; }


        private GameManager GameManager;
        private UserManager UserManager;
        private EventManager EventManager;
        private InvitationManager InvitationManager;

        public MainViewModel()
        {
            User currentUser = SessionManager.GetCurrentUser();

            GameManager = new GameManager(ServiceProvider.GetDatabaseDependentServices);
            EventManager = new EventManager(ServiceProvider.GetDatabaseDependentServices);
            InvitationManager = new InvitationManager(ServiceProvider.GetDatabaseDependentServices);
            UserManager = new UserManager(ServiceProvider.GetDatabaseDependentServices);

            ObservableCollection<Game> games = new ObservableCollection<Game>(GameManager.GetAllManagedObjectsWhere($"user_id = {currentUser.Id}"));
            ObservableCollection<User> users = new ObservableCollection<User>(UserManager.GetAllManagedObjects());
            ObservableCollection<Event>  events = new ObservableCollection<Event>(EventManager.GetAllManagedObjectsWhere($"user_id = {currentUser.Id}"));
            ObservableCollection<Invitation> invitations = new ObservableCollection<Invitation>(InvitationManager.GetAllManagedObjectsWhere($"user_id = {currentUser.Id}"));

            GameViewModel = new GameViewModel(ref currentUser, ref games, ref users);
            EventViewModel = new EventViewModel(ref currentUser, ref games, ref events);
            InvitationViewModel = new InvitationViewModel(ref currentUser, ref invitations, ref events, ref users);
            UserViewModel = new UserViewModel(ref currentUser, ref users);
        }
    }
}
