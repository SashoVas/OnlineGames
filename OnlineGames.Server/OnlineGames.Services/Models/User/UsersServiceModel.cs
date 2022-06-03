using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGames.Services.Models.User
{
    public class UsersServiceModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public bool Accepted { get; set; }
    }
}
