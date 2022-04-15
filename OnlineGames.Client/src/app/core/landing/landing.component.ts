
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { RoomService } from '../services/room.service';


@Component({
  selector: 'app-landing',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.css']
})
export class LandingComponent implements OnInit {

  constructor(private router:Router,private roomService:RoomService) { }
  @ViewChild('roomId')
  roomId?:ElementRef;
  ngOnInit(): void {

  }

  createRoom(){
    this.roomService.createRoom().subscribe((data)=>
      this.router.navigate(['games/tictactoe/tictactoe'], { queryParams: { roomName: data['roomId'] ,first:true} }));
    
  }
  joinRoom(){
    console.log(this.roomId);
    this.router.navigate(['games/tictactoe/tictactoe'], { queryParams: { roomName: this.roomId?.nativeElement.value ,first:false} });
  }
}