using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineGames.Services.Contracts;

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
            return new  { 
             RoomId=await this.roomService.CreateTicTacToeRoom(this.User.Identity.Name)
            };
        }
    }
}
