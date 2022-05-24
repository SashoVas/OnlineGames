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
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();

        }
        public async Task SendMessage(string roomId,string contents)
        {
            await this.messageService.SendMessageToRoomChat(this.Context.User.FindFirstValue(ClaimTypes.NameIdentifier),roomId,contents);
        }
    }
}
