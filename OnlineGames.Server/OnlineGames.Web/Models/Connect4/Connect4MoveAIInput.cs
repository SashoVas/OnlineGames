using System.ComponentModel.DataAnnotations;

namespace OnlineGames.Web.Models.Connect4
{
    public class Connect4MoveAIInput: Connect4MoveInput
    {
        [Range(1,12)]
        public int Difficulty { get; set; }
    }
}
