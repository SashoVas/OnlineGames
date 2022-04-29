using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineGames.Data.Models;

namespace OnlineGames.Data
{
    public class OnlineGamesDbContext : IdentityDbContext<User>
    {
        public DbSet<Room> TicTacToeRooms { get; set; }
        public OnlineGamesDbContext(DbContextOptions<OnlineGamesDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>()
                .HasOne(u => u.TicTacToeRoom)
                .WithMany(t => t.Users)
                .HasForeignKey(u => u.TicTacToeRoomId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}