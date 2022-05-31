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
        Task<MessageServiceModel> SendMessageToFriendChat(string userId,string friendUserName,string contents);
        Task<MessageServiceModel> SendMessageToRoomChat(string userId, string roomId, string contents,bool isName);

    }
}
