using System.ComponentModel.DataAnnotations;

namespace OnlineGames.Web.Models.Identity
{
    public class LoginInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string UserName { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Password { get; set; }
    }
}
