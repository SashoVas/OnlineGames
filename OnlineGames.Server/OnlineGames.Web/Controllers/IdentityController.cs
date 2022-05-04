using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineGames.Data.Models;
using OnlineGames.Services.Contracts;
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
        private IIdentityService identityService;
        private readonly AppSettings appSettings;

        public IdentityController(IIdentityService identityService, IOptions<AppSettings> appSettings)
        {
            this.identityService = identityService;
            this.appSettings = appSettings.Value;
        }
        
        [HttpPost("Login")]
        public async Task<ActionResult<LoginReturnModel>> Login(LoginInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }
            try
            {
                return new LoginReturnModel
                {
                    Token =await identityService.Login(input.UserName, input.Password, this.appSettings.Secret)
                };
            }
            catch (Exception)
            {
                return this.BadRequest("Invalid password");
            }
            
        }
        [HttpPost("Register")]
        public async Task<ActionResult<object>> Register([FromBody] RegisterInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }
            try
            {
                return new
                {
                    Id =  await this.identityService.Register(input.UserName, input.Password, input.ConfirmPassword)
                };
            }
            catch (Exception)
            {
                return this.BadRequest("Invalid data");
            }
        }
    }
}
