using Microsoft.AspNetCore.Mvc;
using OnlineGames.Services.Contracts;

namespace OnlineGames.Web.Controllers
{
    public class UserController : ApiController
    {
        private IUserService userService;
        public UserController(IUserService userService)
            => this.userService = userService;
        [HttpGet("{id?}")]
        public async Task<object> GetUser(string id)
        {
            var user= await userService.GetUser(id??GetUserId());
            if (user==null)
            {
                return BadRequest();
            }
            return user;
        }
    }
}
