using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineGames.Services.Contracts;
using OnlineGames.Services.Models;
using OnlineGames.Web.Models.Room;
using System.Security.Claims;

namespace OnlineGames.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService roomService;
        public RoomController(IRoomService roomService)
        {
            this.roomService = roomService;
        }
        [HttpPost("CreateTicTacToeRoom")]
        public async Task<ActionResult<object>>CreateTicTacToeRoom()
        {
            var roomId = await this.roomService.CreateTicTacToeRoom(this.User.Identity.Name);
            await this.roomService.SetRoomToUser(User.FindFirstValue(ClaimTypes.NameIdentifier),roomId);
            return new  { 
             RoomId= roomId
            };
        }

        [HttpPost("CreateConnect4Room")]
        public async Task<ActionResult<object>> CreateConnect4Room()
        {
            var roomId = await this.roomService.CreateConnect4Room(this.User.Identity.Name);
            await this.roomService.SetRoomToUser(User.FindFirstValue(ClaimTypes.NameIdentifier), roomId);
            return new
            {
                RoomId = roomId
            };
        }
        [HttpPost("AddToRoom")]
        public async Task<ActionResult<object>>AddToRoom([FromBody] AddToRoomInputModel input)
        {
            await this.roomService.SetRoomToUser(User.FindFirstValue(ClaimTypes.NameIdentifier), input.RoomId);
            return new {RoomId=input.RoomId };
        }
        [HttpGet("GetRooms")]
        public async Task<IEnumerable<RoomsServiceModel>>GetRooms()
        {
            return await this.roomService.GetAvailableRooms();
        }
    }
}