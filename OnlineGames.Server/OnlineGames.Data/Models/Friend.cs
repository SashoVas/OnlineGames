using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGames.Data.Models
{
    public class Friend
    {
        public Friend()
        {
            Messages = new HashSet<Message>();
        }
        public string Id { get; set; }
        public string User1Id { get; set; }
        public User User1 { get; set; }
        public string User2Id { get; set; }
        public User User2 { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
