using CoreBusiness;
using CoreBusiness.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using UseCases.DataStorePluginInterfaces.Generic;

namespace Plugins.DataStore.SQL.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Entity
    {
        private readonly PortalContext db;
        private readonly Response response = new();
        public GenericRepository(PortalContext _db)
        {
            db = _db;
        }

        public Response Create(T model)
        {
            try
            {
                db.Add(model);
                db.SaveChanges();
                response.IsSuccess = true;
                response.Message = "Added Successfully";
                response.Objects = model;
                response.Id = model.Id;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error: " + ex.Message;
            }

            return response;
        }

        public Response Delete(object Id)
        {
            T _model = db.Set<T>().Find(Id);
            if (_model == null)
            {
                response.IsSuccess = false;
                response.Message = "Error: Data not found with this Id:  - " + Id;
                return response;
            }
            try
            {

                db.Set<T>().Remove(_model);
                db.SaveChanges();
                response.IsSuccess = true;
                response.Message = "Record Deleted Successfully .";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error: " + ex.Message;
            }
            return response;
        }

        public IQueryable<T> GetAll()
        => db.Set<T>();

        public T GetById(object id)
            => db.Set<T>().Find(id);

        public Response Update(T model)
        {
            //var _model = db.Set<T>().Find(model.Id);
            if (model != null)
            {
                try
                {
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                    response.IsSuccess = true;
                    response.Message = "Updated  Successfully";
                }
                catch (Exception ex)
                {
                    response.IsSuccess = false;
                    response.Message = "Error: " + ex.Message;
                }
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Error: Data not found with this Id:  - " + model.Id;
            }

            return response;
        }
    }
}
