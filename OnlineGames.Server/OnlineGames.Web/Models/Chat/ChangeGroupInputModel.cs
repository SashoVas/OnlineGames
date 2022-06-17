using System.ComponentModel.DataAnnotations;

namespace OnlineGames.Web.Models.Chat
{
    public class ChangeGroupInputModel
    {
        [Required]
        public string FriendName { get; set; }
        [Required]
        public string OldFriendName { get; set; }
    }
}
