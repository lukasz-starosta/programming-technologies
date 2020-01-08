using ProgrammingTechnologies.BLL.Managers;
using ProgrammingTechnologies.BO.Models;
using ProgrammingTechnologies.Helpers;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingTechnologies.ViewModels
{
    internal class InvitationViewModel : ViewModel<Invitation>
    {
        private InvitationManager InvitationManager { get; set; }
        public InvitationViewModel(ref User currentUser, ref ObservableCollection<Invitation> invitations, ref ObservableCollection<Event> events, ref ObservableCollection<User> users)
        {
            Name = "Invitations";
            Items = invitations;
            Events = events;
            Users = users;

            CurrentUser = currentUser;

            InvitationManager = new InvitationManager(ServiceProvider.GetDatabaseDependentServices);
  
            if (Items.Count > 0)
            {
                SelectedItem = Items[0];

                foreach (Invitation i in Items)
                {
                    i.Event = GetInvitationEvent(i);
                    i.User = GetInvitationUser(i);
                    i.Title = i.CreateTitle();
                }
            }

            SubmitCommand = new RelayCommand(() => Task.Run(() => UpdateItem()), () => SelectedItem != null && SelectedItem.isValid());
            AddCommand = new RelayCommand(() => Task.Run(() => AddItem()));
            DeleteCommand = new RelayCommand(() => Task.Run(() => DeleteItem()), () => SelectedItem != null && CanDeleteItem());
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