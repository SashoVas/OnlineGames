using Microsoft.AspNetCore.SignalR;
using OnlineGames.Services.Contracts;
using System.Security.Claims;

namespace OnlineGames.Web.Hubs
{
    public class ChatHub:Hub
    {
        private readonly IMessageService messageService;
        public ChatHub(IMessageService messageService)
        {
            this.messageService = messageService;
        }
        public async Task JoinGroup(string groupName)
        {
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, groupName);
        }
        public async Task LeaveGroup()
        {
            await this.Groups.RemoveFromGroupAsync(this.Context.ConnectionId, "groupName");
        }
        public async Task SendMessage(string roomId,string contents)
        {
            var message=await this.messageService.SendMessageToRoomChat(this.Context.User.FindFirstValue(ClaimTypes.NameIdentifier),roomId,contents);
            message.UserName = this.Context.User.Identity.Name;
            await this.Clients.Group(roomId).SendAsync("ReceiveMessage",message);
        }
    }
}
