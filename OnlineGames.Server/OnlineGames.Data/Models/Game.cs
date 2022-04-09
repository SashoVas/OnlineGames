using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGames.Data.Models
{
    public class Game
    {
        public Game()
        {
            this.Players = new HashSet<User>();
        }
        public int Id { get; set; }
        public string Board { get; set; }
        public ICollection<User> Players { get; set; }
    }
}
