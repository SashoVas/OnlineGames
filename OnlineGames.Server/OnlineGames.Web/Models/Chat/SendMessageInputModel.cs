using System.ComponentModel.DataAnnotations;

namespace OnlineGames.Web.Models.Chat
{
    public class SendMessageInputModel: MessageJoinGroupInputModel
    {
        [Required]
        public string Contents { get; set; }
    }
}
