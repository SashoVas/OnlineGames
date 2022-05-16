using System.ComponentModel.DataAnnotations;

namespace OnlineGames.Web.Models.Connect4
{
    public class Connect4MoveInput
    {
        [Required]
        [Range(0, 6)]
        public int Col { get; set; }
    }
}
