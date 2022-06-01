using OnlineGames.Services.Models.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGames.Services.Contracts
{
    public interface IMessageService
    {
        Task<MessageServiceModel> SendMessageToChat(string userId, string roomId, string contents,bool isName);
        Task<IEnumerable<MessageServiceModel>> GetMessages(string userId, string friendName,int page);
    }
}
