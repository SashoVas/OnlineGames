using Microsoft.EntityFrameworkCore;
using OnlineGames.Data;
using OnlineGames.Data.Models;
using OnlineGames.Services.Contracts;
using OnlineGames.Services.Models.Message;

namespace OnlineGames.Services
{
    public class MessageService : IMessageService
    {
        private readonly IRepository<Message> repo;
        public MessageService(IRepository<Message> messageRepository) 
            => this.repo = messageRepository;

        public async Task<IEnumerable<MessageServiceModel>> GetMessages(string userId, string friendId,int page)
            => await repo.GetAll()
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
            await repo.AddAsync(message);
            await repo.SaveChangesAsync();
            return new MessageServiceModel
            {
                Contents=contents,
                PostedOn=message.PostedOn.ToString("dd/MM/yyyy")
            };
        }
    }
}
