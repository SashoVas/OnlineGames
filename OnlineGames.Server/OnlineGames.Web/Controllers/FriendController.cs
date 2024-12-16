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
            => this.friendService = friendService;
        [HttpPost]
        public async Task<ActionResult<object>> AddFriend(SendFriendRequestInputModel input)
        {
            if (await friendService.FriendExist(GetUserId(), input.FriendUserName))
            {
                return NotFound("Already friend");
            }
            try
            {
                var id = await friendService.SendFriendRequest(GetUserId(), input.FriendUserName);
                return Created(nameof(AddFriend), new { Id = id });
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult> AcceptFriendRequest(FriendIdInputModel input)
        {
            if (!await friendService.AcceptFriendRequest(GetUserId(), input.Id))
            {
                return NotFound("There is no request");
            }
            return Ok();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FriendServiceModel>>> GetFriends()
            => Ok(await friendService.GetFriends(GetUserId()));
        [HttpDelete("{id}")]
        public async Task<ActionResult> UnFriend(string id)
        {
            if (!await friendService.DeleteFriend(GetUserId(), id))
            {
                return NotFound("No such friend");
            }
            return Ok();
        }
    }
}
