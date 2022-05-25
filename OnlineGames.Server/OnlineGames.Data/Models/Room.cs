using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGames.Data.Models
{
    public class Room
    {
        public Room()
        {
            Users = new List<User>();
        }
        public string Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string BoardString { get; set; } = "000000000";
        [Required]
        public bool FirstPlayerTurn { get; set; } = true;
        [MaxLength(100)]
        public string? FirstPlayerName { get; set; }
        public ICollection<User> Users { get; set; }
        [Required]
        public bool Private { get; set; } = false;
        [Required]
        [MaxLength(30)]
        public string GameName { get; set; }
        public ICollection<Message> Messages { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;

    }
}