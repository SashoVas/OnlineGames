using Microsoft.AspNetCore.Mvc;
using OnlineGames.Services.Contracts;
using OnlineGames.Services.Models.User;
using OnlineGames.Web.Models.User;

namespace OnlineGames.Web.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
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
