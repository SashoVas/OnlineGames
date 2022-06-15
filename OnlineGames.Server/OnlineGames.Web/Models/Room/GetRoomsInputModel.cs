using System.ComponentModel.DataAnnotations;

namespace OnlineGames.Web.Models.Room
{
    public class GetRoomsInputModel
    {
        [Required]
        public string Game { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public int Page { get; set; }
    }
}
