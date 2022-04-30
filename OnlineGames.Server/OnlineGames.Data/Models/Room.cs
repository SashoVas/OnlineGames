using System;
using System.Collections.Generic;
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
        public string BoardString { get; set; } = "000000000";
        public bool FirstPlayerTurn { get; set; } = true;
        public string? FirstPlayerName { get; set; }
        public ICollection<User> Users { get; set; }
        public bool Private { get; set; } = false;

    }
}