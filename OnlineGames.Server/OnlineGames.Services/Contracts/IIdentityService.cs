using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGames.Services.Contracts
{
    public interface IIdentityService
    {
        Task<string> Login(string userName, string password, string secret);
        Task<string> Register(string username, string password, string confirmPassword);
    }
}
