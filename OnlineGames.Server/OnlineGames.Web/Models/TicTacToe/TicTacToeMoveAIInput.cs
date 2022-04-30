﻿using System.ComponentModel.DataAnnotations;

namespace OnlineGames.Web.Models.TicTacToe
{
    public class TicTacToeMoveAIInput
    {
        [Required]
        [Range(0,2)]
        public int Row { get; set; }
        [Required]
        [Range(0, 2)]
        public int Col { get; set; }
    }
}
