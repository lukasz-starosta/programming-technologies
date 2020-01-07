using ProgrammingTechnologies.BLL.Managers;
using ProgrammingTechnologies.BO.Models;
using ProgrammingTechnologies.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingTechnologies.ViewModels
{
    internal class InvitationViewModel : ViewModel<Invitation>
    {
        // TODO: get this from session manager
        public User CurrentUser { get; set; }
        private InvitationManager InvitationManager { get; set; }
        private UserManager UserManager { get; set; }
        private EventManager EventManager { get; set; }
        public InvitationViewModel()
        {
            Name = "Invitations";
            InvitationManager = new InvitationManager(ServiceProvider.GetDatabaseDependentServices);
            UserManager = new UserManager(ServiceProvider.GetDatabaseDependentServices);
            EventManager = new EventManager(ServiceProvider.GetDatabaseDependentServices);

            CurrentUser = new User() { Id = 135, Name = "Lukasz", LastName = "Starosta" };

            Users = new ObservableCollection<User>(UserManager.GetAllManagedObjects());
            Events = new ObservableCollection<Event>(EventManager.GetAllManagedObjects());

            Items = new ObservableCollection<Invitation>(InvitationManager.GetAllManagedObjects());
            SelectedItem = Items[0];

            foreach (Invitation i in Items)
            {
                i.Event = GetInvitationEvent(i);
                i.User = GetInvitationUser(i);
                i.Title = i.CreateTitle();
            }

            SubmitCommand = new RelayCommand(() => Task.Run(() => UpdateItem()), SelectedItem.isValid);
            AddCommand = new RelayCommand(() => Task.Run(() => AddItem()));
            DeleteCommand = new RelayCommand(() => Task.Run(() => DeleteItem()), CanDeleteItem);
        }

        public ObservableCollection<Event> Events
        {
            get; set;

        }
        public Event SelectedEvent { get; set; }

        public ObservableCollection<User> Users
        {
            get; set;
        }
        public User SelectedUser { get; set; }

        private User GetInvitationUser(Invitation i)
        {
            return Users.Where(User => User.Id == i.UserId).First();
        }

        private Event GetInvitationEvent(Invitation i)
        {
            return Events.Where(Event => Event.Id == i.EventId).First();
        }

        protected override void AddItem()
        {
            Invitation newInvitation = new Invitation()
            {
                UserId = CurrentUser.Id,
                EventId = Events[0].Id
            };

            InvitationManager.CreateManagedObject(ref newInvitation);

            // GUI can only be updated by using Dispatcher
            App.Current.Dispatcher.Invoke(() =>
            {
                Items.Add(newInvitation);
                newInvitation.Event = GetInvitationEvent(newInvitation);
                newInvitation.User = GetInvitationUser(newInvitation);
                newInvitation.Title = newInvitation.CreateTitle();
                SelectedItem = newInvitation;
            });
        }
        protected override void UpdateItem()
        {
            Invitation invitationToUpdate = SelectedItem;
            InvitationManager.UpdateManagedObject(ref invitationToUpdate);
            SelectedItem.Event = GetInvitationEvent(invitationToUpdate);
            SelectedItem.User = GetInvitationUser(invitationToUpdate);
            SelectedItem.Title = SelectedItem.CreateTitle();
        }

        protected override void DeleteItem()
        {
            int currentItemIndex = Items.IndexOf(SelectedItem);
            // Get previous game (but game at 0 if there is no game left)
            int previousItemIndex = currentItemIndex > 0 ? currentItemIndex - 1 : 0;
            InvitationManager.DeleteManagedObject(SelectedItem);

            // GUI can only be updated by using Dispatcher
            App.Current.Dispatcher.Invoke(() =>
            {
                Items.Remove(SelectedItem);
                SelectedItem = Items[previousItemIndex];
            });
        }
    }
}