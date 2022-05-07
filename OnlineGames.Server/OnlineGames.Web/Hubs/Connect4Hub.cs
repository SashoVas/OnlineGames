﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using OnlineGames.Services.Contracts;
using OnlineGames.Web.Models.Connect4;
using System.Security.Claims;

namespace OnlineGames.Web.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class Connect4Hub:GameHub
    {
        private readonly IConnect4Service connect4Service;
        public Connect4Hub(IRoomService roomService, IConnect4Service connect4Service) : base(roomService)
        {
            this.connect4Service = connect4Service;
        }
        public async Task MakeMoveAI(Connect4MoveAIInput input)
        {
            try
            {
                //Skip update board if the ai is first
                if (input.Col != -1)
                {
                    await roomService.UpdateBoardConnect4(GetUserId(), input.Col,GetUserName());
                }
                var boardString = await roomService.GetUserBoard(GetUserId());
                if (!boardString.Contains("0"))
                {
                    //The board is full
                    return;
                }
                var currentPlayer = await roomService.GetTurn(GetUserId());
                var output = await this.connect4Service.MakeMove(boardString, currentPlayer, input.Difficulty);
                await roomService.UpdateBoardAIConnect4(GetUserId(), output);
                await this.Clients.Caller.SendAsync("OponentMove", output);
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        public async Task MakeMoveOponent( int col)
        {
            try
            {
                await roomService.UpdateBoardConnect4(GetUserId(), col,GetUserName());
                await this.Clients.OthersInGroup(await this.roomService.GetRoomId(GetUserId())).SendAsync("OponentMove", col);
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
