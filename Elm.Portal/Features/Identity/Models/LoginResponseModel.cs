
using System;

namespace Appo.Server.Features.Identity.Models
{
    public class LoginResponseModel
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string id { get; set; }
        public int? CompanyId { get; set; }
        public int? JobseekerId { get; set; }


    }
}
