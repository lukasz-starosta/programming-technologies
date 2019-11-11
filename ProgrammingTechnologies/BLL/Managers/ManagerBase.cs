using ProgrammingTechnologies.BO.Models;
using ProgrammingTechnologies.DAL.Services;
using ProgrammingTechnologies.Helpers;
using System.Collections.Generic;

namespace ProgrammingTechnologies.BLL.Managers
{
    public abstract class ManagerBase<T>
    {
        protected IService<User> userService;
        protected IService<Game> gameService;
        protected IService<Event> eventService;
        protected IService<Invitation> invitationService;

        public ManagerBase(ServiceProvider.OutServices initalizeServices)
        {
            initalizeServices(out userService, out gameService, out eventService, out invitationService);
        }

        #region CRUD

        public abstract void CreateManagedObject(ref T _object);
        public abstract T GetManagedObjectWhere(string condition);
        public abstract void UpdateManagedObject(ref T _object);
        public abstract void DeleteManagedObjectWhere(string condition);
        public abstract void DeleteManagedObject(T _object);

        #endregion

        public abstract List<T> GetAllManagedObjects();
        public abstract List<T> GetAllManagedObjectsWhere(string condition);

    }
}
