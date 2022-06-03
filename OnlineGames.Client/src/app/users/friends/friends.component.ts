import { Component, OnInit } from '@angular/core';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-friends',
  templateUrl: './friends.component.html',
  styleUrls: ['./friends.component.css']
})
export class FriendsComponent implements OnInit {
  groupName!:string;
  constructor(private userService:UserService) { }
  ngOnInit(): void {
  }
  setRoomId($event:any){
    this.groupName=$event['groupName'];
  }
  unFriend(){
    this.userService.unFriend(this.groupName).subscribe();
  }
}
