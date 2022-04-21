using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGames.Services.Models
{
    public class RoomsServiceModel
    {
        public string UserName { get; set; }
        public string GameName { get; set; }
        public int Capacity { get; set; }
        public int Players { get; set; }
        public string RoomId { get; set; }
    }
}
