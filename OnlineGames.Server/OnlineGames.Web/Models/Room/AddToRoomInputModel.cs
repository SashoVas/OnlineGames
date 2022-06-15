using System.ComponentModel.DataAnnotations;

namespace OnlineGames.Web.Models.Room
{
    public class AddToRoomInputModel
    {
        [Required]
        public string RoomId { get; set; }
    }
}
