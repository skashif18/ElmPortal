using Appo.Server.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Plugins.DataStore.SQL;
using System.Linq;
using System.Security.Claims;
using UseCases.DataStorePluginInterfaces;

namespace Appo.Server.Infrastructure.Services
{

    public class CurrentUserService : ICurrentUserService
    {
        private readonly ClaimsPrincipal user;
        private readonly PortalContext db;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor, PortalContext _db)
        {
            this.user = httpContextAccessor.HttpContext?.User;
            this.db = _db;
        }

        public string GetId()
            => user?.GetId();

        public string GetUserName()
            => user?.Identity?.Name;

        public int GetEmployeeId()
            => db.Employees.Where(m => m.Email == user.Identity.Name).FirstOrDefault().Id;

    }
}
