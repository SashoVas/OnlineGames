using Microsoft.EntityFrameworkCore;
using Moq;
using OnlineGames.Data;
using OnlineGames.Data.Models;
using OnlineGames.Services;
using OnlineGames.Services.Contracts;
using OnlineGames.Tests.Infrasturcture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                new Message { Id = 2, Contents = "Read message", PostedOn = DateTime.Now.AddMinutes(-1), Seen = true, SenderId = "user1", FriendChat = new Friend { Id="id2", User1Id = "user1", User2Id = "user2" } }
            };
            return messages;
        }
        private async Task SeedData(OnlineGamesDbContext context)
        {
            context.Messages.AddRange(GetDummyData());
            await context.SaveChangesAsync();

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
    }
}