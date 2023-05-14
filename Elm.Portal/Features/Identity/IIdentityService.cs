using Appo.Server.Features.Identity.Models;
using CoreBusiness;

namespace Appo.Server.Features.Identity
{
    public interface IIdentityService
    {
        string GenerateJwtToken(string userId, string userName, string secret);
        
    }
}
