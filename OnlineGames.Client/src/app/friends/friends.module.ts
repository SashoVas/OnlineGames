import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FriendsComponent } from './friends/friends.component';
import { FriendListComponent } from './friend-list/friend-list.component';
import { AddFriendComponent } from './add-friend/add-friend.component';
import { CoreModule } from '../core/core.module';
import { FriendsRoutingModule } from './friends-routing.module';



@NgModule({
  declarations: 
  [FriendsComponent,
  FriendListComponent,
  AddFriendComponent
  ],
  imports: [
    CommonModule,
    FriendsRoutingModule,
    CoreModule
  ]
})
export class FriendsModule { }
