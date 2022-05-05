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
        [HttpPost]
        public async Task<ActionResult<object>> CreateRoom(CreateRoomInputModel input)
        {
            string roomId;
            switch (input.Game)
            {
                case ("TicTacToe"):
                    roomId=await this.roomService.CreateTicTacToeRoom(this.User.Identity.Name, false);
                    break;
                case ("Connect4"):
                    roomId= await this.roomService.CreateConnect4Room(this.User.Identity.Name, false);
                    break;
                default:
                    return this.BadRequest();
            }
            await this.roomService.SetRoomToUser(User.FindFirstValue(ClaimTypes.NameIdentifier), roomId);
            return new
            {
                RoomId = roomId
            };
        }

        [HttpPut]
        public async Task<ActionResult<object>>AddToRoom([FromBody] AddToRoomInputModel input)
        {
            try
            {
                await this.roomService.SetRoomToUser(User.FindFirstValue(ClaimTypes.NameIdentifier), input.RoomId);
                return new {RoomId=input.RoomId };
            }
            catch (Exception)
            {
                return this.BadRequest();
            }
            
        }
        [HttpGet]
        public async Task<IEnumerable<RoomsServiceModel>>GetRooms([FromQuery]GetRoomsInputModel input)
        {
            return await this.roomService.GetAvailableRooms(input.Game=="null"?null: input.Game,input.Count,input.Page);
        }
    }
}