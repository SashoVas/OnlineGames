using System.ComponentModel.DataAnnotations;

namespace OnlineGames.Web.Models.Chat
{
    public class ReadMessageInputModel
    {
        [Required]
        public int MessageId { get; set; }
    }
}
