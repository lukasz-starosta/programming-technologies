using System.Collections.Generic;

namespace ProgrammingTechnologies.DAL.Services
{
    public interface IService<T>
    {
        #region CRUD

        void CreateServicedObject(ref T _object);
        T GetServicedObjectWhere(string condition);
        void UpdateServicedObject(ref T _object);
        void DeleteServicedObjectWhere(string condition);
        void DeleteServicedObject(T _object);

        #endregion

        List<T> GetAllServicedObjects();
        List<T> GetAllServicedObjectsWhere(string condition);
    }
}
