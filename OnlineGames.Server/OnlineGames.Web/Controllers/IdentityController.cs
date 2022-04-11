using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineGames.Data.Models;
using OnlineGames.Web.Infrastructure;
using OnlineGames.Web.Models.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineGames.Web.Controllers
{
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private UserManager<User> userManager;
        private AppSettings appSettings;

        public IdentityController(UserManager<User> userManager, IOptions<AppSettings> appSettings)
        {
            this.userManager = userManager;
            this.appSettings = appSettings.Value;
        }
        private string GetJwt(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginReturnModel>> Login(LoginInputModel input)
        {
            var user = await userManager.FindByNameAsync(input.UserName);
            if (user == null)
            {
                return Unauthorized();
            }
            var isValidPassword = await userManager.CheckPasswordAsync(user, input.Password);

            return new LoginReturnModel
            {
                Token= GetJwt(user) 
            };
        }
        [HttpPost("Register")]
        public async Task<ActionResult<object>> Register([FromBody] RegisterInputModel input)
        {
            var user = new User
            {
                UserName = input.UserName
            };
            var result = await this.userManager.CreateAsync(user, input.Password);
            if (!result.Succeeded)
            {
                return this.BadRequest(result.Errors);
            }
            return new 
            { 
                Id=user.Id 
            };
        }
    }
}
