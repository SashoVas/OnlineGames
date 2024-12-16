using OnlineGames.Data;
using OnlineGames.Data.Models;
using OnlineGames.Services;
using OnlineGames.Tests.Infrasturcture;
using Xunit;

namespace OnlineGames.Tests.Services
{
    public class MessageServiceTests
    {
        private List<Message> GetDummyData()
        {
            var messages = new List<Message>
            {
                new Message { Id = 1, Contents = "Unread message", PostedOn = DateTime.Now, Seen = false, SenderId = "user2", FriendChat = new Friend { Id="id1", User1Id = "user1", User2Id = "user2" } },
                new Message { Id = 2, Contents = "Read message", PostedOn = DateTime.Now.AddMinutes(-1), Seen = true, SenderId = "user1", FriendChat = new Friend { Id="id2", User1Id = "user1", User2Id = "user2" } },
                new Message { Id = 3, Contents = "Unread message", PostedOn = DateTime.Now, Seen = false, SenderId = "user3", FriendChat = new Friend { Id="id3", User1Id = "user3", User2Id = "user2" } },

            };
            return messages;
        }
        private async Task<List<Message>> SeedData(OnlineGamesDbContext context)
        {
            var data = GetDummyData();
            context.Messages.AddRange(data);
            await context.SaveChangesAsync();
            return data;

        }

        [Fact]
        public async Task GetMessages_ReturnsCorrectMessages()
        {
            // Arrange
            var context = OnlineGamesDbContextFactory.InitializeContext();
            await SeedData(context);
            var repo = new Repository<Message>(context);
            var messageService = new MessageService(repo);

            // Act
            var result = await messageService.GetMessages("user1", "user2", 0);

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal("Unread message", result.First().Contents);
        }

        [Fact]
        public async Task GetMessagesUnread_ReturnsUnreadMessages()
        {
            // Arrange
            var context = OnlineGamesDbContextFactory.InitializeContext();
            await SeedData(context);
            var repo = new Repository<Message>(context);
            var messageService = new MessageService(repo);

            // Act
            var result = await messageService.GetMessagesUnread("user1");
            var result2 = await messageService.GetMessagesUnread("user3");

            // Assert
            Assert.Single(result);
            Assert.Equal("Unread message", result.First().Contents);
            Assert.Empty(result2);
        }

        [Fact]
        public async Task ReadMessage_MarksMessageAsSeen()
        {
            // Arrange
            var context = OnlineGamesDbContextFactory.InitializeContext();
            var data = await SeedData(context);
            var repo = new Repository<Message>(context);
            var messageService = new MessageService(repo);

            // Act
            var result = await messageService.ReadMessage("user1", 1);

            // Assert
            Assert.True(result);
            Assert.True(data[0].Seen);

        }
        [Fact]
        public async Task ReadMessage_ReturnsFalseIfMessageNotFound()
        {
            // Arrange
            var context = OnlineGamesDbContextFactory.InitializeContext();
            await SeedData(context);
            var repo = new Repository<Message>(context);
            var messageService = new MessageService(repo);

            // Act
            var result = await messageService.ReadMessage("user2", 1);

            // Assert
            Assert.False(result);
        }
        [Fact]
        public async Task SendMessageToChat_AddsMessageAndReturnsServiceModel()
        {
            // Arrange
            var context = OnlineGamesDbContextFactory.InitializeContext();
            await SeedData(context);
            var repo = new Repository<Message>(context);
            var messageService = new MessageService(repo);
            var messageContents = "Hello World!";
            var message = new Message
            {
                Id = 1,
                Contents = messageContents,
                PostedOn = DateTime.Now,
                SenderId = "user1",
                FriendChatId = "friend1"
            };
            // Act
            var result = await messageService.SendMessageToChat("user1", "friend1", messageContents, true);
            var contents = repo.GetAll();
            // Assert
            Assert.NotNull(result);
            Assert.Equal(messageContents, result.Contents);
            Assert.Equal(4, contents.Count());
        }
    }
}