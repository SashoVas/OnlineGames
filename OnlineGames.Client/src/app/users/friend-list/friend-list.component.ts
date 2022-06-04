import { Component, EventEmitter, OnInit, Output } from '@angular/core';
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
  @Output() setRoomNameEventEmmiter=new EventEmitter<any>();
  constructor(private userService:UserService,private messageService:MessageService ) { }

  ngOnInit(): void {
    this.fetchData();
  }

  changeFriendClick(id:string ){
    this.messageService.triggerChangeFriend(id);
  }
  fetchData(){
    this.userService
    .getFriends()
    .subscribe(data=>
      {
        this.friends=data;
        if(data.length>0&&data![0]['accepted'])
        {
          this.changeFriendClick(this.friends[0]["id"]);
          this.setRoomNameEventEmmiter.emit({id:this.friends[0]["id"],groupName:this.friends[0]['userName']});
        } 
        else
        {
          this.setRoomNameEventEmmiter.emit({id:null,groupName:null});
        }
      });
  }
  acceptFriend(id:string){
    this.userService.acceptFriendRequest(id).subscribe();
    this.fetchData();
  }
  unFriend(id:string){
    this.userService.unFriend(id).subscribe(()=>this.fetchData());
  }
}
