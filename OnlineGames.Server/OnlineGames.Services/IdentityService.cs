using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineGames.Data.Models;
using OnlineGames.Services.Contracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGames.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> userManager;
        public IdentityService(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }
        private string GetJwt(User user,string Secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public async Task<string> Login(string userName,string password,string secret)
        {
            var user = await userManager.FindByNameAsync(userName);
            
            var isValidPassword = await userManager.CheckPasswordAsync(user,password);
            if (!isValidPassword)
            {
                throw new UnauthorizedAccessException();
            }
            return GetJwt(user,secret);
            
        }

        public async Task<string> Register(string username,string password,string confirmPassword)
        {
            var user = new User
            {
                UserName = username
            };
            var result = await this.userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                throw new ArgumentException();
            }
            return user.Id;
        }
    }
}
