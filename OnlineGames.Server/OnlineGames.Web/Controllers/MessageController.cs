using Microsoft.AspNetCore.Mvc;
using OnlineGames.Services.Contracts;
using OnlineGames.Services.Models.Message;

namespace OnlineGames.Web.Controllers
{
    public class MessageController : ApiController
    {
        private readonly IMessageService messageService;
        public MessageController(IMessageService messageService)
            => this.messageService = messageService;
        [HttpGet("{page}/{id}")]
        public async Task<ActionResult<MessageServiceModel>> GetMessages(int page, string id)
            => Ok(await this.messageService.GetMessages(GetUserId(), id, page));
    }
}
