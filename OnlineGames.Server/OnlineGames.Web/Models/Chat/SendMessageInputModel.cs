namespace OnlineGames.Web.Models.Chat
{
    public class SendMessageInputModel
    {
        public string GroupName { get; set; }
        public string Contents { get; set; }
        public bool IsName { get; set; }
    }
}
