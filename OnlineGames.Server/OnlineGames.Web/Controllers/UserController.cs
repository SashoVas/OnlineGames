using Microsoft.AspNetCore.Mvc;
using OnlineGames.Services.Contracts;
using OnlineGames.Services.Models.User;
using OnlineGames.Web.Models.User;

namespace OnlineGames.Web.Controllers
{
    public class UserController : ApiController
    {
        private IUserService userService;
        public UserController(IUserService userService)
            => this.userService = userService;
        [HttpGet("{name?}")]
        public async Task<ActionResult<UserServiceModel>> GetUser(string? name)
        {
            var user= await userService.GetUser(name??this.User.Identity.Name);
            user.IsMe = (name == null)||name==(User.Identity.Name);
            if (user==null)
            {
                return BadRequest();
            }
            return Ok(user);
        }
        [HttpPut]
        public async Task<ActionResult<UserServiceModel>> Update(UpdateUserInputModel input )
        {
            if (!ModelState.IsValid )
            {
                return BadRequest();
            }
            var user = await this.userService.UpdateUser(GetUserId(), input.Description, input.ImgUrl, input.UserName);
            return Ok(user);
        }
        [HttpGet("GetUserCard")]
        public async Task<ActionResult<UserCardServiceModel>> GetUserCard() 
            => Ok(await userService.GetUserCard(GetUserId()));
    }
}
