using System.ComponentModel.DataAnnotations;

namespace OnlineGames.Data.Models
{
    public class Message
    {
        public string Id { get; set; }
        public string? SenderId { get; set; }
        public User? Sender { get; set; }
        public DateTime PostedOn { get; set; } = DateTime.UtcNow;
        [Required()]
        [MaxLength(300)]
        public string Contents { get; set; }
        public string? FriendChatId { get; set; }
        public Friend? FriendChat { get; set; }
        public string? RoomChatId { get; set; }
        public Room? RoomChat { get; set; }
    }
}
