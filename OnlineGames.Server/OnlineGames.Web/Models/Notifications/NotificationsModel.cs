using OnlineGames.Services.Models.Friend;
using OnlineGames.Services.Models.Message;

namespace OnlineGames.Web.Models.Notificatons
{
    public class NotificationsModel
    {
        public IEnumerable<FriendServiceModel> FriendRequests { get; set; }
        public IEnumerable<MessageServiceModel> Messages { get; set; }
    }
}
