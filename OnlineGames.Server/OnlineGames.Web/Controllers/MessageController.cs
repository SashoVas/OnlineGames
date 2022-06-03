using Microsoft.AspNetCore.Mvc;
using OnlineGames.Services.Contracts;

namespace OnlineGames.Web.Controllers
{
    public class MessageController : ApiController
    {
        private readonly IMessageService messageService;
        public MessageController(IMessageService messageService) 
            => this.messageService = messageService;
        [HttpGet("{page}/{id}")]
        public async Task<object> GetMessages(int page, string id) 
            => await this.messageService.GetMessages(GetUserId(), id,page);
    }
}
