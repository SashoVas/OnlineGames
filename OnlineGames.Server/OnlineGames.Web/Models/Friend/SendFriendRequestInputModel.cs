using System.ComponentModel.DataAnnotations;

namespace OnlineGames.Web.Models.Friend
{
    public class SendFriendRequestInputModel
    {
        [Required]
        public string FriendUserName { get; set; }
    }
}
