import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsersRoutingModule } from './users-routing.module';
import { CoreModule } from '../core/core.module';
import { ProfileComponent } from './profile/profile.component';
import { EditProfileComponent } from './edit-profile/edit-profile.component';
import { ReactiveFormsModule } from '@angular/forms';
import { NotificationsComponent } from './notifications/notifications.component';

@NgModule({
  declarations: [
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
