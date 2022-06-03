using Microsoft.EntityFrameworkCore;
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
            => this.dbContext = dbContext;

        public async Task<IEnumerable<MessageServiceModel>> GetMessages(string userId, string friendId,int page)
            => await dbContext.Messages
                .Where(m => (m.FriendChat.User1Id == userId && m.FriendChat.User2Id==friendId)
                ||(m.FriendChat.User2Id == userId && m.FriendChat.User1Id == friendId))
                .OrderByDescending(m=>m.PostedOn)
                .Skip(page*20)
                .Take(20)
                .Select(m => new MessageServiceModel
                {
                    Contents = m.Contents,
                    PostedOn = m.PostedOn.ToString("dd/MM,yyyy"),
                    UserName=m.Sender.UserName
                }).ToListAsync();

        public async Task<MessageServiceModel> SendMessageToChat(string userId, string roomId, string contents,bool isName)
        {
            var message = new Message
            {
                Id = Guid.NewGuid().ToString(),
                Contents = contents,
                SenderId=userId,
                RoomChatId=!isName?roomId:null,
                FriendChatId=isName?roomId:null
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
