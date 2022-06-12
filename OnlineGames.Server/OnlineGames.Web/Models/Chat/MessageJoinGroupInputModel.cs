using System.ComponentModel.DataAnnotations;

namespace OnlineGames.Web.Models.Chat
{
    public class MessageJoinGroupInputModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public bool IsName { get; set; }
    }
}
