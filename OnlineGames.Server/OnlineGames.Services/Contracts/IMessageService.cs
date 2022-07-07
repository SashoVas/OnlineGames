using OnlineGames.Services.Models.Message;

namespace OnlineGames.Services.Contracts
{
    public interface IMessageService
    {
        Task<MessageServiceModel> SendMessageToChat(string userId, string roomId, string contents,bool isName);
        Task<IEnumerable<MessageServiceModel>> GetMessages(string userId, string friendId,int page);
        Task<bool> ReadMessage(string userId, int messageId);
        Task<IEnumerable<MessageServiceModel>> GetMessagesUnread(string userId);

    }
}
