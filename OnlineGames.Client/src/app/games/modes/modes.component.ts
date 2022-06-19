import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { IRoom } from 'src/app/core/interfaces/IRoom';
import { RoomService } from '../services/room.service';

@Component({
  selector: 'app-modes',
  templateUrl: './modes.component.html',
  styleUrls: ['./modes.component.css']
})
export class ModesComponent implements OnInit {

  @ViewChild('ticTacToeRoomId')
  ticTacToeRoomId?:ElementRef;
  @ViewChild('connect4RoomId')
  connect4RoomId?:ElementRef;
  constructor(private router:Router,private roomService:RoomService) { }
  ngOnInit(): void {
  }
  createRoomTicTacToe(){
    this.roomService.createRoom("TicTacToe").subscribe((data)=>
      this.router.navigate(['games/modes/tictactoe/'], { queryParams: { roomName: data['roomId'] ,first:true} }));
    
  }
  createRoomConnect4(){
    this.roomService.createRoom("Connect4").subscribe((data)=>
      this.router.navigate(['games/modes/connect4/'], { queryParams: { roomName: data['roomId'] ,first:true} }));
  }
  joinRoomTicTacToe(){
    let roomId:string=this.ticTacToeRoomId?.nativeElement.value;
    if(roomId!='')
    {
      this.roomService.setUserToRoom(roomId).subscribe((room:IRoom)=>{
            this.router.navigate(['games/modes/tictactoe/'], { queryParams: { roomName:room['roomId'] ,first:false} });
          });
    }
  }
  joinRoomConnect4(){
    let roomId:string=this.connect4RoomId?.nativeElement.value;
    if (roomId!='')
    {
      this.roomService.setUserToRoom(roomId).subscribe((room:IRoom)=>{
            this.router.navigate(['games/modes/connect4/'], { queryParams: { roomName:room['roomId'] ,first:false} });
          });      
    }
  }

}
