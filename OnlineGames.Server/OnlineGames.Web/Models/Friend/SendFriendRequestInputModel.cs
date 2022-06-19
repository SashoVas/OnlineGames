using System.ComponentModel.DataAnnotations;

namespace OnlineGames.Web.Models.Friend
{
    public class SendFriendRequestInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string FriendUserName { get; set; }
    }
}
