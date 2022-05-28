import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsersRoutingModule } from './users-routing.module';
import { FriendsComponent } from './friends/friends.component';
import { FriendListComponent } from './friend-list/friend-list.component';
import { CoreModule } from '../core/core.module';
import { AddFriendComponent } from './add-friend/add-friend.component';



@NgModule({
  declarations: [
    FriendsComponent,
    FriendListComponent,
    AddFriendComponent
  ],
  imports: [
    CommonModule,
    UsersRoutingModule,
    CoreModule
  ]
})
export class UsersModule { }
