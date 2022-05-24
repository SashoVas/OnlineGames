using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGames.Data.Models
{
    public class Message
    {
        public string Id { get; set; }
        public string? SenderId { get; set; }
        public User? Sender { get; set; }
        public DateTime PostedOn { get; set; } = DateTime.Now;
        [Required()]
        [MaxLength(300)]
        public string Contents { get; set; }
        public string? FriendChatId { get; set; }
        public Friend? FriendChat { get; set; }
        public string? RoomChatId { get; set; }
        public Room? RoomChat { get; set; }
    }
}
