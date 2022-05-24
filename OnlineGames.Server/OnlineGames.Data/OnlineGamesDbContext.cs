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

            builder.Entity<User>()
                .HasOne(u => u.Room)
                .WithMany(t => t.Users)
                .HasForeignKey(u => u.RoomId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Friend>()
                .HasOne(f => f.User1)
                .WithMany(u => u.FriendsWith)
                .HasForeignKey(f => f.User1Id);

            builder.Entity<Friend>()
                .HasOne(f => f.User2)
                .WithMany(u => u.FriendsOf)
                .HasForeignKey(f => f.User2Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Friend>()
                .HasMany(f => f.Messages)
                .WithOne(m => m.FriendChat)
                .HasForeignKey(m => m.FriendChatId);

            builder.Entity<Room>()
                .HasMany(r => r.Messages)
                .WithOne(m => m.RoomChat)
                .HasForeignKey(m => m.RoomChatId);

            builder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.SenderId);
        }
    }
}