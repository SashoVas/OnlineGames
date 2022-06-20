import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ModesComponent } from './modes/modes.component';
import { GamesRoutingModule } from './games-routing.module';
import { RoomsComponent } from './rooms/rooms.component';
import { RoomsItemComponent } from './rooms-item/rooms-item.component';

@NgModule({
  declarations: [
    ModesComponent,
    RoomsComponent,
    RoomsItemComponent
  ],
  imports: [
    CommonModule,
    GamesRoutingModule
  ]
})
export class GamesModule { }
