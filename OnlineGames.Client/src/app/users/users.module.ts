import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsersRoutingModule } from './users-routing.module';
import { FriendsComponent } from './friends/friends.component';
import { FriendListComponent } from './friend-list/friend-list.component';
import { CoreModule } from '../core/core.module';
import { AddFriendComponent } from './add-friend/add-friend.component';
import { ProfileComponent } from './profile/profile.component';
import { EditProfileComponent } from './edit-profile/edit-profile.component';
import { ReactiveFormsModule } from '@angular/forms';
import { NotificationsComponent } from './notifications/notifications.component';



@NgModule({
  declarations: [
    FriendsComponent,
    FriendListComponent,
    AddFriendComponent,
    ProfileComponent,
    EditProfileComponent,
    NotificationsComponent
  ],
  imports: [
    CommonModule,
    UsersRoutingModule,
    CoreModule,
    ReactiveFormsModule
  ]
})
export class UsersModule { }
