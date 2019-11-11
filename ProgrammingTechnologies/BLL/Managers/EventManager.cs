using ProgrammingTechnologies.BO.Models;
using ProgrammingTechnologies.Helpers;
using System;
using System.Collections.Generic;

namespace ProgrammingTechnologies.BLL.Managers
{
    public class EventManager : ManagerBase<Event>
    {
        public EventManager(ServiceProvider.OutServices initializeServices) : base(initializeServices)
        {
        }

        #region ManagerBase

        public override void CreateManagedObject(ref Event _object)
        {
            eventService.CreateServicedObject(ref _object);
        }

        public override void DeleteManagedObject(Event _object)
        {
            eventService.DeleteServicedObject(_object);
        }

        public override void DeleteManagedObjectWhere(string condition)
        {
            eventService.DeleteServicedObjectWhere(condition);
        }

        public override List<Event> GetAllManagedObjects()
        {
            return eventService.GetAllServicedObjects();
        }

        public override List<Event> GetAllManagedObjectsWhere(string condition)
        {
            return eventService.GetAllServicedObjectsWhere(condition);
        }

        public override Event GetManagedObjectWhere(string condition)
        {
            return eventService.GetServicedObjectWhere(condition);
        }

        public override void UpdateManagedObject(ref Event _object)
        {
            eventService.UpdateServicedObject(ref _object);
        }

        #endregion

        public Game GetEventGame(Event _event)
        {
            return gameService.GetServicedObjectWhere($"id = {_event.GameId}");
        }

        public User GetEventOwner(Event _event)
        {
            return userService.GetServicedObjectWhere($"id = {_event.UserId}");
        }

        public List<User> GetInvitedUsers(Event _event)
        {
            List<Invitation> invitations = invitationService.GetAllServicedObjectsWhere($"event_id = {_event.Id}");
            string userIds = "(";
            for (int i = 0; i < invitations.Count; i++)
            {
                if (i == invitations.Count - 1)
                {
                    userIds += $"{invitations[i].EventId})";
                }
                else
                {
                    userIds += $"{invitations[i].EventId}, ";
                }
            }
            Console.WriteLine($"id in {userIds}");
            return userService.GetAllServicedObjectsWhere($"id in {userIds}");
        }
    }
}
