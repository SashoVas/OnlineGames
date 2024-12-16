using OnlineGames.Data;
using OnlineGames.Data.Models;
using OnlineGames.Services;
using OnlineGames.Tests.Infrasturcture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using OnlineGames.Services.Contracts;

namespace OnlineGames.Tests.Services
{
    public class UserServiceTests
    {
        private List<User> GetDummyData()
        {
            var messages = new List<User>
            {
                new User{ Id="user1",UserName="user1" },
                new User{ Id="user2",UserName="user2" },
                new User{ Id="user3",UserName="user3" },


            };
            return messages;
        }
        private async Task<List<User>> SeedData(OnlineGamesDbContext context)
        {
            var data = GetDummyData();
            context.Users.AddRange(data);
            await context.SaveChangesAsync();
            return data;

        }

        [Fact]
        public async Task TestGetUser()
        {
            //Arange
            var context = OnlineGamesDbContextFactory.InitializeContext();
            await SeedData(context);
            var repo = new Repository<User>(context);
            var userService = new UserService(repo, null);
            //Act

            var result1 = await userService.GetUser("user1");
            var result2 = await userService.GetUser("user2");
            var result3 = await userService.GetUser("user3");

            //Assert
            Assert.Equal("user1", result1.Username);
            Assert.Equal("user2", result2.Username);
            Assert.Equal("user3", result3.Username);

        }
        [Fact]
        public async Task TestUpdateUser()
        {
            //Arange
            var context = OnlineGamesDbContextFactory.InitializeContext();
            await SeedData(context);
            var repo = new Repository<User>(context);
            var mock = new Mock<IIdentityService>();
            var real = repo.GetAll().Where(u => u.Id == "user1").FirstOrDefault();
            mock.Setup(m => m.GetJwt(real, "smt")).Returns("yes");
            var userService = new UserService(repo, mock.Object);
            //Act
            var result = await userService.UpdateUser("user1", "description", "not url", "username", "smt");

            //Assert
            Assert.Equal("username", result.Username);
            Assert.Equal("description", result.Description);
            Assert.Equal("not url", result.ImgUrl);
            Assert.Equal("yes", result.Token);
            real = repo.GetAll().Where(u => u.Id == "user1").FirstOrDefault();

            Assert.Equal("username", real.UserName);
            Assert.Equal("description", real.Description);
            Assert.Equal("not url", real.ImgUrl);

        }

        [Fact]
        public async Task TestGetUserIdFromName()
        {
            //Arange
            var context = OnlineGamesDbContextFactory.InitializeContext();
            await SeedData(context);
            var repo = new Repository<User>(context);
            var userService = new UserService(repo, null);
            //Act

            var result = await userService.GetUserIdFromName("user1");
            //Assert
            Assert.Equal("user1", result);
        }

    }
}
