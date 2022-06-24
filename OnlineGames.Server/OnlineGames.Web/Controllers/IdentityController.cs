using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OnlineGames.Services.Contracts;
using OnlineGames.Web.Infrastructure;
using OnlineGames.Web.Models.Identity;

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
