 using System.Collections.Generic;
using ProgrammingTechnologies.BO.Models;
using ProgrammingTechnologies.Helpers;

namespace ProgrammingTechnologies.BLL.Managers
{
    public class UserManager : ManagerBase<User>
    {
        public UserManager(ServiceProvider.OutServices initalizeServices) : base(initalizeServices)
        {
        }

        #region ManagerBase

        public override void CreateManagedObject(ref User _object)
        {
            userService.CreateServicedObject(ref _object);
        }

        public override void DeleteManagedObject(User _object)
        {
            userService.DeleteServicedObject(_object);
        }

        public override void DeleteManagedObjectWhere(string condition)
        {
            userService.DeleteServicedObjectWhere(condition);
        }

        public override List<User> GetAllManagedObjects()
        {
            return userService.GetAllServicedObjects();
        }

        public override List<User> GetAllManagedObjectsWhere(string condition)
        {
            return userService.GetAllServicedObjectsWhere(condition);
        }

        public override User GetManagedObjectWhere(string condition)
        {
            return userService.GetServicedObjectWhere(condition);
        }

        public override void UpdateManagedObject(ref User _object)
        {
            userService.UpdateServicedObject(ref _object);
        }

        #endregion

        public List<Game> GetGamesOf(User user)
        {
            return gameService.GetAllServicedObjectsWhere($"user_id = {user.Id}");
        }

        public List<Event> GetEventsOrganizedBy(User user)
        {
            return eventService.GetAllServicedObjectsWhere($"user_id = {user.Id}");
        }

        public List<Event> GetInvitedEventsOf(User user)
        {
            List<Invitation> invitations = invitationService.GetAllServicedObjectsWhere($"user_id = {user.Id}");
            string eventIds = "(";
            for (int i = 0; i < invitations.Count; i++)
            {
                if (i == invitations.Count - 1)
                {
                    eventIds += $"{invitations[i].EventId})";
                }
                else
                {
                    eventIds += $"{invitations[i].EventId}, ";
                }
            }
            return eventService.GetAllServicedObjectsWhere($"id in {eventIds}");
        }

        public void InviteUserToEvent(User user, Event _event)
        {
            Invitation invitation = new Invitation()
            {
                UserId = user.Id,
                EventId = _event.Id
            };
            invitationService.CreateServicedObject(ref invitation);
        }
    }
}
