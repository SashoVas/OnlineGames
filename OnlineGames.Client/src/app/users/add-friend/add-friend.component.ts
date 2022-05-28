import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-add-friend',
  templateUrl: './add-friend.component.html',
  styleUrls: ['./add-friend.component.css']
})
export class AddFriendComponent implements OnInit {

  constructor(private userService:UserService) { }
  @ViewChild('userNameField') userNameField!:ElementRef;
  ngOnInit(): void {
  }
  addFriend(){
    this.userService.sendFriendRequest(this.userNameField!.nativeElement.value).subscribe();
  }
}
