using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineGames.Data.Models;

namespace OnlineGames.Data
{
    public class OnlineGamesDbContext : IdentityDbContext<User>
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Message> Messages { get; set; }
        public OnlineGamesDbContext(DbContextOptions<OnlineGamesDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Room>()
                .HasOne(f => f.Player1)
                .WithOne(u => u.Room1)
                .HasForeignKey<Room>(r => r.Player1Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Room>()
                .HasOne(f => f.Player2)
                .WithOne(u => u.Room2)
                .HasForeignKey<Room>(r => r.Player2Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Friend>()
                .HasOne(f => f.User1)
                .WithMany(u => u.FriendsWith)
                .HasForeignKey(f => f.User1Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Friend>()
                .HasOne(f => f.User2)
                .WithMany(u => u.FriendsOf)
                .HasForeignKey(f => f.User2Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Friend>()
                .HasMany(f => f.Messages)
                .WithOne(m => m.FriendChat)
                .HasForeignKey(m => m.FriendChatId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Room>()
                .HasMany(r => r.Messages)
                .WithOne(m => m.RoomChat)
                .HasForeignKey(m => m.RoomChatId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}