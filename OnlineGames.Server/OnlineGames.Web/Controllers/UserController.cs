using Microsoft.AspNetCore.Mvc;
using OnlineGames.Services.Contracts;
using OnlineGames.Web.Models.User;

namespace OnlineGames.Web.Controllers
{
    public class UserController : ApiController
    {
        private IUserService userService;
        public UserController(IUserService userService)
            => this.userService = userService;
        [HttpGet("{name?}")]
        public async Task<object> GetUser(string? name)
        {
            var user= await userService.GetUser(name??this.User.Identity.Name);
            user.IsMe = name == null;
            if (user==null)
            {
                return BadRequest();
            }
            return user;
        }
        [HttpPut]
        public async Task<object> Update(UpdateUserInputModel input )
        {
            if (!ModelState.IsValid|| !await this.userService.UpdateUser(GetUserId(), input.Description, input.ImgUrl, input.UserName))
            {
                return BadRequest();
            }
            return input;
        }
    }
}
