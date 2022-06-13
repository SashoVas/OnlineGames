﻿using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult> AddFriend(SendFriendRequestInputModel input)
        {
            if (!ModelState.IsValid ||await friendService.FriendExist(GetUserId(),input.FriendUserName))
            {
                return BadRequest();
            }
            try
            {
                var id = await friendService.SendFriendRequest(GetUserId(), input.FriendUserName);
                return Created(nameof(this.Created),id);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public async Task<ActionResult> AcceptFriendRequest(FriendIdInputModel input)
        {
            if (!ModelState.IsValid || !await friendService.AcceptFriendRequest(GetUserId(), input.Id))
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FriendServiceModel>>> GetFriends() 
            =>Ok( await friendService.GetFriends(GetUserId()));
        [HttpDelete("{id}")]
        public async Task<ActionResult> UnFriend(string id)
        {
            if ( !await friendService.DeleteFriend(GetUserId(),id))
            {
                throw new ArgumentException();
            }
            return Ok();
        }
    }
}
