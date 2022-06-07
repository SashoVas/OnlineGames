namespace OnlineGames.Services.Models.Room
{
    public class RoomsServiceModel
    {
        public string UserName { get; set; }
        public string GameName { get; set; }
        public int Capacity { get; set; }
        public int Players { get; set; }
        public string RoomId { get; set; }
        public bool First { get; set; }
    }
}
