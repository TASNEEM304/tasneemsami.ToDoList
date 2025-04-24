using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TasneemSami.ToDoList.Database.Data;
using TasneemSami.ToDoList.Database.Models;
using TasneemSami.ToDoList.Services.UserServices;

namespace TasneemSami.ToDoList.Services
{
    public class JwtService
    {
        private readonly string _key;

        public JwtService(IConfiguration config)
        {
            _key = config["Jwt:Key"];
        }

        public string GenerateToken(UserGetOutPutDto user)
        {
            var claims = new[]
            {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.Role, user.ROLE.ToString()),
        new Claim("Id", user.ID.ToString()) // 👈 أضف هذا
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
          //"DefaultConnection": "Server=db;Database=ToDoDb;User=sa;Password=YourStrong!Passw0rd;"
    }
}
