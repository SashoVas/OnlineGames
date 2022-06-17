using System.ComponentModel.DataAnnotations;

namespace OnlineGames.Web.Models.Chat
{
    public class ReadMessageInputModel
    {
        [Required]
        public string MessageId { get; set; }
    }
}
