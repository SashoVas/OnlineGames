using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineGames.Data.Models;

namespace OnlineGames.Data
{
    public class OnlineGamesDbContext : IdentityDbContext<User>
    {
        public OnlineGamesDbContext(DbContextOptions<OnlineGamesDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
    }
}