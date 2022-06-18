using OnlineGames.Services.Models.Message;

namespace OnlineGames.Services.Models.Friend
{
    public class NotificationsServiceModel
    {
        public IEnumerable<FriendServiceModel> FrendRequestName { get; set; }
        public IEnumerable<MessageServiceModel> Messages { get; set; }
    }
}
