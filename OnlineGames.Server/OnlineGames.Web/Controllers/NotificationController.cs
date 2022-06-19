using Microsoft.AspNetCore.Mvc;
using OnlineGames.Services.Contracts;
using OnlineGames.Web.Models.Notificatons;

namespace OnlineGames.Web.Controllers
{
    public class NotificationController : ApiController
    {
        private readonly IFriendService friendService;
        private readonly IMessageService messageService;
        public NotificationController(IFriendService friendService, IMessageService messageService)
        {
            this.friendService = friendService;
            this.messageService = messageService;
        }
        [HttpGet]
        public async Task<ActionResult<NotificationsModel>> GetNotifications() 
            => Ok(new NotificationsModel
            {
                FriendRequests = await friendService.GetRequests(GetUserId()),
                Messages = await messageService.GetMessagesUnread(GetUserId())
            });
    }
}
