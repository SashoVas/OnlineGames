using Microsoft.AspNetCore.SignalR;
using OnlineGames.Services.Contracts;
using OnlineGames.Web.Models.Chat;
using System.Security.Claims;

namespace OnlineGames.Web.Hubs
{
    public class ChatHub:Hub
    {
        private readonly IMessageService messageService;
        private readonly IUserService userService;
        public ChatHub(IMessageService messageService, IUserService userService)
        {
            this.messageService = messageService;
            this.userService = userService;
        }
        public async Task JoinGroup(MessageJoinGroupInputModel input)
        {
            if (input.IsName)
            {
                //here if the chat is with friend
                var group =await userService.GetFriendId(this.Context.User.FindFirstValue(ClaimTypes.NameIdentifier), input.GroupName);
                if (group==null)
                {
                    return;
                }
                input.GroupName = group;
            }
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, input.GroupName);
        }
        public async Task ChangeGroup(MessageJoinGroupInputModel input)
        {
            await this.Groups.RemoveFromGroupAsync(this.Context.ConnectionId, input.GroupName);
            await this.JoinGroup(input);
        }
        public async Task SendMessage(SendMessageInputModel input)
        {
            if (input.IsName)
            {
                //here if the chat is with friend
                var group = await userService.GetFriendId(this.Context.User.FindFirstValue(ClaimTypes.NameIdentifier), input.GroupName);
                if (group == null)
                {
                    return;
                }
                input.GroupName = group;
            }
            var message=await this.messageService.SendMessageToRoomChat(this.Context.User.FindFirstValue(ClaimTypes.NameIdentifier), input.GroupName, input.Contents,input.IsName);
            message.UserName = this.Context.User.Identity.Name;
            await this.Clients.Group(input.GroupName).SendAsync("ReceiveMessage",message);
        }
    }
}
