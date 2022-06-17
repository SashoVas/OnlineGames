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
        private async Task ValidateInput(MessageJoinGroupInputModel input)
        {
            if (input.IsName)
            {
                //here if the chat is with friend
                var group = await friendService.GetFriendId(GetUserId(), input.Id);
                if (group == null)
                {
                    throw new ArgumentException();
                }
                input.Id = group;
            }
            else if (!await userService.IsUserInRoom(GetUserId(), input.Id))
            {
                throw new ArgumentException();
            }
        }

        private string GetUserId()
            => this.Context.User.FindFirstValue(ClaimTypes.NameIdentifier);

        public async Task JoinGroup(MessageJoinGroupInputModel input)
        {
            await ValidateInput(input);
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, input.Id);
        }

        public async Task ChangeGroup(ChangeGroupInputModel input)
        {
            var odlGroup = await friendService.GetFriendId(GetUserId(), input.OldFriendName);
            await this.Groups.RemoveFromGroupAsync(this.Context.ConnectionId, odlGroup);
            await this.JoinGroup(new MessageJoinGroupInputModel 
            { 
                Id=input.FriendName,
                IsName=true
            });
        }

        public async Task SendMessage(SendMessageInputModel input)
        {
            await ValidateInput(input);
            var message =await this.messageService.SendMessageToChat(GetUserId(), input.Id, input.Contents,input.IsName);
            message.UserName = this.Context.User.Identity.Name;
            await this.Clients.Group(input.Id).SendAsync("ReceiveMessage",message);
        }

        //Here when user see new message
        public async Task ReadMessage(ReadMessageInputModel input) 
            => await messageService.ReadMessage(GetUserId(), input.MessageId);
    }
}
