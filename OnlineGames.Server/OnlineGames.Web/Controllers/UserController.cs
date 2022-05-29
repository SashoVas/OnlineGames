using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineGames.Services.Contracts;
using OnlineGames.Services.Models.User;
using OnlineGames.Web.Models.User;
using System.Security.Claims;

namespace OnlineGames.Web.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        private string GetUserId()
            => this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        [HttpPost]
        public async Task<object> AddFriend(SendFriendRequestInputModel input)
        {
            if (await userService.FriendExist(GetUserId(),input.FriendUserName))
            {
                return BadRequest();
            }
            if (!await userService.SendFriendRequest(GetUserId(), input.FriendUserName))
            {
                return BadRequest();
            }
            return new { friendUserName = input.FriendUserName };
        }
        [HttpPut]
        public async Task<object> AcceptFriendRequest(SendFriendRequestInputModel input)
        {
            if (!await userService.AcceptFriendRequest(GetUserId(), input.FriendUserName))
            {
                return BadRequest();
            }
            return new { friendUserName = input.FriendUserName };
        }
        [HttpGet]
        public async Task<IEnumerable<UsersServiceModel>> GetFriends() 
            => await userService.GetFriends(GetUserId());
    }
}
