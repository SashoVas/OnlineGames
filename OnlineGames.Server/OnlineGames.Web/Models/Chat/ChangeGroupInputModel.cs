using System.ComponentModel.DataAnnotations;

namespace OnlineGames.Web.Models.Chat
{
    public class ChangeGroupInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string FriendName { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string OldFriendName { get; set; }
    }
}
