using System.ComponentModel.DataAnnotations;

namespace OnlineGames.Web.Models.Friend
{
    public class FriendIdInputModel
    {
        [Required]
        public string Id { get; set; }
    }
}
