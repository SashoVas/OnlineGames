﻿using System.ComponentModel.DataAnnotations;

namespace OnlineGames.Web.Models.Identity
{
    public class RegisterInputModel
    {
        [Required]
        [MinLength(3)]
        public string UserName { get; set; }
        [Required]
        [MinLength(3)]
        public string Password { get; set; }
        [Required]
        [MinLength(3)]
        public string ConfirmPassword { get; set; }
    }
}
