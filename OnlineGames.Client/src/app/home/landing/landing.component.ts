
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { IRoom } from '../../core/interfaces/IRoom';
import { RoomService } from '../services/room.service';


@Component({
  selector: 'app-landing',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.css']
})
export class LandingComponent implements OnInit {

  @ViewChild('roomId')
  roomId?:ElementRef;

  constructor(private router:Router,private roomService:RoomService) { }
  ngOnInit(): void {
  }

  createRoom(){
    this.roomService.createRoom().subscribe((data)=>
      this.router.navigate(['tictactoe/tictactoe'], { queryParams: { roomName: data['roomId'] ,first:true} }));
    
  }
  joinRoom(){
    let roomId:string=this.roomId?.nativeElement.value;
    this.roomService.setUserToRoom(roomId).subscribe();
    this.router.navigate(['tictactoe/tictactoe'], { queryParams: { roomName:roomId ,first:false} });
  }
}