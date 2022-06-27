using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OnlineGames.Services.Contracts;
using OnlineGames.Services.Models.User;
using OnlineGames.Web.Infrastructure;
using OnlineGames.Web.Models.User;

namespace OnlineGames.Web.Controllers
{
    public class UserController : ApiController
    {
        private IUserService userService;
        private readonly AppSettings appSettings;

        public UserController(IUserService userService, IOptions<AppSettings> appSettings)
        {
            this.userService = userService;
            this.appSettings = appSettings.Value;
        }

        [HttpGet("{name?}")]
        public async Task<ActionResult<UserServiceModel>> GetUser(string? name)
        {
            var user= await userService.GetUser(name??this.User.Identity.Name);
            if (user==null)
            {
                return NotFound("No such user");
            }
            user.IsMe = (name == null)||name==(User.Identity.Name);
            return Ok(user);
        }
        [HttpPut]
        public async Task<ActionResult<UpdateUserServiceModel>> Update(UpdateUserInputModel input) 
            => Ok(await this.userService.UpdateUser(GetUserId(), input.Description, input.ImgUrl, input.UserName, appSettings.Secret));
        [HttpGet("GetUserCard")]
        public async Task<ActionResult<UserCardServiceModel>> GetUserCard() 
            => Ok(await userService.GetUserCard(GetUserId()));
    }
}
