using Microsoft.EntityFrameworkCore;
using OnlineGames.Data;
using System;


namespace OnlineGames.Tests.Infrasturcture
{
    internal class OnlineGamesDbContextFactory
    {
        public static OnlineGamesDbContext InitializeContext()
        {
            var options = new DbContextOptionsBuilder<OnlineGamesDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            
            var context = new OnlineGamesDbContext(options);
            return context;
        }
    }
}
