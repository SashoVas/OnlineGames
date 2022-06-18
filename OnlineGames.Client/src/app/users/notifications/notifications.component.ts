import { Component, OnInit } from '@angular/core';
import { INotifications } from 'src/app/core/interfaces/INotifications';
import { FriendService } from '../services/friend.service';
import { NotificationsService } from '../services/notifications.service';

@Component({
  selector: 'app-notifications',
  templateUrl: './notifications.component.html',
  styleUrls: ['./notifications.component.css']
})
export class NotificationsComponent implements OnInit {
 notifications!:INotifications
  constructor(private notificationsService:NotificationsService,private friendService:FriendService) { 
    this.fetchData();
  }

  ngOnInit(): void {

  }
  fetchData(){
    this.notificationsService.getNorifications().subscribe(data=>this.notifications=data);
  }
  acceptFriend(id:string){
    this.friendService.acceptFriendRequest(id).subscribe(()=>this.fetchData());
  }
  unFriend(id:string){
    this.friendService.unFriend(id).subscribe(()=>this.fetchData());
  }
}
