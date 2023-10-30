using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApi.DLayer.Entity;
using WebApi.DLayer.Interfaces;


namespace WebApi.DLayer.Classes
{
    public class Tokengeeration : ITokenManager
    {
        private readonly string _key;
        private readonly byte[] _secret;
        private readonly JwtTokenConfig _jwtTokenConfig;

        private readonly IdentityDBContext _context;

        public Tokengeeration(JwtTokenConfig jwtTokenConfig, IdentityDBContext context)
        {
            _key = jwtTokenConfig.Secret;
            _jwtTokenConfig = jwtTokenConfig;
            _secret = Encoding.ASCII.GetBytes(jwtTokenConfig.Secret);
            _context = context;
        }

        public Task<string> GenerateTokenAsync(AspNetUser user)         {
           
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenKey = Encoding.ASCII.GetBytes(_key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.UniqueName,user.Email),
                    new Claim(JwtRegisteredClaimNames.Actort,user.PhoneNumber),
                    new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Task.FromResult(tokenHandler.WriteToken(token));
        }
    }
}
