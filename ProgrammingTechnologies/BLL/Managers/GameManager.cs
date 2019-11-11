using ProgrammingTechnologies.BO.Models;
using ProgrammingTechnologies.Helpers;
using System.Collections.Generic;

namespace ProgrammingTechnologies.BLL.Managers
{
    public class GameManager : ManagerBase<Game>
    {
        public GameManager(ServiceProvider.OutServices initializeServices) : base(initializeServices)
        {
        }

        #region ManagerBase

        public override void CreateManagedObject(ref Game _object)
        {
            gameService.CreateServicedObject(ref _object);
        }

        public override void DeleteManagedObject(Game _object)
        {
            gameService.DeleteServicedObject(_object);
        }

        public override void DeleteManagedObjectWhere(string condition)
        {
            gameService.DeleteServicedObjectWhere(condition);
        }

        public override List<Game> GetAllManagedObjects()
        {
            return gameService.GetAllServicedObjects();
        }

        public override List<Game> GetAllManagedObjectsWhere(string condition)
        {
            return gameService.GetAllServicedObjectsWhere(condition);
        }

        public override Game GetManagedObjectWhere(string condition)
        {
            return gameService.GetServicedObjectWhere(condition);
        }

        public override void UpdateManagedObject(ref Game _object)
        {
            gameService.UpdateServicedObject(ref _object);
        }

        #endregion

        public User GetGameOwner(Game game)
        {
            return userService.GetServicedObjectWhere($"id = {game.UserId}");
        }
    }
}
