using System.ComponentModel.DataAnnotations;

namespace OnlineGames.Web.Models.Identity
{
    public class RegisterInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string UserName { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Password { get; set; }
        [Required]
        [MaxLength(100)]
        public string ConfirmPassword { get; set; }
    }
}
