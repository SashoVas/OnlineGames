import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Observable } from 'rxjs';
import { IFriend } from 'src/app/core/interfaces/IFriend';
import { MessageService } from 'src/app/core/services/message.service';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-friend-list',
  templateUrl: './friend-list.component.html',
  styleUrls: ['./friend-list.component.css']
})
export class FriendListComponent implements OnInit {
  friends!:Array<IFriend>;
  constructor(private userService:UserService,private messageService:MessageService ) { }

  ngOnInit(): void {
    this.fetchData();
  }

  changeFriendClick(friendUserName:string ){
    console.log('clicked');
    this.messageService.triggerChangeFriend(friendUserName);
  }
  fetchData(){
    this.userService
    .getFriends()
    .subscribe(data=>this.friends=data);
  }
  acceptFriend(friendUserName:string){
    this.userService.acceptFriendRequest(friendUserName).subscribe();
    this.fetchData();
  }
}
