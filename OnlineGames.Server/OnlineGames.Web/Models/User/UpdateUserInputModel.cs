using System.ComponentModel.DataAnnotations;

namespace OnlineGames.Web.Models.User
{
    public class UpdateUserInputModel
    {
        [MaxLength(300)]
        public string? Description { get; set; }
        [Url]
        public string? ImgUrl { get; set; }
        [MaxLength(50)]
        public string? UserName { get; set; }
    }
}
