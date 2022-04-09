using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineGames.Data.Models;

namespace OnlineGames.Data
{
    public class OnlineGamesDbContext : IdentityDbContext<User>
    {
        public DbSet<Game> Games { get; set; }
        public OnlineGamesDbContext(DbContextOptions<OnlineGamesDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Game>()
                .HasMany(g=>g.Players)
                .WithOne(u=>u.Game)
                .HasForeignKey(u=>u.Id);

        }
    }
}