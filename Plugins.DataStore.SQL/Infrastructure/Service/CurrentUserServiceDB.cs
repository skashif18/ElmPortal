using CoreBusiness.Models;
using Microsoft.AspNetCore.Http;
using Plugins.DataStore.SQL.Generic;
using Plugins.DataStore.SQL.Infrastructure.Extensions;
using System.Linq;
using System.Security.Claims;
using UseCases.DataStorePluginInterfaces.Generic;

namespace Plugins.DataStore.SQL.Infrastructure.Services
{

    public class CurrentUserServiceDB : ICurrentUserServiceDB
    {
        private readonly ClaimsPrincipal user;
        private GenericRepository<Employee> repository;

        public CurrentUserServiceDB(IHttpContextAccessor httpContextAccessor)
        {
            this.user = httpContextAccessor.HttpContext?.User;
            
            
        }

        public string GetId()
            => user?.GetId();

        public string GetUserName()
        => user?.Identity?.Name;


        public int GetEmployeeId()
           => repository.GetAll().Where(m => m.Email == user.Identity.Name).FirstOrDefault().Id;

    }
}
