using ProgrammingTechnologies.BO.Models;
using ProgrammingTechnologies.Helpers;
using System.Collections.Generic;

namespace ProgrammingTechnologies.BLL.Managers
{
    public class InvitationManager : ManagerBase<Invitation>
    {
        public InvitationManager(ServiceProvider.OutServices initializeServices) : base(initializeServices)
        {
        }

        #region ManagerBase

        public override void CreateManagedObject(ref Invitation _object)
        {
            invitationService.CreateServicedObject(ref _object);
        }

        public override void DeleteManagedObject(Invitation _object)
        {
            invitationService.DeleteServicedObject(_object);
        }

        public override void DeleteManagedObjectWhere(string condition)
        {
            invitationService.DeleteServicedObjectWhere(condition);
        }

        public override List<Invitation> GetAllManagedObjects()
        {
            return invitationService.GetAllServicedObjects();
        }

        public override List<Invitation> GetAllManagedObjectsWhere(string condition)
        {
            return invitationService.GetAllServicedObjectsWhere(condition);
        }

        public override Invitation GetManagedObjectWhere(string condition)
        {
            return invitationService.GetServicedObjectWhere(condition);
        }

        public override void UpdateManagedObject(ref Invitation _object)
        {
            invitationService.UpdateServicedObject(ref _object);
        }

        #endregion

        public Event GetInvitationEvent(Invitation invitation)
        {
            return eventService.GetServicedObjectWhere($"id = {invitation.EventId}");
        }

        public User GetInvitationUser(Invitation invitation)
        {
            return userService.GetServicedObjectWhere($"id = {invitation.UserId}");
        }
    }
}
