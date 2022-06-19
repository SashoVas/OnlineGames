using System.ComponentModel.DataAnnotations;

namespace OnlineGames.Web.Models.Chat
{
    public class SendMessageInputModel: MessageJoinGroupInputModel
    {
        [Required]
        [MaxLength(300)]
        public string Contents { get; set; }
    }
}
