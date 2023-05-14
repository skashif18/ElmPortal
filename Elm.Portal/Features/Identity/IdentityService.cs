using Appo.Server.Features.Identity.Models;
using AutoMapper;
using CoreBusiness;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UseCases.DataStorePluginInterfaces;

namespace Appo.Server.Features.Identity
{
    public class IdentityService : IIdentityService
    {


        
      
        private readonly IMapper mapper;
        public IdentityService( IMapper _mapper)
        {
            mapper = _mapper;
        }
       
        public string GenerateJwtToken(string userId,string userName, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Name, userName)
                }),

                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
           return  tokenHandler.WriteToken(token);

        }
    }
}
