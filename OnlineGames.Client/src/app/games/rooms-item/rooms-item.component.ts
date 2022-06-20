import { Component,  Input,  OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IRoom } from 'src/app/core/interfaces/IRoom';
import { RoomService } from '../services/room.service';

@Component({
  selector: 'app-rooms-item',
  templateUrl: './rooms-item.component.html',
  styleUrls: ['./rooms-item.component.css']
})
export class RoomsItemComponent implements OnInit {
  rooms!:Array<IRoom>;
  page:number=0;
  @Input() game:string|null=null;
  @Input() count:number=5;
  constructor(private router:Router,private roomService:RoomService) { 
  }
  ngOnInit(): void {
    this.fetchData();
  }

  refreshRooms(){
    this.page=0;
    this.fetchData();
  }
  fetchData(){
    this.roomService
    .getAvailableRooms(this.game,this.count,this.page)
    .subscribe(data=>this.rooms=data);
  }
  joinRoom(roomId:string,first?:boolean,game?:string){
    this.roomService.setUserToRoom(roomId).subscribe((room:IRoom)=>{
      if(game=="TicTacToe")
      {
        this.router.navigate(['tictactoe/tictactoe'], { queryParams: { roomName:room['roomId'] ,first:first} });
      }
      else{
        this.router.navigate(['connect4/connect4game'], { queryParams: { roomName:room['roomId'] ,first:first} });
      }
    },
    (error)=>this.refreshRooms());
    
  }
  loadRooms(){
    this.page++;
    this.roomService
    .getAvailableRooms(this.game,this.count,this.page)
    .subscribe(data=>this.rooms=this.rooms.concat(data));
    
  }
}
