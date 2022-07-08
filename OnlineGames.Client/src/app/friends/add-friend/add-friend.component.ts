import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FriendService } from '../../users/services/friend.service';
@Component({
  selector: 'app-add-friend',
  templateUrl: './add-friend.component.html',
  styleUrls: ['./add-friend.component.css']
})
export class AddFriendComponent implements OnInit {

  constructor(private friendService:FriendService) { }
  @ViewChild('userNameField') userNameField!:ElementRef;
  ngOnInit(): void {
  }
  addFriend(){
    this.friendService.sendFriendRequest(this.userNameField!.nativeElement.value).subscribe();
  }
}
