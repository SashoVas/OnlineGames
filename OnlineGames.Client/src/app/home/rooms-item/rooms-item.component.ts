import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { IRoom } from 'src/app/core/interfaces/IRoom';
import { RoomService } from '../services/room.service';

@Component({
  selector: 'app-rooms-item',
  templateUrl: './rooms-item.component.html',
  styleUrls: ['./rooms-item.component.css']
})
export class RoomsItemComponent implements OnInit {
  rooms$:Observable<Array<IRoom>>;
  constructor(private router:Router,private roomService:RoomService) { 
    this.rooms$=this.roomService.getAvailableRooms();
  }
  
  ngOnInit(): void {
  }
  joinRoom(roomId:string,first:boolean){
    this.roomService.setUserToRoom(roomId).subscribe();
    this.router.navigate(['tictactoe/tictactoe'], { queryParams: { roomName:roomId ,first:first} });
  }
}
