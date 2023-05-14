using CoreBusiness;
using CoreBusiness.Base;
using System.Collections.Generic;
using System.Linq;

namespace UseCases.DataStorePluginInterfaces.Generic
{
    public interface IGenericRepository<T> where T : Entity
    {
        Response Create(T model);
        Response Update(T model);
        IQueryable<T> GetAll();
        T GetById(object id);
        Response Delete(object Id);
    }
}
