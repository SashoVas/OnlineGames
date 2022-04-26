
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { RoomService } from '../services/room.service';


@Component({
  selector: 'app-landing',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.css']
})
export class LandingComponent implements OnInit {

  @ViewChild('ticTacToeRoomId')
  ticTacToeRoomId?:ElementRef;
  @ViewChild('connect4RoomId')
  connect4RoomId?:ElementRef;
  constructor(private router:Router,private roomService:RoomService) { }
  ngOnInit(): void {
  }

  createRoomTicTacToe(){
    this.roomService.createRoomTicTacToe().subscribe((data)=>
      this.router.navigate(['tictactoe/tictactoe'], { queryParams: { roomName: data['roomId'] ,first:true} }));
    
  }
  joinRoomTicTacToe(){
    let roomId:string=this.ticTacToeRoomId?.nativeElement.value;
    this.roomService.setUserToRoom(roomId).subscribe();
    this.router.navigate(['tictactoe/tictactoe'], { queryParams: { roomName:roomId ,first:false} });
  }

  createRoomConnect4(){
    this.roomService.createRoomConnect4().subscribe((data)=>
      this.router.navigate(['connect4/connect4game'], { queryParams: { roomName: data['roomId'] ,first:true} }));
    
  }
  joinRoomConnect4(){
    let roomId:string=this.connect4RoomId?.nativeElement.value;
    this.roomService.setUserToRoom(roomId).subscribe();
    this.router.navigate(['connect4/connect4game'], { queryParams: { roomName:roomId ,first:false} });
  }
}