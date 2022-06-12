using Microsoft.AspNetCore.Mvc;
using OnlineGames.Services.Contracts;
using OnlineGames.Services.Models.Friend;
using OnlineGames.Web.Models.Friend;

namespace OnlineGames.Web.Controllers
{
    public class FriendController : ApiController
    {
        private readonly IFriendService friendService;
        public FriendController(IFriendService friendService)
        {
            this.friendService = friendService;
        }
        [HttpPost]
        public async Task<object> AddFriend(SendFriendRequestInputModel input)
        {
            if (!ModelState.IsValid ||await friendService.FriendExist(GetUserId(),input.FriendUserName))
            {
                return BadRequest();
            }
            if (!await friendService.SendFriendRequest(GetUserId(), input.FriendUserName))
            {
                return BadRequest();
            }
            return new { friendUserName = input.FriendUserName };
        }
        [HttpPut]
        public async Task<object> AcceptFriendRequest(FriendIdInputModel input)
        {
            if (!ModelState.IsValid || !await friendService.AcceptFriendRequest(GetUserId(), input.Id))
            {
                return BadRequest();
            }
            return new { Id = input.Id };
        }
        [HttpGet]
        public async Task<IEnumerable<FriendServiceModel>> GetFriends() 
            => await friendService.GetFriends(GetUserId());
        [HttpDelete("{id}")]
        public async Task UnFriend(string id)
        {
            if ( !await friendService.DeleteFriend(GetUserId(),id))
            {
                throw new ArgumentException();
            }
        }
    }
}
