using Microsoft.AspNetCore.SignalR;
using OnlineGames.Services.Contracts;
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
        public async Task AddToGroup(string groupName)
        {
            var userId = this.Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (groupName == null)
            {
                //Here if the oponent is ai and we dont want our room id to be exposed
                groupName = await this.roomService.CreateTicTacToeRoom(this.Context.User.Identity.Name);
                await this.roomService.SetRoomToUser(userId, groupName);
            }
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, groupName);
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
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
