using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace OnlineGames.Data.Models
{
    public class User:IdentityUser
    {
        public User()
        {
            this.FriendsWith = new HashSet<Friend>();
            this.FriendsOf = new HashSet<Friend>();
            this.Messages = new HashSet<Message>();
        }
        public string? Description { get; set; }
        public string? ImgUrl { get; set; }
        public Room? Room1 { get; set; }
        public Room? Room2 { get; set; }
        public ICollection<Friend> FriendsWith { get; set; }
        public ICollection<Friend> FriendsOf { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
