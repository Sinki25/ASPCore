using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using ApiCore.BO;
using ApiCore.DB;
using Microsoft.IdentityModel.Tokens;

namespace ApiCore.JwtSecurity
{
    public class JwtSecurity
    {
        public string Login(DTO.User user)
        {
            User person;
            using (var db = new PeerDb())
            {
                person = db.Users.FirstOrDefault(x => x.Name == user.Name && x.Password == user.Password);
            }

            if (person != null)
            {
                return GenerateToken(person);
            }

            return null;
        }

        public string Logout(User identity)
        {

            return null;
        }

        private string GenerateToken(User user)
        {
            var handler = new JwtSecurityTokenHandler();

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.Name));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

            ClaimsIdentity identity = new ClaimsIdentity(claims);

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = JwtTokenDefinitions.Issuer,
                Audience = JwtTokenDefinitions.Audience,
                SigningCredentials = JwtTokenDefinitions.SigningCredentials,
                Subject = identity,
                Expires = DateTime.Now.Add(JwtTokenDefinitions.TokenExpirationTime),
                NotBefore = DateTime.Now
            });


            return handler.WriteToken(securityToken);
        }
    }
}
