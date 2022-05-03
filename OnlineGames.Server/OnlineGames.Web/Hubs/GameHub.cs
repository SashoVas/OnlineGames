using Microsoft.AspNetCore.SignalR;
using OnlineGames.Services.Contracts;
using OnlineGames.Services.Models;
using System.Security.Claims;

namespace OnlineGames.Web.Hubs
{
    public class GameHub:Hub
    {
        protected readonly IRoomService roomService;
        public GameHub(IRoomService roomService)
        {
            this.roomService = roomService;
        }
        
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await this.ClearBoard();
            await this.roomService.RemoveRoom(this.Context.User.FindFirstValue(ClaimTypes.NameIdentifier));
            await base.OnDisconnectedAsync(exception);
        }
        public async Task ClearBoard()
        {
            var userId = this.Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await this.roomService.ClearBoard(userId);
            await this.Clients.Group(await this.roomService.GetRoomId(userId)).SendAsync("ClearBoard");
        }
    }
}
