using OnlineGames.Data;
using OnlineGames.Data.Models;
using OnlineGames.Services.Contracts;
using OnlineGames.Services.Models.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGames.Services
{
    public class MessageService : IMessageService
    {
        private readonly OnlineGamesDbContext dbContext;
        public MessageService(OnlineGamesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Task<MessageServiceModel> SendMessageToFriendChat(string userId, string friendUserName, string contents)
        {
            throw new NotImplementedException();
        }

        public async Task<MessageServiceModel> SendMessageToRoomChat(string userId, string roomId, string contents)
        {
            var message = new Message
            {
                Id = Guid.NewGuid().ToString(),
                Contents = contents,
                SenderId=userId,
                RoomChatId=roomId
            };
            await dbContext.Messages.AddAsync(message);
            await dbContext.SaveChangesAsync();
            return new MessageServiceModel
            {
                Contents=contents,
                PostedOn=message.PostedOn.ToString("dd/MM/yyyy")
            };
        }
    }
}
