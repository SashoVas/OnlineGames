using System.ComponentModel.DataAnnotations;

namespace OnlineGames.Web.Models.Room
{
    public class CreateRoomInputModel
    {
        [Required]
        public string Game { get; set; }
    }
}
