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
        private readonly IFriendService friendService;
        public ChatHub(IMessageService messageService, IUserService userService, IFriendService friendService)
        {
            this.messageService = messageService;
            this.userService = userService;
            this.friendService = friendService;
        }
        public async Task JoinGroup(MessageJoinGroupInputModel input)
        {
            await ValidateInput(input);
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, input.Id);
        }
        public async Task ChangeGroup(MessageJoinGroupInputModel input)
        {
            await this.Groups.RemoveFromGroupAsync(this.Context.ConnectionId, input.Id);
            await this.JoinGroup(input);
        }
        public async Task SendMessage(SendMessageInputModel input)
        {
            await ValidateInput(input);
            var message =await this.messageService.SendMessageToChat(this.Context.User.FindFirstValue(ClaimTypes.NameIdentifier), input.Id, input.Contents,input.IsName);
            message.UserName = this.Context.User.Identity.Name;
            await this.Clients.Group(input.Id).SendAsync("ReceiveMessage",message);
        }
        private async Task ValidateInput(MessageJoinGroupInputModel input)
        {
            if (input.IsName)
            {
                //here if the chat is with friend
                var group = await friendService.GetFriendId(this.Context.User.FindFirstValue(ClaimTypes.NameIdentifier), input.Id);
                if (group == null)
                {
                    throw new ArgumentException();
                }
                input.Id = group;
            }
            else if (!await userService.IsUserInRoom(this.Context.User.FindFirstValue(ClaimTypes.NameIdentifier), input.Id))
            {
                throw new ArgumentException();
            }
        }
    }
}
