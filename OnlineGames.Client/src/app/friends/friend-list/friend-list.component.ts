import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { IFriend } from 'src/app/core/interfaces/IFriend';
import { MessageService } from 'src/app/core/services/message.service';
import { FriendService } from '../../users/services/friend.service';

@Component({
  selector: 'app-friend-list',
  templateUrl: './friend-list.component.html',
  styleUrls: ['./friend-list.component.css']
})
export class FriendListComponent implements OnInit {
  friends!:Array<IFriend>;
  @Output() setRoomNameEventEmmiter=new EventEmitter<any>();
  constructor(private friendService:FriendService,private messageService:MessageService ) { }

  ngOnInit(): void {
    this.fetchData();
  }

  changeFriendClick(id:string,userName:string,i:number ){
    this.setRoomNameEventEmmiter.emit({id:id,groupName:userName});
    this.messageService.triggerChangeFriend(id);
    this.friends[i].haveMessage=false
  }
  fetchData(){
    this.friendService
    .getFriends()
    .subscribe(data=>
      {
        this.friends=data;
        if(data.length>0&&data![0]['accepted'])
        {
          this.changeFriendClick(this.friends[0]["id"],this.friends[0]["userName"],0);
        } 
        else
        {
          this.setRoomNameEventEmmiter.emit({id:null,groupName:null});
        }
      });
  }
  acceptFriend(id:string){
    this.friendService.acceptFriendRequest(id).subscribe();
    this.fetchData();
  }
  unFriend(id:string){
    this.friendService.unFriend(id).subscribe(()=>this.fetchData());
  }
}
