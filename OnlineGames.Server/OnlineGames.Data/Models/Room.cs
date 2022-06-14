using System.ComponentModel.DataAnnotations;

namespace OnlineGames.Data.Models
{
    public class Room
    {
        public string Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string BoardString { get; set; } = "000000000";
        [Required]
        public bool FirstPlayerTurn { get; set; } = true;
        [MaxLength(100)]
        public string? FirstPlayerName { get; set; }
        [Required]
        public bool Private { get; set; } = false;
        [Required]
        [MaxLength(30)]
        public string GameName { get; set; }
        public ICollection<Message> Messages { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public string? Player1Id { get; set; }
        public User? Player1 { get; set; }
        public string? Player2Id { get; set; }
        public User? Player2 { get; set; }

    }
}