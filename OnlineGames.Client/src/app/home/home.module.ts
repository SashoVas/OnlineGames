import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { CoreModule } from '../core/core.module';
import { LandingComponent } from './landing/landing.component';
import { HomeRoutingModule } from './home-routing.module';
import { RoomsItemComponent } from './rooms-item/rooms-item.component';
import { GamesComponent } from './games/games.component';
import { RoomsComponent } from './rooms/rooms.component';
import { NotificationsComponent } from './notifications/notifications.component';



@NgModule({
  declarations: [LandingComponent, RoomsItemComponent, GamesComponent, RoomsComponent, NotificationsComponent],
  imports: [
    CommonModule,
    RouterModule,
    HttpClientModule,
    CoreModule,
    HomeRoutingModule
  ]
})
export class HomeModule { }
