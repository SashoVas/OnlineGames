using Microsoft.AspNetCore.Mvc;
using OnlineGames.Services.Contracts;
using OnlineGames.Services.Models.Room;
using OnlineGames.Web.Models.Room;

namespace OnlineGames.Web.Controllers
{
    public class RoomController : ApiController
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
            int board;
            switch (input.Game)
            {
                case ("TicTacToe"):
                    board = 3 * 3;
                    break;
                case ("Connect4"):
                    board = 6 * 7;
                    break;
                default:
                    return this.BadRequest();
            }
            roomId = await this.roomService.CreateRoom(this.User.Identity.Name, false, input.Game, board);
            await this.roomService.SetRoomToUser(GetUserId(), roomId);
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
                await this.roomService.SetRoomToUser(GetUserId(), input.RoomId);
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