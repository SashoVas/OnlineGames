using OnlineGames.Data;
using OnlineGames.Data.Models;
using OnlineGames.Services;
using OnlineGames.Services.Models.Friend;
using OnlineGames.Tests.Infrasturcture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OnlineGames.Tests.Services
{
    public class FriendSrviceTests
    {
        private List<Friend> GetDummyData()
        {
            User user2 = new User { Id = "user2", UserName = "user2" };
            var messages = new List<Friend>
            {
                new Friend {Id="1",User1Id="user1",User1=new User{ Id= "user1",UserName="user1" },User2Id="user2",User2=user2,Accepted=true,Date=DateTime.Now },
                new Friend {Id="2",User1Id="user3",User1=new User{ Id= "user3",UserName="user3" },User2Id="user2",User2=user2,Accepted=false,Date=DateTime.Now },
                //new Friend {},

            };
            return messages;
        }
        private async Task<List<Friend>> SeedData(OnlineGamesDbContext context)
        {
            var data = GetDummyData();
            context.Friends.AddRange(data);
            await context.SaveChangesAsync();
            return data;

        }
        [Fact]
        public async Task TestFriendExist()
        {
            //Arange
            var context = OnlineGamesDbContextFactory.InitializeContext();
            await SeedData(context);
            var repo = new Repository<Friend>(context);
            var friendService = new FriendService(repo,null);

            //Act
            bool first=await friendService.FriendExist("user1", "user2");
            bool second = await friendService.FriendExist("user1", "user3");
            bool third = await friendService.FriendExist("user3", "user1");
            bool fourth = await friendService.FriendExist("user2", "user1");

            //Assert
            Assert.True(first);
            Assert.True(fourth);
            Assert.False(second);
            Assert.False(third);
        }
        [Fact]
        public async Task TestAcceptFriendRequest()
        {
            //Arange
            var context = OnlineGamesDbContextFactory.InitializeContext();
            await SeedData(context);
            var repo = new Repository<Friend>(context);
            var friendService = new FriendService(repo, null);

            //Act

            var result= await friendService.AcceptFriendRequest("user2","user3");

            //Assert

            Assert.True(result);
            Assert.True(repo.GetAll().Last().Accepted);
        }

        [Fact]
        public async Task TestDeleteFriend()
        {
            //Arange
            var context = OnlineGamesDbContextFactory.InitializeContext();
            await SeedData(context);
            var repo = new Repository<Friend>(context);
            var friendService = new FriendService(repo, null);

            //Act
            var result = await friendService.DeleteFriend("user1","user2");

            //Assert
            Assert.True(result);
            Assert.Equal(1,repo.GetAll().Count());
            Assert.Null(repo.GetAll().FirstOrDefault(f => f.User1Id == "user1" && f.User2Id == "user2"));
        }

        [Fact]
        public async Task TestGetFriends()
        {
            //Arange
            var context = OnlineGamesDbContextFactory.InitializeContext();
            await SeedData(context);
            var repo = new Repository<Friend>(context);
            var friendService = new FriendService(repo, null);
            //Act

            var result = await friendService.GetFriends("user1");
            
            //Assert
            
            Assert.Single(result);
            Assert.Equal("user2", result.First().Id);
        }

        [Fact]
        public async Task TestGetRequests()
        {
            //Arange
            var context = OnlineGamesDbContextFactory.InitializeContext();
            await SeedData(context);
            var repo = new Repository<Friend>(context);
            var friendService = new FriendService(repo, null);
            //Act

            var result = await friendService.GetRequests("user3");

            //Assert

            Assert.Single(result);
            Assert.Equal("user2", result.First().Id);
        }

    }
}
