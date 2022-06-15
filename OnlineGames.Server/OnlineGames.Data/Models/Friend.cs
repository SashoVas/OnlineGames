using System.ComponentModel.DataAnnotations;

namespace OnlineGames.Data.Models
{
    public class Friend
    {
        public Friend()
        {
            Messages = new HashSet<Message>();
        }
        public string Id { get; set; }
        [Required]
        public string User1Id { get; set; }
        [Required]
        public User User1 { get; set; }
        [Required]
        public string User2Id { get; set; }
        [Required]
        public User User2 { get; set; }
        [Required]
        public bool Accepted { get; set; }
        [Required]
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public ICollection<Message> Messages { get; set; }
    }
}
