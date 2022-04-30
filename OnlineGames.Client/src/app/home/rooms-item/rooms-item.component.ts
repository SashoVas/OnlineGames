import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { IRoom } from 'src/app/core/interfaces/IRoom';
import { RoomService } from '../services/room.service';

@Component({
  selector: 'app-rooms-item',
  templateUrl: './rooms-item.component.html',
  styleUrls: ['./rooms-item.component.css']
})
export class RoomsItemComponent implements OnInit,OnDestroy {
  rooms!:Array<IRoom>;
  roomsSubscribtion:Subscription;
  constructor(private router:Router,private roomService:RoomService) { 
    this.roomsSubscribtion=this.roomService
    .getAvailableRooms()
    .subscribe(data=>this.rooms=data);
  }
  ngOnDestroy(): void {
    this.roomsSubscribtion.unsubscribe();
  }
  refreshRooms(){
    this.roomsSubscribtion=this.roomService
    .getAvailableRooms()
    .subscribe(data=>this.rooms=data);
  }
  ngOnInit(): void {
  }
  joinRoom(roomId:string,first:boolean,game:string){
    this.roomService.setUserToRoom(roomId).subscribe();
    if(game=="TicTacToe")
      {
        this.router.navigate(['tictactoe/tictactoe'], { queryParams: { roomName:roomId ,first:first} });
      }
      else{
        this.router.navigate(['connect4/connect4game'], { queryParams: { roomName:roomId ,first:first} });
      }
  }
}
