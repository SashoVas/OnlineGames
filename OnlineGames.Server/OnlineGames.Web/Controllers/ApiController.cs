using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace OnlineGames.Web.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        protected string GetUserId()
            => User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
