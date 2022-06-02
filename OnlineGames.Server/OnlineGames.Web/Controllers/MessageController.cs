using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineGames.Services.Contracts;
using System.Security.Claims;

namespace OnlineGames.Web.Controllers
{
    public class MessageController : ApiController
    {
        private readonly IMessageService messageService;
        public MessageController(IMessageService messageService) 
            => this.messageService = messageService;
        [HttpGet("{page}/{friendName}")]
        public async Task<object> GetMessages(int page, string friendName) 
            => await this.messageService.GetMessages(GetUserId(), friendName,page);
    }
}
