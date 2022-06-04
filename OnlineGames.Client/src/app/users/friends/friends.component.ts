import { Component, OnInit } from '@angular/core';
@Component({
  selector: 'app-friends',
  templateUrl: './friends.component.html',
  styleUrls: ['./friends.component.css']
})
export class FriendsComponent implements OnInit {
  groupName!:string;
  groupId!:string
  constructor() { }
  ngOnInit(): void {
  }
  setRoomId($event:any){
    this.groupName=$event['groupName'];
    this.groupId=$event['id'];
  }
}
