import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-friends',
  templateUrl: './friends.component.html',
  styleUrls: ['./friends.component.css']
})
export class FriendsComponent implements OnInit {
  groupName!:string;
  constructor() { }
  ngOnInit(): void {
  }
  setRoomId($event:any){
    this.groupName=$event['groupName'];
  }
}
