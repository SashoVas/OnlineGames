using Microsoft.AspNetCore.SignalR;
using OnlineGames.Services.Contracts;
using System.Security.Claims;

namespace OnlineGames.Web.Hubs
{
    public abstract class GameHub:Hub
    {
        protected readonly IRoomService roomService;
        public GameHub(IRoomService roomService) 
            => this.roomService = roomService;

        protected string GetUserId() 
            => this.Context.User.FindFirstValue(ClaimTypes.NameIdentifier);

        protected string GetUserName() 
            => this.Context.User.Identity.Name;

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await this.ClearBoard();
            await this.roomService.RemoveRoom(GetUserId());
            await base.OnDisconnectedAsync(exception);
        }
        public async Task ClearBoard()
        {
            await this.roomService.ClearBoard(GetUserId(),GetUserName());
            await this.Clients.Group(await this.roomService.GetRoomId(GetUserId())).SendAsync("ClearBoard");
        }
        public async Task AddToGroup(string groupName)
        {
            if (groupName == null )
            {
                //Here if the oponent is ai and we dont want our room id to be exposed
                switch (this.GetType().Name)
                {
                    case ("TicTacToeHub"):
                        groupName = await this.roomService.CreateRoom(GetUserName(), true, "TicTacToe",3*3);
                        break;
                    case ("Connect4Hub"):
                        groupName = await this.roomService.CreateRoom(GetUserName(), true, "Connect4", 6*7);
                    break;
                    default:
                        throw new ArgumentException();
                }
                await this.roomService.SetRoomToUser(GetUserId(), groupName);
            }
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, groupName);
        }
    }
}
