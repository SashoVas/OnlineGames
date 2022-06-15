﻿using Microsoft.AspNetCore.Mvc;
using OnlineGames.Services.Contracts;
using OnlineGames.Services.Models.Room;
using OnlineGames.Web.Models.Room;

namespace OnlineGames.Web.Controllers
{
    public class RoomController : ApiController
    {
        private readonly IRoomService roomService;
        public RoomController(IRoomService roomService) 
            => this.roomService = roomService;
        [HttpPost]
        public async Task<ActionResult<object>> CreateRoom(CreateRoomInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            string roomId;
            int board;
            switch (input.Game)
            {
                case ("TicTacToe"):
                    board = 3 * 3;
                    break;
                case ("Connect4"):
                    board = 6 * 7;
                    break;
                default:
                    return this.BadRequest();
            }
            roomId = await this.roomService.CreateRoom(this.User.Identity.Name, false, input.Game, board);
            await this.roomService.SetRoomToUser(GetUserId(), roomId,User.Identity.Name);
            return Created(nameof(this.Created),new
            {
                RoomId = roomId
            });
        }

        [HttpPut]
        public async Task<ActionResult<object>>AddToRoom(AddToRoomInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                await this.roomService.SetRoomToUser(GetUserId(), input.RoomId,User.Identity.Name);
                return Ok(new {RoomId=input.RoomId });
            }
            catch (Exception)
            {
                return this.BadRequest();
            }
            
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomsServiceModel>>>GetRooms([FromQuery]GetRoomsInputModel input)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok( await this.roomService.GetAvailableRooms(input.Game=="null"?null: input.Game,input.Count,input.Page));
        }
    }
}