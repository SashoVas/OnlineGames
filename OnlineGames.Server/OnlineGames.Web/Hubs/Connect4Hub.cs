using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using OnlineGames.Services.Contracts;
using System.Security.Claims;

namespace OnlineGames.Web.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class Connect4Hub:GameHub
    {
        public Connect4Hub(IRoomService roomService) : base(roomService)
        {
        }

        public async Task TestAll()
        {
            await this.Clients.OthersInGroup(await roomService.GetRoomId(this.Context.User.FindFirstValue(ClaimTypes.NameIdentifier))).SendAsync("Connect4HubTest", 5);
        }

    }
}
