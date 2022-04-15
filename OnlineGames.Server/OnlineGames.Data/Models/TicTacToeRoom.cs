using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGames.Data.Models
{
    public class TicTacToeRoom
    {
        public TicTacToeRoom()
        {
            Users = new HashSet<User>();
        }
        public string Id { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
