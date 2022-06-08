namespace OnlineGames.Services.Models.User
{
    public class UserServiceModel
    {
        public string Username { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public int Friends { get; set; }
        public int Wins { get; set; }
        public bool IsMe { get; set; }
    }
}
