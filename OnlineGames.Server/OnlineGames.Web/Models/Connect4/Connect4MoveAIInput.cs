using System.ComponentModel.DataAnnotations;

namespace OnlineGames.Web.Models.Connect4
{
    public class Connect4MoveAIInput
    {
        [Required]
        [Range(0,6)]
        public int Col { get; set; }
        [Range(1,12)]
        public int Difficulty { get; set; }
    }
}
